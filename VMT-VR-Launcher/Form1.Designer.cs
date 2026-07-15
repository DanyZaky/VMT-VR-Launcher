#nullable enable
using System.Drawing.Drawing2D;
using VMT_VR_Launcher.Controls;

namespace VMT_VR_Launcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // ─── Header ──────────────────────────────────────────────────
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblGameName = null!;

        // ─── Left Column: Configuration & Update ─────────────────────
        private ThemedPanel pnlConfig = null!;
        private Label lblConfigTitle = null!;
        private Label lblBuildPathLabel = null!;
        private TextBox txtBuildPath = null!;
        private ThemedButton btnBrowseBuild = null!;

        private ThemedPanel pnlUpdate = null!;
        private Label lblUpdateTitle = null!;
        private Label lblZipPathLabel = null!;
        private TextBox txtZipPath = null!;
        private ThemedButton btnBrowseZip = null!;
        private ThemedButton btnUpdate = null!;

        // ─── Right Column: Build Info & Network Data ─────────────────
        private ThemedPanel pnlBuildInfo = null!;
        private Label lblBuildInfoTitle = null!;
        private Label lblGameNameLabel = null!;
        private Label lblGameNameValue = null!;
        private Label lblVersionLabel = null!;
        private Label lblVersionValue = null!;
        private Label lblBuildDateLabel = null!;
        private Label lblBuildDateValue = null!;

        private ThemedPanel pnlNetworkData = null!;
        private Label lblNetworkTitle = null!;
        private Label lblDbUrlLabel = null!;
        private Label lblDbUrlValue = null!;
        private Label lblServerLabel = null!;
        private Label lblServerValue = null!;
        private Label lblPortLabel = null!;
        private Label lblPortValue = null!;
        private Label lblDebugLabel = null!;
        private Label lblDebugValue = null!;
        private Label lblApiLabel = null!;
        private Label lblApiValue = null!;

        // ─── Bottom: Progress & Actions ──────────────────────────────
        private ThemedProgressBar progressBar = null!;
        private Label lblStatus = null!;
        private Panel pnlActions = null!;
        private ThemedButton btnRefresh = null!;
        private ThemedButton btnOpenFolder = null!;
        private ThemedButton btnLaunchApp = null!;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();

            // ═══════════════════════════════════════════════════════════
            //  FORM SETTINGS
            // ═══════════════════════════════════════════════════════════
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(900, 620);
            Text = "VMT VR Launcher";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = ThemeColors.BackgroundDark;
            ForeColor = ThemeColors.TextPrimary;
            DoubleBuffered = true;
            Font = ThemeColors.FontBody;

            int margin = 16;
            int leftColX = margin;
            int leftColWidth = 426;
            int rightColX = leftColX + leftColWidth + margin;
            int rightColWidth = 426;

            // ═══════════════════════════════════════════════════════════
            //  HEADER
            // ═══════════════════════════════════════════════════════════
            pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(900, 70),
                BackColor = ThemeColors.BackgroundMain,
            };

            lblTitle = new Label
            {
                Text = "🚀  VMT VR Launcher",
                Font = ThemeColors.FontTitle,
                ForeColor = ThemeColors.TextHighlight,
                AutoSize = true,
                Location = new Point(margin + 4, 12),
                BackColor = Color.Transparent,
            };

            lblSubtitle = new Label
            {
                Text = "Unity Build Version Manager",
                Font = ThemeColors.FontSubtitle,
                ForeColor = ThemeColors.TextMuted,
                AutoSize = true,
                Location = new Point(margin + 8, 44),
                BackColor = Color.Transparent,
            };

            lblGameName = new Label
            {
                Text = "",
                Font = ThemeColors.FontVersion,
                ForeColor = ThemeColors.AccentBlue,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(650, 22),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
            };

            pnlHeader.Controls.AddRange([lblTitle, lblSubtitle, lblGameName]);

            // ═══════════════════════════════════════════════════════════
            //  LEFT COLUMN — BUILD CONFIGURATION
            // ═══════════════════════════════════════════════════════════
            int leftY = pnlHeader.Bottom + margin;

            pnlConfig = new ThemedPanel
            {
                Location = new Point(leftColX, leftY),
                Size = new Size(leftColWidth, 120),
            };

            lblConfigTitle = new Label
            {
                Text = "📁  BUILD CONFIGURATION",
                Font = ThemeColors.FontSectionTitle,
                ForeColor = ThemeColors.AccentBlue,
                AutoSize = true,
                Location = new Point(16, 14),
                BackColor = Color.Transparent,
            };

            lblBuildPathLabel = new Label
            {
                Text = "Build Directory",
                Font = ThemeColors.FontSmall,
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(16, 48),
                BackColor = Color.Transparent,
            };

            txtBuildPath = new TextBox
            {
                Location = new Point(16, 70),
                Size = new Size(leftColWidth - 116, 28),
                BackColor = ThemeColors.BackgroundInput,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                Font = ThemeColors.FontBody,
                ReadOnly = true,
            };

            btnBrowseBuild = new ThemedButton
            {
                Text = "Browse",
                Icon = "📂",
                Style = ThemedButton.ButtonStyle.Secondary,
                Location = new Point(leftColWidth - 94, 68),
                Size = new Size(78, 32),
            };

            pnlConfig.Controls.AddRange([lblConfigTitle, lblBuildPathLabel, txtBuildPath, btnBrowseBuild]);

            // ═══════════════════════════════════════════════════════════
            //  LEFT COLUMN — UPDATE BUILD
            // ═══════════════════════════════════════════════════════════
            int updateY = pnlConfig.Bottom + margin;

            pnlUpdate = new ThemedPanel
            {
                Location = new Point(leftColX, updateY),
                Size = new Size(leftColWidth, 200),
            };

            lblUpdateTitle = new Label
            {
                Text = "📦  UPDATE BUILD",
                Font = ThemeColors.FontSectionTitle,
                ForeColor = ThemeColors.AccentBlue,
                AutoSize = true,
                Location = new Point(16, 14),
                BackColor = Color.Transparent,
            };

            lblZipPathLabel = new Label
            {
                Text = "Select Update Package (.zip)",
                Font = ThemeColors.FontSmall,
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(16, 48),
                BackColor = Color.Transparent,
            };

            txtZipPath = new TextBox
            {
                Location = new Point(16, 70),
                Size = new Size(leftColWidth - 116, 28),
                BackColor = ThemeColors.BackgroundInput,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                Font = ThemeColors.FontBody,
                ReadOnly = true,
            };

            btnBrowseZip = new ThemedButton
            {
                Text = "Browse",
                Icon = "📄",
                Style = ThemedButton.ButtonStyle.Secondary,
                Location = new Point(leftColWidth - 94, 68),
                Size = new Size(78, 32),
            };

            btnUpdate = new ThemedButton
            {
                Text = "UPDATE BUILD",
                Icon = "⬆",
                Style = ThemedButton.ButtonStyle.Primary,
                Location = new Point(16, 116),
                Size = new Size(leftColWidth - 32, 40),
                Enabled = false,
            };

            // Info label about preservation
            var lblPreserveInfo = new Label
            {
                Text = "ℹ  StreamingAssets data (DataSave, configs) will be preserved",
                Font = ThemeColors.FontSmall,
                ForeColor = ThemeColors.TextMuted,
                AutoSize = true,
                Location = new Point(16, 166),
                BackColor = Color.Transparent,
            };

            pnlUpdate.Controls.AddRange([lblUpdateTitle, lblZipPathLabel, txtZipPath, btnBrowseZip, btnUpdate, lblPreserveInfo]);

            // ═══════════════════════════════════════════════════════════
            //  RIGHT COLUMN — BUILD INFORMATION
            // ═══════════════════════════════════════════════════════════

            pnlBuildInfo = new ThemedPanel
            {
                Location = new Point(rightColX, leftY),
                Size = new Size(rightColWidth, 140),
            };

            lblBuildInfoTitle = new Label
            {
                Text = "🔧  BUILD INFORMATION",
                Font = ThemeColors.FontSectionTitle,
                ForeColor = ThemeColors.AccentBlue,
                AutoSize = true,
                Location = new Point(16, 14),
                BackColor = Color.Transparent,
            };

            int infoRowY = 50;
            int infoRowH = 28;
            int labelW = 90;
            int valueX = labelW + 16;

            lblGameNameLabel = CreateInfoLabel("Game Name", 16, infoRowY);
            lblGameNameValue = CreateInfoValue("—", valueX, infoRowY);
            infoRowY += infoRowH;

            lblVersionLabel = CreateInfoLabel("Version", 16, infoRowY);
            lblVersionValue = CreateInfoValue("—", valueX, infoRowY);
            lblVersionValue.ForeColor = ThemeColors.AccentGreen;
            lblVersionValue.Font = ThemeColors.FontVersion;
            infoRowY += infoRowH;

            lblBuildDateLabel = CreateInfoLabel("Build Date", 16, infoRowY);
            lblBuildDateValue = CreateInfoValue("—", valueX, infoRowY);

            pnlBuildInfo.Controls.AddRange([
                lblBuildInfoTitle,
                lblGameNameLabel, lblGameNameValue,
                lblVersionLabel, lblVersionValue,
                lblBuildDateLabel, lblBuildDateValue
            ]);

            // ═══════════════════════════════════════════════════════════
            //  RIGHT COLUMN — NETWORK DATA
            // ═══════════════════════════════════════════════════════════
            int netY = pnlBuildInfo.Bottom + margin;

            pnlNetworkData = new ThemedPanel
            {
                Location = new Point(rightColX, netY),
                Size = new Size(rightColWidth, 180),
            };

            lblNetworkTitle = new Label
            {
                Text = "🌐  NETWORK DATA",
                Font = ThemeColors.FontSectionTitle,
                ForeColor = ThemeColors.AccentBlue,
                AutoSize = true,
                Location = new Point(16, 14),
                BackColor = Color.Transparent,
            };

            int netRowY = 46;
            int netRowH = 26;

            lblDbUrlLabel = CreateInfoLabel("Database URL", 16, netRowY);
            lblDbUrlValue = CreateInfoValue("—", valueX, netRowY);
            lblDbUrlValue.MaximumSize = new Size(rightColWidth - valueX - 16, 0);
            lblDbUrlValue.AutoEllipsis = true;
            netRowY += netRowH;

            lblServerLabel = CreateInfoLabel("Server", 16, netRowY);
            lblServerValue = CreateInfoValue("—", valueX, netRowY);
            netRowY += netRowH;

            lblPortLabel = CreateInfoLabel("Port", 16, netRowY);
            lblPortValue = CreateInfoValue("—", valueX, netRowY);
            netRowY += netRowH;

            lblDebugLabel = CreateInfoLabel("Debug Mode", 16, netRowY);
            lblDebugValue = CreateInfoValue("—", valueX, netRowY);
            netRowY += netRowH;

            lblApiLabel = CreateInfoLabel("API Mode", 16, netRowY);
            lblApiValue = CreateInfoValue("—", valueX, netRowY);

            pnlNetworkData.Controls.AddRange([
                lblNetworkTitle,
                lblDbUrlLabel, lblDbUrlValue,
                lblServerLabel, lblServerValue,
                lblPortLabel, lblPortValue,
                lblDebugLabel, lblDebugValue,
                lblApiLabel, lblApiValue
            ]);

            // ═══════════════════════════════════════════════════════════
            //  BOTTOM — PROGRESS BAR
            // ═══════════════════════════════════════════════════════════
            int progressY = 480;

            progressBar = new ThemedProgressBar
            {
                Location = new Point(margin, progressY),
                Size = new Size(900 - margin * 2, 28),
                Visible = false,
            };

            lblStatus = new Label
            {
                Text = "",
                Font = ThemeColors.FontSmall,
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(margin, progressY + 34),
                BackColor = Color.Transparent,
                Visible = false,
            };

            // ═══════════════════════════════════════════════════════════
            //  BOTTOM — ACTION BUTTONS
            // ═══════════════════════════════════════════════════════════
            int actionsY = 540;

            pnlActions = new Panel
            {
                Location = new Point(margin, actionsY),
                Size = new Size(900 - margin * 2, 50),
                BackColor = Color.Transparent,
            };

            btnRefresh = new ThemedButton
            {
                Text = "Refresh",
                Icon = "🔄",
                Style = ThemedButton.ButtonStyle.Secondary,
                Location = new Point(0, 4),
                Size = new Size(120, 40),
            };

            btnOpenFolder = new ThemedButton
            {
                Text = "Open Folder",
                Icon = "📂",
                Style = ThemedButton.ButtonStyle.Secondary,
                Location = new Point(130, 4),
                Size = new Size(140, 40),
            };

            btnLaunchApp = new ThemedButton
            {
                Text = "Launch App",
                Icon = "🚀",
                Style = ThemedButton.ButtonStyle.Success,
                Location = new Point(900 - margin * 2 - 180, 4),
                Size = new Size(180, 40),
            };

            pnlActions.Controls.AddRange([btnRefresh, btnOpenFolder, btnLaunchApp]);

            // ═══════════════════════════════════════════════════════════
            //  ADD ALL TO FORM
            // ═══════════════════════════════════════════════════════════
            Controls.AddRange([
                pnlHeader,
                pnlConfig,
                pnlUpdate,
                pnlBuildInfo,
                pnlNetworkData,
                progressBar,
                lblStatus,
                pnlActions
            ]);

            ResumeLayout(false);
            PerformLayout();
        }

        // ─── Helper: Create Info Row Labels ──────────────────────────

        private static Label CreateInfoLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = ThemeColors.FontSmall,
                ForeColor = ThemeColors.TextMuted,
                AutoSize = true,
                Location = new Point(x, y),
                BackColor = Color.Transparent,
            };
        }

        private static Label CreateInfoValue(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = ThemeColors.FontInfoValue,
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(x, y),
                BackColor = Color.Transparent,
            };
        }

        #endregion
    }
}
