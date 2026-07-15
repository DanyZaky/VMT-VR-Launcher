using System.Drawing.Drawing2D;

namespace VMT_VR_Launcher.Controls
{
    /// <summary>
    /// Custom progress bar with smooth fill, rounded corners, and percentage text.
    /// </summary>
    public class ThemedProgressBar : Control
    {
        private int _value = 0;
        private int _maximum = 100;
        private int _cornerRadius = 6;
        private string _statusText = string.Empty;

        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Clamp(value, 0, _maximum);
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set { _maximum = Math.Max(1, value); Invalidate(); }
        }

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; Invalidate(); }
        }

        public ThemedProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Height = 28;
            BackColor = ThemeColors.ProgressTrack;
            ForeColor = ThemeColors.TextPrimary;
            Font = ThemeColors.FontSmall;
        }

        private GraphicsPath GetRoundedRectPath(RectangleF rect, int radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2f;

            if (rect.Width < d) rect.Width = d;
            if (rect.Height < d) rect.Height = d;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var trackRect = new RectangleF(0, 0, Width - 1, Height - 1);

            // Track
            using (var trackPath = GetRoundedRectPath(trackRect, _cornerRadius))
            using (var trackBrush = new SolidBrush(ThemeColors.ProgressTrack))
            {
                g.FillPath(trackBrush, trackPath);
            }

            // Fill
            float progress = (float)_value / _maximum;
            if (progress > 0)
            {
                float fillWidth = Math.Max(_cornerRadius * 2, (Width - 1) * progress);
                var fillRect = new RectangleF(0, 0, fillWidth, Height - 1);

                using var fillPath = GetRoundedRectPath(fillRect, _cornerRadius);
                using var fillBrush = new LinearGradientBrush(
                    fillRect,
                    ThemeColors.AccentBlue,
                    Color.FromArgb(96, 165, 250), // Lighter blue
                    LinearGradientMode.Horizontal);
                g.FillPath(fillBrush, fillPath);
            }

            // Text
            string text = string.IsNullOrEmpty(_statusText)
                ? $"{(int)(progress * 100)}%"
                : $"{_statusText}  —  {(int)(progress * 100)}%";

            var textSize = g.MeasureString(text, Font);
            float x = (Width - textSize.Width) / 2;
            float y = (Height - textSize.Height) / 2;

            using (var textBrush = new SolidBrush(ThemeColors.TextHighlight))
            {
                g.DrawString(text, Font, textBrush, x, y);
            }
        }
    }
}
