using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;

namespace HardLife_Options.NodeControls
{
    /// <summary>
    /// 此类仅演示 作为MixRGB节点的下拉框控件
    /// </summary>
    public class STNodeSelectEnumBox : STNodeControl
    {
        private Enum _Enum;
        public Enum Enum
        {
            get => _Enum;
            set
            {
                _Enum = value;
                Invalidate();
            }
        }

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
        protected override void OnPaint(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            dt.SolidBrush.Color = Color.FromArgb(80, 10, 0, 0);
            g.FillRectangle(dt.SolidBrush, ClientRectangle);
            m_sf.Alignment = StringAlignment.Near;
            g.DrawString(Enum.ToString(), Font, Brushes.White, ClientRectangle, m_sf);
            g.FillPolygon(Brushes.Gray, new Point[]{
                new Point(Right - 25, 7),
                new Point(Right - 15, 7),
                new Point(Right - 20, 12)
            });
        }
        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Point pt = new(Left + Owner.Left, Top + Owner.Top + Owner.TitleHeight);
            pt = Owner.Owner.CanvasToControl(pt);
            pt = Owner.Owner.PointToScreen(pt);
            FrmEnumSelect frm = new(Enum, pt, Width, Owner.Owner.CanvasScale);
            System.Windows.Forms.DialogResult v = frm.ShowDialog();
            if (v != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            Enum = frm.Enum;
            OnValueChanged(new EventArgs());
        }
    }
}
