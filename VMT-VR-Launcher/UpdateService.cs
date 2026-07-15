using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;
using VMT_VR_Launcher.Models;

namespace VMT_VR_Launcher
{
    /// <summary>
    /// Progress data reported during update operations.
    /// </summary>
    public class UpdateProgress
    {
        public int Percentage { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// Handles all update/extraction logic for the VMT VR Launcher.
    /// Implements safe StreamingAssets preservation during Unity build updates.
    /// </summary>
    public class UpdateService
    {
        // ═══════════════════════════════════════════════════════════════
        //  BUILD DIRECTORY DETECTION
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Finds the *_Data folder inside the build directory.
        /// Unity builds always have a folder named "{GameName}_Data".
        /// </summary>
        public static string? FindDataFolder(string buildPath)
        {
            if (string.IsNullOrWhiteSpace(buildPath) || !Directory.Exists(buildPath))
                return null;

            var dataFolders = Directory.GetDirectories(buildPath, "*_Data");
            return dataFolders.Length > 0 ? dataFolders[0] : null;
        }

        /// <summary>
        /// Gets the StreamingAssets path from the build directory.
        /// Returns: {buildPath}/{*_Data}/StreamingAssets/
        /// </summary>
        public static string? FindStreamingAssetsPath(string buildPath)
        {
            var dataFolder = FindDataFolder(buildPath);
            if (dataFolder == null) return null;

            string saPath = Path.Combine(dataFolder, "StreamingAssets");
            return Directory.Exists(saPath) ? saPath : null;
        }

        /// <summary>
        /// Auto-detects the game executable in the build directory.
        /// Matches the *_Data folder name convention: "Game Name_Data" → "Game Name.exe"
        /// </summary>
        public static string? FindGameExecutable(string buildPath)
        {
            var dataFolder = FindDataFolder(buildPath);
            if (dataFolder == null) return null;

            string folderName = Path.GetFileName(dataFolder);
            // Remove "_Data" suffix to get the game name
            string gameName = folderName[..^5]; // Remove "_Data"
            string exePath = Path.Combine(buildPath, $"{gameName}.exe");

            return File.Exists(exePath) ? exePath : null;
        }

        /// <summary>
        /// Checks if the game process is currently running.
        /// </summary>
        public static bool IsGameRunning(string buildPath)
        {
            var exePath = FindGameExecutable(buildPath);
            if (exePath == null) return false;

            string processName = Path.GetFileNameWithoutExtension(exePath);
            return Process.GetProcessesByName(processName).Length > 0;
        }

        // ═══════════════════════════════════════════════════════════════
        //  JSON DATA LOADING
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Reads BuildInfo.json from StreamingAssets.
        /// </summary>
        public static BuildInfo? LoadBuildInfo(string buildPath)
        {
            var saPath = FindStreamingAssetsPath(buildPath);
            if (saPath == null) return null;

            string jsonPath = Path.Combine(saPath, "BuildInfo.json");
            return LoadJson<BuildInfo>(jsonPath);
        }

        /// <summary>
        /// Reads NetworkData.json from StreamingAssets.
        /// </summary>
        public static NetworkData? LoadNetworkData(string buildPath)
        {
            var saPath = FindStreamingAssetsPath(buildPath);
            if (saPath == null) return null;

            string jsonPath = Path.Combine(saPath, "NetworkData.json");
            return LoadJson<NetworkData>(jsonPath);
        }

        private static T? LoadJson<T>(string path) where T : class
        {
            if (!File.Exists(path)) return null;

            try
            {
                string json = File.ReadAllText(path);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UpdateService] Failed to read {Path.GetFileName(path)}: {ex.Message}");
                return null;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        //  UPDATE PROCESS
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Validates that a zip file has a reasonable Unity build structure.
        /// </summary>
        public static bool ValidateZipStructure(string zipPath)
        {
            try
            {
                using var archive = ZipFile.OpenRead(zipPath);
                // Check for at least one *_Data folder entry
                bool hasDataFolder = archive.Entries.Any(e =>
                    e.FullName.Contains("_Data/") || e.FullName.Contains("_Data\\"));
                return hasDataFolder;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Performs the full update process with StreamingAssets preservation.
        /// 
        /// Mechanism:
        /// 1. Backup entire StreamingAssets folder to temp
        /// 2. Delete BuildInfo.json from backup (we want the new one from zip)
        /// 3. Extract zip into build directory (overwrites everything)
        /// 4. Restore old StreamingAssets files that are NOT in the new extraction
        ///    (preserves DataSave, images, configs, etc.)
        /// 5. Cleanup temp backup
        /// </summary>
        public static async Task PerformUpdateAsync(
            string zipPath,
            string buildPath,
            IProgress<UpdateProgress>? progress = null,
            CancellationToken cancellationToken = default)
        {
            // Step 1: Validate
            progress?.Report(new UpdateProgress { Percentage = 0, Status = "Validating..." });
            await Task.Delay(200, cancellationToken); // Brief pause for UI

            if (!File.Exists(zipPath))
                throw new FileNotFoundException("ZIP file not found.", zipPath);

            if (!Directory.Exists(buildPath))
                throw new DirectoryNotFoundException($"Build directory not found: {buildPath}");

            // Step 2: Check if game is running
            progress?.Report(new UpdateProgress { Percentage = 2, Status = "Checking running processes..." });
            if (IsGameRunning(buildPath))
                throw new InvalidOperationException(
                    "The game is currently running! Please close it before updating.");

            // Step 3: Backup StreamingAssets
            progress?.Report(new UpdateProgress { Percentage = 5, Status = "Backing up StreamingAssets..." });

            string? saPath = FindStreamingAssetsPath(buildPath);
            string tempBackupPath = Path.Combine(Path.GetTempPath(), $"VMT_SA_Backup_{DateTime.Now:yyyyMMdd_HHmmss}");

            bool hasExistingStreamingAssets = false;

            if (saPath != null && Directory.Exists(saPath))
            {
                hasExistingStreamingAssets = true;
                await Task.Run(() => CopyDirectory(saPath, tempBackupPath), cancellationToken);

                // Delete BuildInfo.json from backup (we want the new one)
                string backupBuildInfo = Path.Combine(tempBackupPath, "BuildInfo.json");
                if (File.Exists(backupBuildInfo))
                {
                    File.Delete(backupBuildInfo);
                    Debug.WriteLine("[UpdateService] Removed BuildInfo.json from backup (will use new version)");
                }
            }

            progress?.Report(new UpdateProgress { Percentage = 15, Status = "Backup complete" });
            cancellationToken.ThrowIfCancellationRequested();

            // Step 4: Extract ZIP to build directory
            progress?.Report(new UpdateProgress { Percentage = 20, Status = "Extracting update..." });

            await Task.Run(() =>
            {
                using var archive = ZipFile.OpenRead(zipPath);
                int totalEntries = archive.Entries.Count;
                int processed = 0;

                foreach (var entry in archive.Entries)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string destinationPath = Path.Combine(buildPath, entry.FullName);

                    // Ensure directory exists
                    string? dirPath = Path.GetDirectoryName(destinationPath);
                    if (dirPath != null && !Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);

                    // Skip directories (entries ending with / or \)
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        processed++;
                        continue;
                    }

                    // Extract file (overwrite)
                    entry.ExtractToFile(destinationPath, overwrite: true);

                    processed++;
                    int pct = 20 + (int)(60.0 * processed / totalEntries);
                    progress?.Report(new UpdateProgress
                    {
                        Percentage = pct,
                        Status = $"Extracting... ({processed}/{totalEntries})"
                    });
                }
            }, cancellationToken);

            progress?.Report(new UpdateProgress { Percentage = 80, Status = "Extraction complete" });

            // Step 5: Restore old StreamingAssets files (merge)
            if (hasExistingStreamingAssets && Directory.Exists(tempBackupPath))
            {
                progress?.Report(new UpdateProgress { Percentage = 82, Status = "Restoring preserved data..." });

                // Re-detect StreamingAssets path (it might have changed if _Data folder name changed)
                string? newSaPath = FindStreamingAssetsPath(buildPath);
                if (newSaPath != null)
                {
                    await Task.Run(() =>
                    {
                        MergeDirectories(tempBackupPath, newSaPath);
                    }, cancellationToken);
                }

                progress?.Report(new UpdateProgress { Percentage = 92, Status = "Data restored" });
            }

            // Step 6: Cleanup
            progress?.Report(new UpdateProgress { Percentage = 95, Status = "Cleaning up..." });
            try
            {
                if (Directory.Exists(tempBackupPath))
                    Directory.Delete(tempBackupPath, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UpdateService] Failed to cleanup temp: {ex.Message}");
                // Non-fatal, continue
            }

            progress?.Report(new UpdateProgress { Percentage = 100, Status = "Update complete! ✓" });
        }

        // ═══════════════════════════════════════════════════════════════
        //  FILE SYSTEM HELPERS
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Deep-copies a directory to a new location.
        /// </summary>
        private static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }

        /// <summary>
        /// Merges source directory into destination.
        /// Only copies files that do NOT exist in the destination (preserves new files).
        /// </summary>
        private static void MergeDirectories(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                if (!File.Exists(destFile))
                {
                    File.Copy(file, destFile);
                    Debug.WriteLine($"[UpdateService] Restored: {Path.GetFileName(file)}");
                }
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(dir);
                string destSubDir = Path.Combine(destDir, dirName);
                MergeDirectories(dir, destSubDir);
            }
        }
    }
}
