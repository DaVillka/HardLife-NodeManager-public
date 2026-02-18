using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace HardLife_Options.NodeControls
{
    internal class STNodeButton : STNodeControl
    {
        private Color _saveColot;
        public STNodeButton()
        {
            initialize("Кнопка");
        }
        public STNodeButton(string text)
        {
            initialize(text);
        }
        private void initialize(string text)
        {
            Alignment = System.Drawing.StringAlignment.Center;
            Location = new System.Drawing.Point(0, 0);
            Size = new System.Drawing.Size(60, 20);
            Text = text;
            _saveColot = base.BackColor;
            base.MouseEnter += (sender, e) => { base.BackColor = Color.FromArgb(base.BackColor.A - 50, base.BackColor); };
            base.MouseLeave += (sender, e) => { base.BackColor = _saveColot; };
            base.MouseDown += (sender, e) => { base.BackColor = Color.FromArgb(base.BackColor.A - 50, base.BackColor); };
            base.MouseUp += (sender, e) => { base.BackColor = Color.FromArgb(base.BackColor.A + 50, base.BackColor); };
        }
        public StringAlignment Alignment { get; set; } = StringAlignment.Center;
        protected override void OnPaint(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            m_sf.Alignment = Alignment;
            g.DrawString(Text, Font, Brushes.White, ClientRectangle, m_sf);
            base.OnPaint(dt);
        }
    }
}
