using System;
using System.Collections.Generic;

using System.Drawing;
using System.Windows.Forms;

namespace WinNodeEditorDemo.NodeControls
{
    /// <summary>
    /// 此类仅演示 作为MixRGB节点的下拉选择框弹出菜单
    /// </summary>
    public class FrmEnumSelect : Form
    {
        private Point m_pt;
        private readonly int m_nWidth;
        private readonly float m_scale;
        private readonly List<object> m_lst = new();
        private readonly StringFormat m_sf;

        public Enum Enum { get; set; }

        private bool m_bClosed;

        public FrmEnumSelect(Enum e, Point pt, int nWidth, float scale)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            foreach (object v in Enum.GetValues(e.GetType()))
            {
                m_lst.Add(v);
            }

            Enum = e;
            m_pt = pt;
            m_scale = scale;
            m_nWidth = nWidth;
            m_sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };

            ShowInTaskbar = false;
            BackColor = Color.FromArgb(255, 34, 34, 34);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = m_pt;
            Width = (int)(m_nWidth * m_scale);
            Height = (int)(m_lst.Count * 20 * m_scale);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.ScaleTransform(m_scale, m_scale);
            Rectangle rect = new(0, 0, Width, 20);
            foreach (object v in m_lst)
            {
                g.DrawString(v.ToString(), Font, Brushes.White, rect, m_sf);
                rect.Y += rect.Height;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int nIndex = e.Y / (int)(20 * m_scale);
            if (nIndex >= 0 && nIndex < m_lst.Count)
            {
                Enum = (Enum)m_lst[nIndex];
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            m_bClosed = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (m_bClosed)
            {
                return;
            }
            //this.DialogResult = System.Windows.Forms.DialogResult.None;
            Close();
        }
    }
}
