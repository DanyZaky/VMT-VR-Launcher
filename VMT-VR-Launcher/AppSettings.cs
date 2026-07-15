using System.Text.Json;

namespace VMT_VR_Launcher
{
    /// <summary>
    /// Manages persistent application settings.
    /// Settings are stored as a JSON file in the application directory.
    /// </summary>
    public class AppSettings
    {
        private static readonly string SettingsFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "launcher_settings.json");

        /// <summary>
        /// Path to the Unity build directory (e.g., Z:\_PROJECTS\BUILD Files\BUILD vmt-player\)
        /// </summary>
        public string BuildDirectoryPath { get; set; } = string.Empty;

        /// <summary>
        /// Last selected .zip file path (for convenience)
        /// </summary>
        public string LastZipPath { get; set; } = string.Empty;

        /// <summary>
        /// Load settings from disk. Returns default settings if file doesn't exist.
        /// </summary>
        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<AppSettings>(json, options) ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppSettings] Failed to load: {ex.Message}");
            }
            return new AppSettings();
        }

        /// <summary>
        /// Save current settings to disk.
        /// </summary>
        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(this, options);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppSettings] Failed to save: {ex.Message}");
            }
        }
    }
}
