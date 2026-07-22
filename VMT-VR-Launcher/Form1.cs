using System.Diagnostics;
using VMT_VR_Launcher.Models;

namespace VMT_VR_Launcher
{
    public partial class Form1 : AntdUI.Window
    {
        // ─── State ───────────────────────────────────────────────────
        private AppSettings _settings = null!;
        private BuildInfo? _buildInfo;
        private NetworkData? _networkData;
        private CancellationTokenSource? _updateCts;
        private bool _isUpdating = false;

        // ═══════════════════════════════════════════════════════════════
        //  INITIALIZATION
        // ═══════════════════════════════════════════════════════════════

        public Form1()
        {
            AntdUI.Config.IsDark = true;
            InitializeComponent();
            try { this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch { }
            LoadSettings();
            WireEvents();
            RefreshUI();
        }

        /// <summary>
        /// Load saved settings and populate path fields.
        /// </summary>
        private void LoadSettings()
        {
            _settings = AppSettings.Load();
            txtBuildPath.Text = _settings.BuildDirectoryPath;

            if (!string.IsNullOrWhiteSpace(_settings.LastZipPath) && File.Exists(_settings.LastZipPath))
            {
                txtZipPath.Text = _settings.LastZipPath;
            }
        }

        /// <summary>
        /// Wire up all event handlers.
        /// </summary>
        private void WireEvents()
        {
            // Browse buttons
            btnBrowseBuild.Click += BtnBrowseBuild_Click;
            btnBrowseZip.Click += BtnBrowseZip_Click;

            // Action buttons
            btnUpdate.Click += BtnUpdate_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnOpenFolder.Click += BtnOpenFolder_Click;
            btnLaunchApp.Click += BtnLaunchApp_Click;
            btnApplyNetwork.Click += BtnApplyNetwork_Click;
            btnOpenNetworkJson.Click += BtnOpenNetworkJson_Click;

            // Form closing — save settings
            FormClosing += Form1_FormClosing;

            // Update button state when zip path changes
            txtZipPath.TextChanged += (_, _) => UpdateButtonStates();
            txtBuildPath.TextChanged += (_, _) =>
            {
                _settings.BuildDirectoryPath = txtBuildPath.Text;
                _settings.Save();
                RefreshUI();
            };
        }

        // ═══════════════════════════════════════════════════════════════
        //  PATH BROWSING
        // ═══════════════════════════════════════════════════════════════

        private void BtnBrowseBuild_Click(object? sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select Unity Build Directory",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false,
            };

            if (!string.IsNullOrWhiteSpace(txtBuildPath.Text) && Directory.Exists(txtBuildPath.Text))
                dialog.InitialDirectory = txtBuildPath.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBuildPath.Text = dialog.SelectedPath;
            }
        }

        private void BtnBrowseZip_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Select Update Package (.zip/.rar/.7z)",
                Filter = "Archive files (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z|All files (*.*)|*.*",
                FilterIndex = 1,
            };

            if (!string.IsNullOrWhiteSpace(_settings.LastZipPath))
            {
                string? dir = Path.GetDirectoryName(_settings.LastZipPath);
                if (dir != null && Directory.Exists(dir))
                    dialog.InitialDirectory = dir;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtZipPath.Text = dialog.FileName;
                _settings.LastZipPath = dialog.FileName;
                _settings.Save();
            }
        }

        // ═══════════════════════════════════════════════════════════════
        //  UPDATE PROCESS
        // ═══════════════════════════════════════════════════════════════

        private async void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_isUpdating) return;

            string zipPath = txtZipPath.Text.Trim();
            string buildPath = txtBuildPath.Text.Trim();

            // Validations
            if (string.IsNullOrWhiteSpace(buildPath) || !Directory.Exists(buildPath))
            {
                ShowWarning("Please set a valid Build Directory first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(zipPath) || !File.Exists(zipPath))
            {
                ShowWarning("Please select a valid .zip file.");
                return;
            }

            // Check if game is running
            if (UpdateService.IsGameRunning(buildPath))
            {
                ShowWarning("The game is currently running!\nPlease close it before updating.");
                return;
            }

            // Confirmation
            var result = MessageBox.Show(
                "Are you sure you want to update the build?\n\n" +
                "• StreamingAssets data (DataSave, configs, etc.) will be preserved\n" +
                "• BuildInfo.json will be updated to the new version\n" +
                "• All other files will be overwritten",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // Start update
            _isUpdating = true;
            SetUpdatingUI(true);

            _updateCts = new CancellationTokenSource();
            var progress = new Progress<UpdateProgress>(p =>
            {
                progressBar.Value = p.Percentage / 100f;
                lblStatus.Text = p.Status;
            });

            try
            {
                await UpdateService.PerformUpdateAsync(zipPath, buildPath, progress, _updateCts.Token);

                ShowSuccess("Build updated successfully! ✓");
                RefreshUI();
            }
            catch (OperationCanceledException)
            {
                ShowInfo("Update was cancelled.");
            }
            catch (Exception ex)
            {
                ShowError($"Update failed:\n{ex.Message}");
                Debug.WriteLine($"[Form1] Update error: {ex}");
            }
            finally
            {
                _isUpdating = false;
                _updateCts?.Dispose();
                _updateCts = null;
                SetUpdatingUI(false);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        //  REFRESH & UI UPDATE
        // ═══════════════════════════════════════════════════════════════

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshUI();
            // Flash effect to indicate refresh happened
            lblStatus.Visible = true;
            lblStatus.Text = "✓  Data refreshed";
            lblStatus.ForeColor = Color.LimeGreen;

            var timer = new System.Windows.Forms.Timer { Interval = 2000 };
            timer.Tick += (_, _) =>
            {
                lblStatus.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        /// <summary>
        /// Refreshes all displayed information from the build directory.
        /// </summary>
        private void RefreshUI()
        {
            string buildPath = txtBuildPath.Text.Trim();

            // Load Build Info
            _buildInfo = UpdateService.LoadBuildInfo(buildPath);
            if (_buildInfo != null)
            {
                lblGameNameValue.Text = _buildInfo.GameName;
                lblVersionValue.Text = $"v{_buildInfo.Version}";
                lblBuildDateValue.Text = _buildInfo.BuildDate;
                windowBar.Text = $"VR Launcher - {_buildInfo.GameName}";
            }
            else
            {
                lblGameNameValue.Text = "—";
                lblVersionValue.Text = "—";
                lblBuildDateValue.Text = "—";
                windowBar.Text = "VR Launcher";
            }

            // Load Network Data
            _networkData = UpdateService.LoadNetworkData(buildPath);
            if (_networkData != null)
            {
                txtDbUrl.Text = _networkData.DatabaseURL;
                txtServer.Text = _networkData.Server;
                txtPort.Text = _networkData.Port;
                swDebug.Checked = _networkData.IsDebug;
                swApi.Checked = _networkData.IsAPI;
            }
            else
            {
                txtDbUrl.Text = string.Empty;
                txtServer.Text = string.Empty;
                txtPort.Text = string.Empty;
                swDebug.Checked = false;
                swApi.Checked = false;
            }

            // Load Custom Weapons (.vmt) Count
            try
            {
                string? saPath = UpdateService.FindStreamingAssetsPath(buildPath);
                if (!string.IsNullOrEmpty(saPath))
                {
                    string dataSave = Path.Combine(saPath, "DataSave");
                    if (Directory.Exists(dataSave))
                    {
                        int vmtCount = Directory.GetFiles(dataSave, "*.vmt", SearchOption.AllDirectories).Length;
                        lblVmtCountValue.Text = $"{vmtCount} Files";
                    }
                    else
                    {
                        lblVmtCountValue.Text = "0 Files";
                    }
                }
                else
                {
                    lblVmtCountValue.Text = "—";
                }
            }
            catch
            {
                lblVmtCountValue.Text = "Error";
            }

            UpdateButtonStates();
        }

        /// <summary>
        /// Updates button enabled/disabled states based on current configuration.
        /// </summary>
        private void UpdateButtonStates()
        {
            string buildPath = txtBuildPath.Text.Trim();
            string zipPath = txtZipPath.Text.Trim();

            bool hasBuild = !string.IsNullOrWhiteSpace(buildPath) && Directory.Exists(buildPath);
            bool hasZip = !string.IsNullOrWhiteSpace(zipPath) && File.Exists(zipPath);

            btnUpdate.Enabled = hasBuild && hasZip && !_isUpdating;
            btnOpenFolder.Enabled = hasBuild;
            btnLaunchApp.Enabled = hasBuild && UpdateService.FindGameExecutable(buildPath) != null;
            
            // Network edit form
            btnApplyNetwork.Enabled = hasBuild;
            btnOpenNetworkJson.Enabled = hasBuild;
            txtDbUrl.Enabled = hasBuild;
            txtServer.Enabled = hasBuild;
            txtPort.Enabled = hasBuild;
            swDebug.Enabled = hasBuild;
            swApi.Enabled = hasBuild;
        }

        // ═══════════════════════════════════════════════════════════════
        //  ACTION BUTTONS
        // ═══════════════════════════════════════════════════════════════

        private void BtnApplyNetwork_Click(object? sender, EventArgs e)
        {
            string buildPath = txtBuildPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(buildPath) || !Directory.Exists(buildPath)) return;

            var newData = new NetworkData
            {
                DatabaseURL = txtDbUrl.Text,
                Server = txtServer.Text,
                Port = txtPort.Text,
                IsDebug = swDebug.Checked,
                IsAPI = swApi.Checked
            };

            try
            {
                UpdateService.SaveNetworkData(buildPath, newData);
                ShowSuccess("Network Data saved successfully!");
                RefreshUI();
            }
            catch (Exception ex)
            {
                ShowError($"Failed to save Network Data:\n{ex.Message}");
            }
        }

        private void BtnOpenNetworkJson_Click(object? sender, EventArgs e)
        {
            string buildPath = txtBuildPath.Text.Trim();
            string? saPath = UpdateService.FindStreamingAssetsPath(buildPath);
            if (saPath != null)
            {
                string jsonPath = Path.Combine(saPath, "NetworkData.json");
                if (File.Exists(jsonPath))
                {
                    try
                    {
                        Process.Start("notepad.exe", $"\"{jsonPath}\"");
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Failed to open NetworkData.json:\n{ex.Message}");
                    }
                }
                else
                {
                    ShowError("NetworkData.json not found in StreamingAssets.");
                }
            }
        }

        private void BtnOpenFolder_Click(object? sender, EventArgs e)
        {
            string buildPath = txtBuildPath.Text.Trim();
            if (Directory.Exists(buildPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = buildPath,
                    UseShellExecute = true,
                });
            }
        }

        private void BtnLaunchApp_Click(object? sender, EventArgs e)
        {
            string buildPath = txtBuildPath.Text.Trim();
            string? exePath = UpdateService.FindGameExecutable(buildPath);

            if (exePath != null && File.Exists(exePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = exePath,
                        WorkingDirectory = buildPath,
                        UseShellExecute = true,
                    });
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to launch app:\n{ex.Message}");
                }
            }
            else
            {
                ShowWarning("Game executable not found in the build directory.");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        //  UI HELPERS
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Toggles the UI between normal and "updating" mode.
        /// </summary>
        private void SetUpdatingUI(bool updating)
        {
            progressBar.Visible = updating;
            lblStatus.Visible = updating;
            progressBar.Value = 0;

            // Disable controls during update
            btnBrowseBuild.Enabled = !updating;
            btnBrowseZip.Enabled = !updating;
            btnUpdate.Enabled = !updating;
            btnRefresh.Enabled = !updating;
            btnLaunchApp.Enabled = !updating;

            if (updating)
            {
                btnUpdate.Text = "UPDATING...";
            }
            else
            {
                btnUpdate.Text = "UPDATE BUILD";
                UpdateButtonStates();
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isUpdating)
            {
                var result = MessageBox.Show(
                    "An update is in progress. Are you sure you want to cancel and close?",
                    "Update in Progress",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                _updateCts?.Cancel();
            }

            _settings.BuildDirectoryPath = txtBuildPath.Text;
            _settings.Save();
        }

        // ─── Message Dialogs ─────────────────────────────────────────

        private static void ShowWarning(string message) =>
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static void ShowSuccess(string message) =>
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private static void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
