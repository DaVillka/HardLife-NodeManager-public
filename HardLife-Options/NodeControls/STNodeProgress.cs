using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;

namespace HardLife_Options.NodeControls
{
    /// <summary>
    /// 此类仅演示 作为MixRGB节点的进度条控件
    /// </summary>
    public class STNodeProgress : STNodeControl
    {
        private int _Value = 500;

        public int Value
        {
            get => _Value;
            set
            {
                _Value = value;
                Invalidate();
            }
        }

        private bool m_bMouseDown;
        private readonly int _mix = 0;
        private readonly int _max = 1000;
        public STNodeProgress() { }
        public STNodeProgress(int max)
        {
            _Value = max / 2;
            _max = max;
        }
        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected override void OnPaint(DrawingTools dt)
        {
            base.OnPaint(dt);
            Graphics g = dt.Graphics;
            g.FillRectangle(Brushes.Gray, ClientRectangle);
            g.FillRectangle(Brushes.CornflowerBlue, 0, 0, (int)((float)_Value / _max * Width), Height);
            m_sf.Alignment = StringAlignment.Near;
            g.DrawString(Text, Font, Brushes.White, ClientRectangle, m_sf);
            m_sf.Alignment = StringAlignment.Far;
            g.DrawString(_Value.ToString(), Font, Brushes.White, ClientRectangle, m_sf);

        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_bMouseDown = true;
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_bMouseDown = false;
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!m_bMouseDown)
            {
                return;
            }

            int v = (int)((float)e.X / Width * _max);
            if (v < 0)
            {
                v = 0;
            }

            if (v > _max)
            {
                v = _max;
            }

            _Value = v;
            OnValueChanged(new EventArgs());
            Invalidate();
        }
    }
}
