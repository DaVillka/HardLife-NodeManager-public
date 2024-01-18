using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace WinNodeEditorDemo.NodeControls
{
    internal class STNodeDoubleLabel : STNodeControl
    {
        //public string Text
        public STNodeDoubleLabel(string name, string text = null)
        {

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
