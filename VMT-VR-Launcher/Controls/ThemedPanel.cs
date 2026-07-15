using System.Drawing.Drawing2D;

namespace VMT_VR_Launcher.Controls
{
    /// <summary>
    /// Custom Panel with rounded corners, border styling, and themed background.
    /// </summary>
    public class ThemedPanel : Panel
    {
        private int _cornerRadius = 10;
        private Color _borderColor = ThemeColors.Border;
        private int _borderWidth = 1;

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = value; Invalidate(); }
        }

        public ThemedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = ThemeColors.BackgroundCard;
            ForeColor = ThemeColors.TextPrimary;
            Padding = new Padding(12);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            var arcRect = new Rectangle(rect.X, rect.Y, d, d);

            // Top-left
            path.AddArc(arcRect, 180, 90);
            // Top-right
            arcRect.X = rect.Right - d;
            path.AddArc(arcRect, 270, 90);
            // Bottom-right
            arcRect.Y = rect.Bottom - d;
            path.AddArc(arcRect, 0, 90);
            // Bottom-left
            arcRect.X = rect.X;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using var path = GetRoundedRectPath(rect, _cornerRadius);

            // Fill background
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);
            }

            // Draw border
            if (_borderWidth > 0)
            {
                using var pen = new Pen(_borderColor, _borderWidth);
                g.DrawPath(pen, path);
            }

            // Clip children to rounded region
            Region = new Region(path);
        }
    }
}
