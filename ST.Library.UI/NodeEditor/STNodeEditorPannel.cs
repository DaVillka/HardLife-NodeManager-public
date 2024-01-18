using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ST.Library.UI.NodeEditor
{
    public class STNodeEditorPannel : Control
    {
        private bool _LeftLayout = true;
        /// <summary>
        /// 获取或设置是否是左边布局
        /// </summary>
        [Description("获取或设置是否是左边布局"), DefaultValue(true)]
        public bool LeftLayout
        {
            get => _LeftLayout;
            set
            {
                if (value == _LeftLayout)
                {
                    return;
                }

                _LeftLayout = value;
                SetLocation();
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或这是分割线颜色
        /// </summary>
        [Description("获取或这是分割线颜色"), DefaultValue(typeof(Color), "Black")]
        public Color SplitLineColor { get; set; } = Color.Black;

        /// <summary>
        /// 获取或设置分割线手柄颜色
        /// </summary>
        [Description("获取或设置分割线手柄颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color HandleLineColor { get; set; } = Color.Gray;

        /// <summary>
        /// 获取或设置编辑器缩放时候显示比例
        /// </summary>
        [Description("获取或设置编辑器缩放时候显示比例"), DefaultValue(true)]
        public bool ShowScale { get; set; } = true;

        /// <summary>
        /// 获取或设置节点连线时候是否显示状态
        /// </summary>
        [Description("获取或设置节点连线时候是否显示状态"), DefaultValue(true)]
        public bool ShowConnectionStatus { get; set; } = true;

        private int _X;
        /// <summary>
        /// 获取或设置分割线水平宽度
        /// </summary>
        [Description("获取或设置分割线水平宽度"), DefaultValue(201)]
        public int X
        {
            get => _X;
            set
            {
                if (value < 122)
                {
                    value = 122;
                }
                else if (value > Width - 122)
                {
                    value = Width - 122;
                }

                if (_X == value)
                {
                    return;
                }

                _X = value;
                SetLocation();
            }
        }

        private int _Y;
        /// <summary>
        /// 获取或设置分割线垂直高度
        /// </summary>
        [Description("获取或设置分割线垂直高度")]
        public int Y
        {
            get => _Y;
            set
            {
                if (value < 122)
                {
                    value = 122;
                }
                else if (value > Height - 122)
                {
                    value = Height - 122;
                }

                if (_Y == value)
                {
                    return;
                }

                _Y = value;
                SetLocation();
            }
        }
        /// <summary>
        /// 获取面板中的STNodeEditor
        /// </summary>
        [Description("获取面板中的STNodeEditor"), Browsable(false)]
        public STNodeEditor Editor { get; }
        /// <summary>
        /// 获取面板中的STNodeTreeView
        /// </summary>
        [Description("获取面板中的STNodeTreeView"), Browsable(false)]
        public STNodeTreeView TreeView { get; }
        /// <summary>
        /// 获取面板中的STNodePropertyGrid
        /// </summary>
        [Description("获取面板中的STNodePropertyGrid"), Browsable(false)]
        public STNodePropertyGrid PropertyGrid { get; }

        private Point m_pt_down;
        private bool m_is_mx;
        private bool m_is_my;
        private readonly Pen m_pen;

        private bool m_nInited;
        private readonly Dictionary<ConnectionStatus, string> m_dic_status_key = new Dictionary<ConnectionStatus, string>();

        public override Size MinimumSize
        {
            get => base.MinimumSize;
            set
            {
                value = new Size(250, 250);
                base.MinimumSize = value;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool bRedraw);

        public STNodeEditorPannel()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Editor = new STNodeEditor();
            TreeView = new STNodeTreeView();
            PropertyGrid = new STNodePropertyGrid
            {
                Text = "NodeProperty"
            };
            Controls.Add(Editor);
            Controls.Add(TreeView);
            Controls.Add(PropertyGrid);
            Size = new Size(500, 500);
            MinimumSize = new Size(250, 250);
            BackColor = Color.FromArgb(255, 34, 34, 34);

            m_pen = new Pen(BackColor, 3);

            Type t = typeof(ConnectionStatus);
            Array vv = Enum.GetValues(t);
            object vvv = vv.GetValue(0);
            foreach (System.Reflection.FieldInfo f in t.GetFields())
            {
                if (!f.FieldType.IsEnum)
                {
                    continue;
                }

                foreach (object a in f.GetCustomAttributes(true))
                {
                    if (!(a is DescriptionAttribute))
                    {
                        continue;
                    }

                    m_dic_status_key.Add((ConnectionStatus)f.GetValue(f), ((DescriptionAttribute)a).Description);
                }
            }

            Editor.ActiveChanged += (s, e) => PropertyGrid.SetNode(Editor.ActiveNode);
            Editor.CanvasScaled += (s, e) =>
            {
                if (ShowScale)
                {
                    Editor.ShowAlert(Editor.CanvasScale.ToString("F2"), Color.White, Color.FromArgb(127, 255, 255, 0));
                }
            };
            Editor.OptionConnected += (s, e) =>
            {
                if (ShowConnectionStatus)
                {
                    Editor.ShowAlert(m_dic_status_key[e.Status], Color.White, e.Status == ConnectionStatus.Connected ? Color.FromArgb(125, Color.Lime) : Color.FromArgb(125, Color.Red));
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!m_nInited)
            {
                _Y = Height / 2;
                _X = _LeftLayout ? 201 : Width - 202;
                m_nInited = true;
                SetLocation();
                return;
            }
            SetLocation();
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (width < 250)
            {
                width = 250;
            }

            if (height < 250)
            {
                height = 250;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            m_pen.Width = 3;
            m_pen.Color = SplitLineColor;
            g.DrawLine(m_pen, _X, 0, _X, Height);
            int nX;
            if (_LeftLayout)
            {
                g.DrawLine(m_pen, 0, _Y, _X - 1, _Y);
                nX = _X / 2;
            }
            else
            {
                g.DrawLine(m_pen, _X + 2, _Y, Width, _Y);
                nX = _X + ((Width - _X) / 2);
            }
            m_pen.Width = 1;
            HandleLineColor = Color.Gray;
            m_pen.Color = HandleLineColor;
            g.DrawLine(m_pen, _X, _Y - 10, _X, _Y + 10);
            g.DrawLine(m_pen, nX - 10, _Y, nX + 10, _Y);
        }

        private void SetLocation()
        {
            if (_LeftLayout)
            {
                //m_tree.Location = Point.Empty;
                //m_tree.Size = new Size(m_sx - 1, m_sy - 1);
                _ = STNodeEditorPannel.MoveWindow(TreeView.Handle, 0, 0, _X - 1, _Y - 1, false);

                //m_grid.Location = new Point(0, m_sy + 2);
                //m_grid.Size = new Size(m_sx - 1, this.Height - m_sy - 2);
                _ = STNodeEditorPannel.MoveWindow(PropertyGrid.Handle, 0, _Y + 2, _X - 1, Height - _Y - 2, false);

                //m_editor.Location = new Point(m_sx + 2, 0);
                //m_editor.Size = new Size(this.Width - m_sx - 2, this.Height);
                _ = STNodeEditorPannel.MoveWindow(Editor.Handle, _X + 2, 0, Width - _X - 2, Height, false);
            }
            else
            {
                _ = STNodeEditorPannel.MoveWindow(Editor.Handle, 0, 0, _X - 1, Height, false);
                _ = STNodeEditorPannel.MoveWindow(TreeView.Handle, _X + 2, 0, Width - _X - 2, _Y - 1, false);
                _ = STNodeEditorPannel.MoveWindow(PropertyGrid.Handle, _X + 2, _Y + 2, Width - _X - 2, Height - _Y - 2, false);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_pt_down = e.Location;
            m_is_mx = m_is_my = false;
            if (Cursor == Cursors.VSplit)
            {
                m_is_mx = true;
            }
            else if (Cursor == Cursors.HSplit)
            {
                m_is_my = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                int nw = 122;// (int)(this.Width * 0.1f);
                int nh = 122;// (int)(this.Height * 0.1f);
                if (m_is_mx)
                {
                    _X = e.X;// -m_pt_down.X;
                    if (_X < nw)
                    {
                        _X = nw;
                    }
                    else if (_X + nw > Width)
                    {
                        _X = Width - nw;
                    }
                }
                else if (m_is_my)
                {
                    _Y = e.Y;
                    if (_Y < nh)
                    {
                        _Y = nh;
                    }
                    else if (_Y + nh > Height)
                    {
                        _Y = Height - nh;
                    }
                }
                //m_rx = this.Width - m_sx;// (float)m_sx / this.Width;
                //m_fh = (float)m_sy / this.Height;
                SetLocation();
                Invalidate();
                return;
            }
            Cursor = Math.Abs(e.X - _X) < 2 ? Cursors.VSplit : Math.Abs(e.Y - _Y) < 2 ? Cursors.HSplit : Cursors.Arrow;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_is_mx = m_is_my = false;
            Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// 向树控件中添加一个STNode
        /// </summary>
        /// <param name="stNodeType">STNode类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddSTNode(Type stNodeType)
        {
            return TreeView.AddNode(stNodeType);
        }
        /// <summary>
        /// 从程序集中加载STNode
        /// </summary>
        /// <param name="strFileName">程序集路径</param>
        /// <returns>添加成功个数</returns>
        public int LoadAssembly(string strFileName)
        {
            _ = Editor.LoadAssembly(strFileName);
            return TreeView.LoadAssembly(strFileName);
        }
        /// <summary>
        /// 设置编辑器显示连接状态的文本
        /// </summary>
        /// <param name="status">连接状态</param>
        /// <param name="strText">对应显示文本</param>
        /// <returns>旧文本</returns>
        public string SetConnectionStatusText(ConnectionStatus status, string strText)
        {
            if (m_dic_status_key.ContainsKey(status))
            {
                string strOld = m_dic_status_key[status];
                m_dic_status_key[status] = strText;
                return strOld;
            }
            m_dic_status_key.Add(status, strText);
            return strText;
        }
    }
}
