using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;

namespace WinNodeEditorDemo.NodeControls
{
    /// <summary>
    /// 此类仅演示 作为MixRGB节点的复选框控件
    /// </summary>
    public class STNodeCheckBox : STNodeControl
    {
        private bool _Checked;

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Checked = !Checked;
            OnValueChanged(new EventArgs());
        }

        protected override void OnPaint(DrawingTools dt)
        {
            //base.OnPaint(dt);
            Graphics g = dt.Graphics;
            g.FillRectangle(Brushes.Gray, 0, 5, 10, 10);
            m_sf.Alignment = StringAlignment.Near;
            g.DrawString(Text, Font, Brushes.LightGray, new Rectangle(15, 0, Width - 20, 20), m_sf);
            if (Checked)
            {
                g.FillRectangle(Brushes.Black, 2, 7, 6, 6);
            }
        }
    }
}
