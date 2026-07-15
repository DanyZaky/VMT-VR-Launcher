using System.Drawing.Drawing2D;

namespace VMT_VR_Launcher.Controls
{
    /// <summary>
    /// Custom Button with rounded corners, hover animation, and icon support.
    /// Supports Primary (blue), Success (green), and Secondary (subtle) styles.
    /// </summary>
    public class ThemedButton : Button
    {
        public enum ButtonStyle { Primary, Success, Secondary, Danger }

        private ButtonStyle _style = ButtonStyle.Primary;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private int _cornerRadius = 8;
        private string _icon = string.Empty;

        public ButtonStyle Style
        {
            get => _style;
            set { _style = value; Invalidate(); }
        }

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; Invalidate(); }
        }

        public ThemedButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = ThemeColors.FontButton;
            Cursor = Cursors.Hand;
            Height = 36;
        }

        private (Color bg, Color bgHover, Color bgPressed, Color fg) GetColors()
        {
            return _style switch
            {
                ButtonStyle.Primary => (ThemeColors.AccentBlue, ThemeColors.AccentBlueHover,
                    Color.FromArgb(29, 78, 216), ThemeColors.TextHighlight),
                ButtonStyle.Success => (ThemeColors.AccentGreen, ThemeColors.AccentGreenHover,
                    Color.FromArgb(21, 128, 61), ThemeColors.TextHighlight),
                ButtonStyle.Danger => (ThemeColors.AccentRed, Color.FromArgb(220, 38, 38),
                    Color.FromArgb(185, 28, 28), ThemeColors.TextHighlight),
                ButtonStyle.Secondary => (ThemeColors.BackgroundCard, ThemeColors.BackgroundHover,
                    Color.FromArgb(42, 58, 82), ThemeColors.TextPrimary),
                _ => (ThemeColors.AccentBlue, ThemeColors.AccentBlueHover,
                    Color.FromArgb(29, 78, 216), ThemeColors.TextHighlight)
            };
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            var arcRect = new Rectangle(rect.X, rect.Y, d, d);

            path.AddArc(arcRect, 180, 90);
            arcRect.X = rect.Right - d;
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = rect.Bottom - d;
            path.AddArc(arcRect, 0, 90);
            arcRect.X = rect.X;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var (bg, bgHover, bgPressed, fg) = GetColors();
            Color currentBg = _isPressed ? bgPressed : _isHovered ? bgHover : bg;

            if (!Enabled)
            {
                currentBg = Color.FromArgb(100, bg);
                fg = Color.FromArgb(120, fg);
            }

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = GetRoundedRectPath(rect, _cornerRadius);

            // Fill
            using (var brush = new SolidBrush(currentBg))
            {
                g.FillPath(brush, path);
            }

            // Border for Secondary style
            if (_style == ButtonStyle.Secondary)
            {
                using var pen = new Pen(ThemeColors.Border, 1);
                g.DrawPath(pen, path);
            }

            // Text + Icon
            string displayText = string.IsNullOrEmpty(_icon) ? Text : $"{_icon}  {Text}";
            
            TextRenderer.DrawText(g, displayText, Font, rect, fg, 
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            _isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isPressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }
    }
}
