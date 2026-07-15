namespace VMT_VR_Launcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            windowBar = new AntdUI.WindowBar();
            btnBrowseBuild = new AntdUI.Button();
            txtBuildPath = new AntdUI.Input();
            lblBuildPathLabel = new AntdUI.Label();
            btnUpdate = new AntdUI.Button();
            btnBrowseZip = new AntdUI.Button();
            txtZipPath = new AntdUI.Input();
            lblZipPathLabel = new AntdUI.Label();
            lblBuildDateValue = new AntdUI.Label();
            lblVmtCountValue = new AntdUI.Label();
            lblVmtCountLabel = new AntdUI.Label();
            lblVersionValue = new AntdUI.Label();
            lblGameNameValue = new AntdUI.Label();
            lblBuildDateLabel = new AntdUI.Label();
            lblVersionLabel = new AntdUI.Label();
            lblGameNameLabel = new AntdUI.Label();
            lblBuildInfoTitle = new AntdUI.Label();
            lblApiValue = new AntdUI.Label();
            lblDebugValue = new AntdUI.Label();
            lblPortValue = new AntdUI.Label();
            lblServerValue = new AntdUI.Label();
            lblDbUrlValue = new AntdUI.Label();
            lblApiLabel = new AntdUI.Label();
            lblDebugLabel = new AntdUI.Label();
            lblPortLabel = new AntdUI.Label();
            lblServerLabel = new AntdUI.Label();
            lblDbUrlLabel = new AntdUI.Label();
            lblNetworkTitle = new AntdUI.Label();
            progressBar = new AntdUI.Progress();
            lblStatus = new AntdUI.Label();
            btnRefresh = new AntdUI.Button();
            btnOpenFolder = new AntdUI.Button();
            btnLaunchApp = new AntdUI.Button();
            panelConfig = new AntdUI.Panel();
            panelUpdate = new AntdUI.Panel();
            panelInfo = new AntdUI.Panel();
            panelNetwork = new AntdUI.Panel();
            panelConfig.SuspendLayout();
            panelUpdate.SuspendLayout();
            panelInfo.SuspendLayout();
            panelNetwork.SuspendLayout();
            SuspendLayout();
            // 
            // windowBar
            // 
            windowBar.Dock = DockStyle.Top;
            windowBar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            windowBar.Icon = Properties.Resources.app;
            windowBar.Location = new Point(0, 0);
            windowBar.MaximizeBox = false;
            windowBar.Name = "windowBar";
            windowBar.Size = new Size(884, 40);
            windowBar.TabIndex = 0;
            windowBar.Text = "VR Launcher";
            // 
            // btnBrowseBuild
            // 
            btnBrowseBuild.Location = new Point(324, 46);
            btnBrowseBuild.Name = "btnBrowseBuild";
            btnBrowseBuild.Size = new Size(80, 36);
            btnBrowseBuild.TabIndex = 2;
            btnBrowseBuild.Text = "Browse";
            btnBrowseBuild.Type = AntdUI.TTypeMini.Primary;
            // 
            // txtBuildPath
            // 
            txtBuildPath.Location = new Point(16, 46);
            txtBuildPath.Name = "txtBuildPath";
            txtBuildPath.ReadOnly = true;
            txtBuildPath.Size = new Size(300, 36);
            txtBuildPath.TabIndex = 1;
            // 
            // lblBuildPathLabel
            // 
            lblBuildPathLabel.Location = new Point(16, 16);
            lblBuildPathLabel.Name = "lblBuildPathLabel";
            lblBuildPathLabel.Size = new Size(120, 24);
            lblBuildPathLabel.TabIndex = 0;
            lblBuildPathLabel.Text = "Build Directory";
            // 
            // btnUpdate
            // 
            btnUpdate.Enabled = false;
            btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdate.Location = new Point(16, 113);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(388, 36);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "UPDATE BUILD";
            btnUpdate.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnBrowseZip
            // 
            btnBrowseZip.Location = new Point(324, 46);
            btnBrowseZip.Name = "btnBrowseZip";
            btnBrowseZip.Size = new Size(80, 36);
            btnBrowseZip.TabIndex = 2;
            btnBrowseZip.Text = "Browse";
            btnBrowseZip.Type = AntdUI.TTypeMini.Primary;
            // 
            // txtZipPath
            // 
            txtZipPath.Location = new Point(16, 46);
            txtZipPath.Name = "txtZipPath";
            txtZipPath.ReadOnly = true;
            txtZipPath.Size = new Size(300, 36);
            txtZipPath.TabIndex = 1;
            // 
            // lblZipPathLabel
            // 
            lblZipPathLabel.Location = new Point(16, 16);
            lblZipPathLabel.Name = "lblZipPathLabel";
            lblZipPathLabel.Size = new Size(200, 24);
            lblZipPathLabel.TabIndex = 0;
            lblZipPathLabel.Text = "Select Update Package (.zip/.rar/.7z)";
            // 
            // lblBuildDateValue
            // 
            lblBuildDateValue.Location = new Point(100, 92);
            lblBuildDateValue.Name = "lblBuildDateValue";
            lblBuildDateValue.Size = new Size(280, 20);
            lblBuildDateValue.TabIndex = 5;
            lblBuildDateValue.Text = "—";
            // 
            // lblVmtCountValue
            // 
            lblVmtCountValue.Location = new Point(100, 116);
            lblVmtCountValue.Name = "lblVmtCountValue";
            lblVmtCountValue.Size = new Size(280, 20);
            lblVmtCountValue.TabIndex = 7;
            lblVmtCountValue.Text = "—";
            // 
            // lblVmtCountLabel
            // 
            lblVmtCountLabel.Location = new Point(16, 116);
            lblVmtCountLabel.Name = "lblVmtCountLabel";
            lblVmtCountLabel.Size = new Size(80, 20);
            lblVmtCountLabel.TabIndex = 6;
            lblVmtCountLabel.Text = "VMT Files";
            // 
            // lblVersionValue
            // 
            lblVersionValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVersionValue.Fore = Color.LimeGreen;
            lblVersionValue.ForeColor = Color.LimeGreen;
            lblVersionValue.Location = new Point(100, 68);
            lblVersionValue.Name = "lblVersionValue";
            lblVersionValue.Size = new Size(280, 20);
            lblVersionValue.TabIndex = 3;
            lblVersionValue.Text = "—";
            // 
            // lblGameNameValue
            // 
            lblGameNameValue.Location = new Point(100, 44);
            lblGameNameValue.Name = "lblGameNameValue";
            lblGameNameValue.Size = new Size(280, 20);
            lblGameNameValue.TabIndex = 1;
            lblGameNameValue.Text = "—";
            // 
            // lblBuildDateLabel
            // 
            lblBuildDateLabel.Location = new Point(16, 92);
            lblBuildDateLabel.Name = "lblBuildDateLabel";
            lblBuildDateLabel.Size = new Size(80, 20);
            lblBuildDateLabel.TabIndex = 4;
            lblBuildDateLabel.Text = "Build Date";
            // 
            // lblVersionLabel
            // 
            lblVersionLabel.Location = new Point(16, 68);
            lblVersionLabel.Name = "lblVersionLabel";
            lblVersionLabel.Size = new Size(80, 20);
            lblVersionLabel.TabIndex = 2;
            lblVersionLabel.Text = "Version";
            // 
            // lblGameNameLabel
            // 
            lblGameNameLabel.Location = new Point(16, 44);
            lblGameNameLabel.Name = "lblGameNameLabel";
            lblGameNameLabel.Size = new Size(80, 20);
            lblGameNameLabel.TabIndex = 0;
            lblGameNameLabel.Text = "Game Name";
            // 
            // lblBuildInfoTitle
            // 
            lblBuildInfoTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBuildInfoTitle.Location = new Point(16, 12);
            lblBuildInfoTitle.Name = "lblBuildInfoTitle";
            lblBuildInfoTitle.Size = new Size(200, 24);
            lblBuildInfoTitle.TabIndex = 6;
            lblBuildInfoTitle.Text = "BUILD INFORMATION";
            // 
            // lblApiValue
            // 
            lblApiValue.Location = new Point(100, 140);
            lblApiValue.Name = "lblApiValue";
            lblApiValue.Size = new Size(280, 20);
            lblApiValue.TabIndex = 9;
            lblApiValue.Text = "—";
            // 
            // lblDebugValue
            // 
            lblDebugValue.Location = new Point(100, 116);
            lblDebugValue.Name = "lblDebugValue";
            lblDebugValue.Size = new Size(280, 20);
            lblDebugValue.TabIndex = 8;
            lblDebugValue.Text = "—";
            // 
            // lblPortValue
            // 
            lblPortValue.Location = new Point(100, 92);
            lblPortValue.Name = "lblPortValue";
            lblPortValue.Size = new Size(280, 20);
            lblPortValue.TabIndex = 7;
            lblPortValue.Text = "—";
            // 
            // lblServerValue
            // 
            lblServerValue.Location = new Point(100, 68);
            lblServerValue.Name = "lblServerValue";
            lblServerValue.Size = new Size(280, 20);
            lblServerValue.TabIndex = 6;
            lblServerValue.Text = "—";
            // 
            // lblDbUrlValue
            // 
            lblDbUrlValue.Location = new Point(100, 44);
            lblDbUrlValue.Name = "lblDbUrlValue";
            lblDbUrlValue.Size = new Size(280, 20);
            lblDbUrlValue.TabIndex = 5;
            lblDbUrlValue.Text = "—";
            // 
            // lblApiLabel
            // 
            lblApiLabel.Location = new Point(16, 140);
            lblApiLabel.Name = "lblApiLabel";
            lblApiLabel.Size = new Size(80, 20);
            lblApiLabel.TabIndex = 4;
            lblApiLabel.Text = "API Mode";
            // 
            // lblDebugLabel
            // 
            lblDebugLabel.Location = new Point(16, 116);
            lblDebugLabel.Name = "lblDebugLabel";
            lblDebugLabel.Size = new Size(80, 20);
            lblDebugLabel.TabIndex = 3;
            lblDebugLabel.Text = "Debug";
            // 
            // lblPortLabel
            // 
            lblPortLabel.Location = new Point(16, 92);
            lblPortLabel.Name = "lblPortLabel";
            lblPortLabel.Size = new Size(80, 20);
            lblPortLabel.TabIndex = 2;
            lblPortLabel.Text = "Port";
            // 
            // lblServerLabel
            // 
            lblServerLabel.Location = new Point(16, 68);
            lblServerLabel.Name = "lblServerLabel";
            lblServerLabel.Size = new Size(80, 20);
            lblServerLabel.TabIndex = 1;
            lblServerLabel.Text = "Server";
            // 
            // lblDbUrlLabel
            // 
            lblDbUrlLabel.Location = new Point(16, 44);
            lblDbUrlLabel.Name = "lblDbUrlLabel";
            lblDbUrlLabel.Size = new Size(80, 20);
            lblDbUrlLabel.TabIndex = 0;
            lblDbUrlLabel.Text = "DB URL";
            // 
            // lblNetworkTitle
            // 
            lblNetworkTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNetworkTitle.Location = new Point(16, 12);
            lblNetworkTitle.Name = "lblNetworkTitle";
            lblNetworkTitle.Size = new Size(200, 24);
            lblNetworkTitle.TabIndex = 10;
            lblNetworkTitle.Text = "NETWORK TELEMETRY";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(24, 425);
            progressBar.Name = "progressBar";
            progressBar.Radius = 8;
            progressBar.Size = new Size(836, 16);
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(24, 447);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(300, 20);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Status";
            lblStatus.Visible = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(24, 471);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "SYNC DATA";
            btnRefresh.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Location = new Point(152, 471);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(140, 36);
            btnOpenFolder.TabIndex = 9;
            btnOpenFolder.Text = "OPEN DIRECTORY";
            btnOpenFolder.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnLaunchApp
            // 
            btnLaunchApp.Enabled = false;
            btnLaunchApp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLaunchApp.Location = new Point(710, 467);
            btnLaunchApp.Name = "btnLaunchApp";
            btnLaunchApp.Size = new Size(150, 44);
            btnLaunchApp.TabIndex = 10;
            btnLaunchApp.Text = "INITIALIZE APP";
            btnLaunchApp.Type = AntdUI.TTypeMini.Primary;
            // 
            // panelConfig
            // 
            panelConfig.BorderWidth = 1F;
            panelConfig.Controls.Add(btnBrowseBuild);
            panelConfig.Controls.Add(txtBuildPath);
            panelConfig.Controls.Add(lblBuildPathLabel);
            panelConfig.Location = new Point(24, 60);
            panelConfig.Name = "panelConfig";
            panelConfig.Radius = 8;
            panelConfig.Size = new Size(420, 165);
            panelConfig.TabIndex = 2;
            // 
            // panelUpdate
            // 
            panelUpdate.BorderWidth = 1F;
            panelUpdate.Controls.Add(btnUpdate);
            panelUpdate.Controls.Add(btnBrowseZip);
            panelUpdate.Controls.Add(txtZipPath);
            panelUpdate.Controls.Add(lblZipPathLabel);
            panelUpdate.Location = new Point(24, 240);
            panelUpdate.Name = "panelUpdate";
            panelUpdate.Radius = 8;
            panelUpdate.Size = new Size(420, 165);
            panelUpdate.TabIndex = 3;
            // 
            // panelInfo
            // 
            panelInfo.BorderWidth = 1F;
            panelInfo.Controls.Add(lblVmtCountValue);
            panelInfo.Controls.Add(lblVmtCountLabel);
            panelInfo.Controls.Add(lblBuildDateValue);
            panelInfo.Controls.Add(lblVersionValue);
            panelInfo.Controls.Add(lblGameNameValue);
            panelInfo.Controls.Add(lblBuildDateLabel);
            panelInfo.Controls.Add(lblVersionLabel);
            panelInfo.Controls.Add(lblGameNameLabel);
            panelInfo.Controls.Add(lblBuildInfoTitle);
            panelInfo.Location = new Point(460, 60);
            panelInfo.Name = "panelInfo";
            panelInfo.Radius = 8;
            panelInfo.Size = new Size(400, 165);
            panelInfo.TabIndex = 4;
            // 
            // panelNetwork
            // 
            panelNetwork.BorderWidth = 1F;
            panelNetwork.Controls.Add(lblApiValue);
            panelNetwork.Controls.Add(lblDebugValue);
            panelNetwork.Controls.Add(lblPortValue);
            panelNetwork.Controls.Add(lblServerValue);
            panelNetwork.Controls.Add(lblDbUrlValue);
            panelNetwork.Controls.Add(lblApiLabel);
            panelNetwork.Controls.Add(lblDebugLabel);
            panelNetwork.Controls.Add(lblPortLabel);
            panelNetwork.Controls.Add(lblServerLabel);
            panelNetwork.Controls.Add(lblDbUrlLabel);
            panelNetwork.Controls.Add(lblNetworkTitle);
            panelNetwork.Location = new Point(460, 240);
            panelNetwork.Name = "panelNetwork";
            panelNetwork.Radius = 8;
            panelNetwork.Size = new Size(400, 165);
            panelNetwork.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(884, 540);
            Controls.Add(btnLaunchApp);
            Controls.Add(btnOpenFolder);
            Controls.Add(btnRefresh);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(panelNetwork);
            Controls.Add(panelInfo);
            Controls.Add(panelUpdate);
            Controls.Add(panelConfig);
            Controls.Add(windowBar);
            MaximizeBox = false;
            Resizable = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VR Launcher";
            panelConfig.ResumeLayout(false);
            panelUpdate.ResumeLayout(false);
            panelInfo.ResumeLayout(false);
            panelNetwork.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.WindowBar windowBar;
        private AntdUI.Panel panelConfig;
        private AntdUI.Button btnBrowseBuild;
        private AntdUI.Input txtBuildPath;
        private AntdUI.Label lblBuildPathLabel;
        private AntdUI.Panel panelUpdate;
        private AntdUI.Button btnUpdate;
        private AntdUI.Button btnBrowseZip;
        private AntdUI.Input txtZipPath;
        private AntdUI.Label lblZipPathLabel;
        private AntdUI.Panel panelInfo;
        private AntdUI.Label lblBuildInfoTitle;
        private AntdUI.Label lblBuildDateValue;
        private AntdUI.Label lblVersionValue;
        private AntdUI.Label lblGameNameValue;
        private AntdUI.Label lblBuildDateLabel;
        private AntdUI.Label lblVersionLabel;
        private AntdUI.Label lblGameNameLabel;
        private AntdUI.Label lblVmtCountValue;
        private AntdUI.Label lblVmtCountLabel;
        private AntdUI.Panel panelNetwork;
        private AntdUI.Label lblNetworkTitle;
        private AntdUI.Label lblApiValue;
        private AntdUI.Label lblDebugValue;
        private AntdUI.Label lblPortValue;
        private AntdUI.Label lblServerValue;
        private AntdUI.Label lblDbUrlValue;
        private AntdUI.Label lblApiLabel;
        private AntdUI.Label lblDebugLabel;
        private AntdUI.Label lblPortLabel;
        private AntdUI.Label lblServerLabel;
        private AntdUI.Label lblDbUrlLabel;
        private AntdUI.Progress progressBar;
        private AntdUI.Label lblStatus;
        private AntdUI.Button btnRefresh;
        private AntdUI.Button btnOpenFolder;
        private AntdUI.Button btnLaunchApp;
    }
}
