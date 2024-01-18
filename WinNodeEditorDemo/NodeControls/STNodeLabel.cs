using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace WinNodeEditorDemo.NodeControls
{
    internal class STNodeLabel : STNodeControl
    {
        public StringAlignment Alignment { get; set; } = StringAlignment.Center;
        public float Rotate { get; set; }
        protected override void OnPaint(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            var al = m_sf.Alignment;
            m_sf.Alignment = Alignment;
            g.RotateTransform(Rotate);
            g.DrawString(Text, Font, Brushes.White, ClientRectangle, m_sf);
            base.OnPaint(dt);
            g.RotateTransform(-Rotate);
            m_sf.Alignment = al;
        }
    }
}
