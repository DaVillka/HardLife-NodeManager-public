﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
/*
MIT License

Copyright (c) 2021 DebugST@crystal_lz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
/*
 * create: 2021-02-23
 * modify: 2021-03-02
 * Author: Crystal_lz
 * blog: http://st233.com
 * Gitee: https://gitee.com/DebugST
 * Github: https://github.com/DebugST
 */
namespace ST.Library.UI.NodeEditor
{
    public class STNodeTreeView : Control
    {
        /// <summary>
        /// 获取或设置每行属性选项背景色
        /// </summary>
        [Description("获取或设置每行属性选项背景色")]
        public Color ItemBackColor { get; set; } = Color.FromArgb(255, 45, 45, 45);

        /// <summary>
        /// 获取或设置属性选项被鼠标悬停时候背景色
        /// </summary>
        [Description("获取或设置属性选项被鼠标悬停时候背景色")]
        public Color ItemHoverColor { get; set; } = Color.FromArgb(50, 125, 125, 125);

        private Color _TitleColor = Color.FromArgb(255, 60, 60, 60);
        /// <summary>
        /// 获取或设置顶部检索区域背景色
        /// </summary>
        [Description("获取或设置顶部检索区域背景颜色")]
        public Color TitleColor
        {
            get => _TitleColor;
            set
            {
                _TitleColor = value;
                Invalidate(new Rectangle(0, 0, Width, m_nItemHeight));
            }
        }

        /// <summary>
        /// 获取或设置检索文本框的背景色
        /// </summary>
        [Description("获取或设置检索文本框的背景色")]
        public Color TextBoxColor
        {
            get => m_tbx.BackColor;
            set
            {
                m_tbx.BackColor = value;
                Invalidate(new Rectangle(0, 0, Width, m_nItemHeight));
            }
        }

        /// <summary>
        /// 获取或设置检索时候高亮文本颜色
        /// </summary>
        [Description("获取或设置检索时候高亮文本颜色"), DefaultValue(typeof(Color), "Lime")]
        public Color HightLightTextColor { get; set; } = Color.Lime;

        /// <summary>
        /// 获取或设置信息显示按钮颜色 若设置AutoColor无法设置此属性值
        /// </summary>
        [Description("获取或设置信息显示按钮颜色 若设置AutoColor无法设置此属性值"), DefaultValue(typeof(Color), "Gray")]
        public Color InfoButtonColor { get; set; } = Color.Gray;

        /// <summary>
        /// 获取或设置统计个数的文本颜色
        /// </summary>
        [Description("获取或设置统计个数的文本颜色")]
        public Color FolderCountColor { get; set; } = Color.FromArgb(40, 255, 255, 255);

        private readonly Color _SwitchColor = Color.LightGray;

        /// <summary>
        /// 获取或设置是否统计STNode的个数
        /// </summary>
        [Description("获取或设置是否统计STNode的个数"), DefaultValue(typeof(Color), "LightGray")]
        public bool ShowFolderCount { get; set; } = true;

        /// <summary>
        /// 获取或设置是否显示信息按钮
        /// </summary>
        [Description("获取或设置是否显示信息按钮"), DefaultValue(true)]
        public bool ShowInfoButton { get; set; } = true;

        /// <summary>
        /// 获取或设置预览窗口是否是向左布局
        /// </summary>
        [Description("获取或设置预览窗口是否是向左布局"), DefaultValue(true)]
        public bool InfoPanelIsLeftLayout { get; set; } = true;

        private bool _AutoColor = true;
        /// <summary>
        /// 获取或设置控件中部分颜色来之对应的STNode的标题颜色
        /// </summary>
        [Description("获取或设置控件中部分颜色来之对应的STNode的标题颜色"), DefaultValue(true)]
        public bool AutoColor
        {
            get => _AutoColor;
            set
            {
                _AutoColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取节点预览时候使用的STNodeEditor
        /// </summary>
        [Description("获取节点预览时候使用的STNodeEditor"), Browsable(false)]
        public STNodeEditor Editor { get; }

        /// <summary>
        /// 获取节点预览时候使用的STNodePropertyGrid
        /// </summary>
        [Description("获取节点预览时候使用的STNodePropertyGrid"), Browsable(false)]
        public STNodePropertyGrid PropertyGrid { get; }

        private readonly int m_nItemHeight = 29;

        private static readonly Type m_type_node_base = typeof(STNode);
        private static readonly char[] m_chr_splitter = new char[] { '/', '\\' };
        private STNodeTreeCollection m_items_draw;
        private readonly STNodeTreeCollection m_items_source = new STNodeTreeCollection("ROOT");
        private readonly Dictionary<Type, string> m_dic_all_type = new Dictionary<Type, string>();

        private readonly Pen m_pen;
        private readonly SolidBrush m_brush;
        private readonly StringFormat m_sf;
        private DrawingTools m_dt;
        private readonly Color m_clr_item_1 = Color.FromArgb(10, 0, 0, 0);// Color.FromArgb(255, 40, 40, 40);
        private readonly Color m_clr_item_2 = Color.FromArgb(10, 255, 255, 255);// Color.FromArgb(255, 50, 50, 50);

        private int m_nOffsetY;                         //控件绘制时候需要偏移的垂直高度
        private int m_nSourceOffsetY;                   //绘制源数据时候需要偏移的垂直高度
        private int m_nSearchOffsetY;                   //绘制检索数据时候需要偏移的垂直高度
        private int m_nVHeight;                         //控件中内容需要的总高度

        private bool m_bHoverInfo;                      //当前鼠标是否悬停在信息显示按钮上
        private STNodeTreeCollection m_item_hover;      //当前鼠标悬停的树节点
        private Point m_pt_control;                     //鼠标在控件上的坐标
        private Point m_pt_offsety;                     //鼠标在控件上锤子偏移后的坐标
        private Rectangle m_rect_clear;                 //清空检索按钮区域

        private string m_str_search;                    //检索的文本
        private readonly TextBox m_tbx = new TextBox();          //检索文本框
        /// <summary>
        /// 构造一个STNode树控件
        /// </summary>
        public STNodeTreeView()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            MinimumSize = new System.Drawing.Size(100, 60);
            Size = new System.Drawing.Size(200, 150);
            m_items_draw = m_items_source;
            m_pen = new Pen(Color.Black);
            m_brush = new SolidBrush(Color.White);
            m_sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            m_dt.Pen = m_pen;
            m_dt.SolidBrush = m_brush;
            ForeColor = Color.FromArgb(255, 220, 220, 220);
            BackColor = Color.FromArgb(255, 35, 35, 35);
            m_tbx.Left = 6;
            m_tbx.BackColor = Color.FromArgb(255, 30, 30, 30);
            m_tbx.ForeColor = ForeColor;
            m_tbx.BorderStyle = BorderStyle.None;
            m_tbx.MaxLength = 20;
            m_tbx.TextChanged += new EventHandler(m_tbx_TextChanged);
            Controls.Add(m_tbx);
            AllowDrop = true;

            Editor = new STNodeEditor();
            PropertyGrid = new STNodePropertyGrid();
        }

        #region private method ==========

        private void m_tbx_TextChanged(object sender, EventArgs e)
        {
            m_str_search = m_tbx.Text.Trim().ToLower();
            m_nSearchOffsetY = 0;
            if (m_str_search == string.Empty)
            {
                m_items_draw = m_items_source;
                Invalidate();
                return;
            }
            m_items_draw = m_items_source.Copy();
            _ = Search(m_items_draw, new Stack<string>(), m_str_search);
            Invalidate();
        }

        private bool Search(STNodeTreeCollection items, Stack<string> stack, string strText)
        {
            bool bFound = false;
            _ = new string[items.Count];
            int nCounter = 0;
            foreach (STNodeTreeCollection v in items)
            {
                if (v.NameLower.IndexOf(strText) != -1)
                {
                    v.IsOpen = bFound = true;
                }
                else
                {
                    if (!Search(v, stack, strText))
                    {
                        stack.Push(v.Name);
                        nCounter++;
                    }
                    else
                    {
                        v.IsOpen = bFound = true;
                    }
                }
            }
            for (int i = 0; i < nCounter; i++)
            {
                _ = items.Remove(stack.Pop(), false);
            }

            return bFound;
        }

        private bool AddSTNode(Type stNodeType, STNodeTreeCollection items, string strLibName, bool bShowException)
        {
            if (m_dic_all_type.ContainsKey(stNodeType))
            {
                return false;
            }

            if (stNodeType == null)
            {
                return false;
            }

            if (!stNodeType.IsSubclassOf(m_type_node_base))
            {
                return bShowException ? throw new ArgumentException("不支持的类型[" + stNodeType.FullName + "] [stNodeType]参数值必须为[STNode]子类的类型") : false;
            }
            STNodeAttribute attr = GetNodeAttribute(stNodeType);
            if (attr == null)
            {
                return bShowException ? throw new InvalidOperationException("类型[" + stNodeType.FullName + "]未被[STNodeAttribute]所标记") : false;
            }
            string strPath = string.Empty;
            items.STNodeCount++;
            if (!string.IsNullOrEmpty(attr.Path))
            {
                string[] strKeys = attr.Path.Split(m_chr_splitter);
                for (int i = 0; i < strKeys.Length; i++)
                {
                    items = items.Add(strKeys[i]);
                    items.STNodeCount++;
                    strPath += "/" + strKeys[i];
                }
            }
            try
            {
                STNode node = (STNode)Activator.CreateInstance(stNodeType);
                STNodeTreeCollection stt = new STNodeTreeCollection(node.Title)
                {
                    Path = (strLibName + "/" + attr.Path).Trim('/'),
                    STNodeType = stNodeType
                };
                items[stt.Name] = stt;
                stt.STNodeTypeColor = node.TitleColor;
                m_dic_all_type.Add(stNodeType, stt.Path);
                Invalidate();
            }
            catch (Exception ex)
            {
                return bShowException ? throw ex : false;
            }
            return true;
        }

        private STNodeTreeCollection AddAssemblyPrivate(string strFile)
        {
            strFile = System.IO.Path.GetFullPath(strFile);
            Assembly asm = Assembly.LoadFrom(strFile);
            STNodeTreeCollection items = new STNodeTreeCollection(System.IO.Path.GetFileNameWithoutExtension(strFile));
            foreach (Type v in asm.GetTypes())
            {
                if (v.IsAbstract)
                {
                    continue;
                }

                if (v.IsSubclassOf(m_type_node_base))
                {
                    _ = AddSTNode(v, items, items.Name, false);
                }
            }
            return items;
        }

        private STNodeAttribute GetNodeAttribute(Type stNodeType)
        {
            if (stNodeType == null)
            {
                return null;
            }

            foreach (object v in stNodeType.GetCustomAttributes(true))
            {
                if (!(v is STNodeAttribute))
                {
                    continue;
                }

                return (STNodeAttribute)v;
            }
            return null;
        }

        private STNodeTreeCollection FindItemByPoint(STNodeTreeCollection items, Point pt)
        {
            foreach (STNodeTreeCollection t in items)
            {
                if (t.DisplayRectangle.Contains(pt))
                {
                    return t;
                }

                if (t.IsOpen)
                {
                    STNodeTreeCollection n = FindItemByPoint(t, pt);
                    if (n != null)
                    {
                        return n;
                    }
                }
            }
            return null;
        }

        #endregion

        #region overide method ==========

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            m_tbx.Top = (m_nItemHeight - m_tbx.Height) / 2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            m_tbx.Width = Width - 29;
            m_rect_clear = new Rectangle(Width - 20, 9, 12, 12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            m_nOffsetY = string.IsNullOrEmpty(m_str_search) ? m_nSourceOffsetY : m_nSearchOffsetY;
            Graphics g = e.Graphics;
            m_dt.Graphics = g;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TranslateTransform(0, m_nOffsetY);
            int nCounter = 0;
            foreach (STNodeTreeCollection v in m_items_draw)
            {
                nCounter = OnStartDrawItem(m_dt, v, nCounter, 0);
            }

            m_nVHeight = (nCounter + 1) * m_nItemHeight;
            foreach (STNodeTreeCollection v in m_items_draw)
            {
                OnDrawSwitch(m_dt, v);
            }

            g.ResetTransform();
            OnDrawSearch(m_dt);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool bRedraw = false;
            m_pt_offsety = m_pt_control = e.Location;
            m_pt_offsety.Y -= m_nOffsetY;
            Cursor = !string.IsNullOrEmpty(m_str_search) && m_rect_clear.Contains(e.Location) ? Cursors.Hand : Cursors.Arrow;
            STNodeTreeCollection node = FindItemByPoint(m_items_draw, m_pt_offsety);
            if (m_item_hover != node)
            {
                m_item_hover = node;
                bRedraw = true;
            }
            if (node != null)
            {
                bool bHoverInfo = node.InfoRectangle.Contains(m_pt_offsety);
                if (bHoverInfo != m_bHoverInfo)
                {
                    m_bHoverInfo = bHoverInfo;
                    bRedraw = true;
                }
            }
            if (bRedraw)
            {
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _ = Focus();
            if (!string.IsNullOrEmpty(m_str_search) && m_rect_clear.Contains(e.Location))
            {
                m_tbx.Text = string.Empty;
                return;
            }
            m_pt_offsety = m_pt_control = e.Location;
            m_pt_offsety.Y -= m_nOffsetY;
            if (m_item_hover == null)
            {
                return;
            }

            if (m_item_hover.SwitchRectangle.Contains(m_pt_offsety))
            {
                m_item_hover.IsOpen = !m_item_hover.IsOpen;
                Invalidate();
            }
            else if (m_item_hover.InfoRectangle.Contains(m_pt_offsety))
            {
                Rectangle rect = RectangleToScreen(m_item_hover.DisplayRectangle);
                FrmNodePreviewPanel frm = new FrmNodePreviewPanel(m_item_hover.STNodeType,
                    new Point(rect.Right - m_nItemHeight, rect.Top + m_nOffsetY),
                    m_nItemHeight,
                    InfoPanelIsLeftLayout,
                    Editor, PropertyGrid)
                {
                    BackColor = BackColor
                };
                frm.Show(this);
            }
            else if (m_item_hover.STNodeType != null)
            {
                DataObject d = new DataObject("STNodeType", m_item_hover.STNodeType);
                _ = DoDragDrop(d, DragDropEffects.Copy);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            m_pt_offsety = m_pt_control = e.Location;
            m_pt_offsety.Y -= m_nOffsetY;
            STNodeTreeCollection item = FindItemByPoint(m_items_draw, m_pt_offsety);
            if (item == null || item.STNodeType != null)
            {
                return;
            }

            item.IsOpen = !item.IsOpen;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (m_item_hover != null)
            {
                m_item_hover = null;
                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                if (m_nOffsetY == 0)
                {
                    return;
                }

                m_nOffsetY += m_nItemHeight;
                if (m_nOffsetY > 0)
                {
                    m_nOffsetY = 0;
                }
            }
            else
            {
                if (Height - m_nOffsetY >= m_nVHeight)
                {
                    return;
                }

                m_nOffsetY -= m_nItemHeight;
            }
            if (string.IsNullOrEmpty(m_str_search))
            {
                m_nSourceOffsetY = m_nOffsetY;
            }
            else
            {
                m_nSearchOffsetY = m_nOffsetY;
            }

            Invalidate();
        }

        #endregion

        #region protected method ==========
        /// <summary>
        /// 当绘制检索文本区域时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        protected virtual void OnDrawSearch(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            m_brush.Color = _TitleColor;
            g.FillRectangle(m_brush, 0, 0, Width, m_nItemHeight);
            m_brush.Color = m_tbx.BackColor;
            g.FillRectangle(m_brush, 5, 5, Width - 10, m_nItemHeight - 10);
            m_pen.Color = ForeColor;
            if (string.IsNullOrEmpty(m_str_search))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawEllipse(m_pen, Width - 17, 8, 8, 8);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.DrawLine(m_pen, Width - 13, 17, Width - 13, m_nItemHeight - 9);
            }
            else
            {
                m_pen.Color = ForeColor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawEllipse(m_pen, Width - 20, 9, 10, 10);
                g.DrawLine(m_pen, Width - 18, 11, Width - 12, 17);
                g.DrawLine(m_pen, Width - 12, 11, Width - 18, 17);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            }
        }
        /// <summary>
        /// 当开始绘制树节点的每一个节点时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        /// <param name="Items">当前需要绘制的集合</param>
        /// <param name="nCounter">已经绘制个数的计数器</param>
        /// <param name="nLevel">当前位于第几级子集合</param>
        /// <returns>已经绘制个数</returns>
        protected virtual int OnStartDrawItem(DrawingTools dt, STNodeTreeCollection Items, int nCounter, int nLevel)
        {
            _ = dt.Graphics;
            Items.DisplayRectangle = new Rectangle(0, m_nItemHeight * (nCounter + 1), Width, m_nItemHeight);
            Items.SwitchRectangle = new Rectangle(5 + (nLevel * 10), (nCounter + 1) * m_nItemHeight, 10, m_nItemHeight);
            Items.InfoRectangle = ShowInfoButton && Items.STNodeType != null
                ? new Rectangle(Width - 18, Items.DisplayRectangle.Top + ((m_nItemHeight - 14) / 2), 14, 14)
                : Rectangle.Empty;
            OnDrawItem(dt, Items, nCounter++, nLevel);
            if (!Items.IsOpen)
            {
                return nCounter;
            }

            foreach (STNodeTreeCollection n in Items)
            {
                if (n.STNodeType == null)
                {
                    nCounter = OnStartDrawItem(dt, n, nCounter++, nLevel + 1);
                }
            }
            foreach (STNodeTreeCollection n in Items)
            {
                if (n.STNodeType != null)
                {
                    nCounter = OnStartDrawItem(dt, n, nCounter++, nLevel + 1);
                }
            }
            foreach (STNodeTreeCollection v in Items)
            {
                OnDrawSwitch(dt, v);
            }

            return nCounter;
        }
        /// <summary>
        /// 当绘制树节点每一个节点时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        /// <param name="items">当前需要绘制的集合</param>
        /// <param name="nCounter">已经绘制个数的计数器</param>
        /// <param name="nLevel">当前位于第几级子集合</param>
        protected virtual void OnDrawItem(DrawingTools dt, STNodeTreeCollection items, int nCounter, int nLevel)
        {
            Graphics g = dt.Graphics;
            m_brush.Color = nCounter % 2 == 0 ? m_clr_item_1 : m_clr_item_2;
            g.FillRectangle(m_brush, items.DisplayRectangle);
            if (items == m_item_hover)
            {
                m_brush.Color = ItemHoverColor;
                g.FillRectangle(m_brush, items.DisplayRectangle);
            }
            Rectangle rect = new Rectangle(45 + (nLevel * 10), items.SwitchRectangle.Top, Width - 45 - (nLevel * 10), m_nItemHeight);
            m_pen.Color = Color.FromArgb(100, 125, 125, 125);
            g.DrawLine(m_pen, 9, items.SwitchRectangle.Top + (m_nItemHeight / 2), items.SwitchRectangle.Left + 19, items.SwitchRectangle.Top + (m_nItemHeight / 2));
            if (nCounter != 0)
            {
                for (int i = 0; i <= nLevel; i++)
                {
                    g.DrawLine(m_pen, 9 + (i * 10), items.SwitchRectangle.Top - (m_nItemHeight / 2), 9 + (i * 10), items.SwitchRectangle.Top + (m_nItemHeight / 2) - 1);
                }
            }
            OnDrawItemText(dt, items, rect);
            OnDrawItemIcon(dt, items, rect);
        }
        /// <summary>
        /// 当绘制树节点展开与关闭开关时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        /// <param name="items">当前需要绘制的集合</param>
        protected virtual void OnDrawSwitch(DrawingTools dt, STNodeTreeCollection items)
        {
            Graphics g = dt.Graphics;
            if (items.Count != 0)
            {
                m_pen.Color = _SwitchColor;
                m_brush.Color = m_pen.Color;
                int nT = items.SwitchRectangle.Y + (m_nItemHeight / 2) - 4;
                g.DrawRectangle(m_pen, items.SwitchRectangle.Left, nT, 8, 8);
                g.DrawLine(m_pen, items.SwitchRectangle.Left + 1, nT + 4, items.SwitchRectangle.Right - 3, nT + 4);
                if (items.IsOpen)
                {
                    return;
                }

                g.DrawLine(m_pen, items.SwitchRectangle.Left + 4, nT + 1, items.SwitchRectangle.Left + 4, nT + 7);
                //if (items.IsOpen) {
                //    //g.FillPolygon(m_brush, new Point[]{
                //    //    new Point(items.DotRectangle.Left + 0, items.DotRectangle.Top + m_nItemHeight / 2 - 2),
                //    //    new Point(items.DotRectangle.Left + 9, items.DotRectangle.Top + m_nItemHeight / 2 - 2),
                //    //    new Point(items.DotRectangle.Left + 4, items.DotRectangle.Top + m_nItemHeight / 2 + 3)
                //    //});
                //    g.DrawRectangle(m_pen, items.SwitchRectangle.Left, nT, 8, 8);
                //    g.DrawLine(m_pen, items.SwitchRectangle.Left + 1, nT + 4, items.SwitchRectangle.Right - 3, nT + 4);
                //} else {
                //    //g.FillPolygon(m_brush, new Point[]{
                //    //    new Point(items.DotRectangle.Left + 2, items.DotRectangle.Top + m_nItemHeight / 2 - 5),
                //    //    new Point(items.DotRectangle.Left + 2, items.DotRectangle.Top + m_nItemHeight / 2 + 5),
                //    //    new Point(items.DotRectangle.Left + 7, items.DotRectangle.Top + m_nItemHeight / 2)
                //    //});
                //    g.DrawRectangle(m_pen, items.SwitchRectangle.Left, nT, 8, 8);
                //    g.DrawLine(m_pen, items.SwitchRectangle.Left + 1, nT + 4, items.SwitchRectangle.Right - 3, nT + 4);
                //    g.DrawLine(m_pen, items.SwitchRectangle.Left + 4, nT + 1, items.SwitchRectangle.Left + 4, nT + 7);
                //}
            }
        }
        /// <summary>
        /// 当绘制树节点的文本时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        /// <param name="items">当前需要绘制的集合</param>
        /// <param name="rect">文本域所在矩形区域</param>
        protected virtual void OnDrawItemText(DrawingTools dt, STNodeTreeCollection items, Rectangle rect)
        {
            Graphics g = dt.Graphics;
            rect.Width -= 20;
            m_sf.FormatFlags = StringFormatFlags.NoWrap;
            if (!string.IsNullOrEmpty(m_str_search))
            {
                int nIndex = items.NameLower.IndexOf(m_str_search);
                if (nIndex != -1)
                {
                    CharacterRange[] chrs = { new CharacterRange(nIndex, m_str_search.Length) };//global
                    m_sf.SetMeasurableCharacterRanges(chrs);
                    Region[] regions = g.MeasureCharacterRanges(items.Name, Font, rect, m_sf);
                    g.SetClip(regions[0], System.Drawing.Drawing2D.CombineMode.Intersect);
                    m_brush.Color = HightLightTextColor;
                    g.DrawString(items.Name, Font, m_brush, rect, m_sf);
                    g.ResetClip();
                    g.SetClip(regions[0], System.Drawing.Drawing2D.CombineMode.Exclude);
                    m_brush.Color = items.STNodeType == null ? Color.FromArgb(ForeColor.A * 1 / 2, ForeColor) : ForeColor;
                    g.DrawString(items.Name, Font, m_brush, rect, m_sf);
                    g.ResetClip();
                    return;
                }
            }
            m_brush.Color = items.STNodeType == null ? Color.FromArgb(ForeColor.A * 2 / 3, ForeColor) : ForeColor;
            g.DrawString(items.Name, Font, m_brush, rect, m_sf);
        }
        /// <summary>
        /// 当绘制树节点图标时候发生
        /// </summary>
        /// <param name="dt">绘制工具</param>
        /// <param name="items">当前需要绘制的集合</param>
        /// <param name="rect">文本域所在矩形区域</param>
        protected virtual void OnDrawItemIcon(DrawingTools dt, STNodeTreeCollection items, Rectangle rect)
        {
            Graphics g = dt.Graphics;
            if (items.STNodeType != null)
            {
                m_pen.Color = _AutoColor ? items.STNodeTypeColor : Color.DarkCyan;
                m_brush.Color = Color.LightGray;
                g.DrawRectangle(m_pen, rect.Left - 15, rect.Top + (m_nItemHeight / 2) - 5, 11, 10);
                g.FillRectangle(m_brush, rect.Left - 17, rect.Top + (m_nItemHeight / 2) - 2, 5, 5);
                g.FillRectangle(m_brush, rect.Left - 6, rect.Top + (m_nItemHeight / 2) - 2, 5, 5);
                if (m_item_hover == items && m_bHoverInfo)
                {
                    m_brush.Color = BackColor;
                    g.FillRectangle(m_brush, items.InfoRectangle);
                }
                m_pen.Color = _AutoColor ? items.STNodeTypeColor : InfoButtonColor;
                m_pen.Width = 2;
                g.DrawLine(m_pen, items.InfoRectangle.X + 4, items.InfoRectangle.Y + 3, items.InfoRectangle.X + 10, items.InfoRectangle.Y + 3);
                g.DrawLine(m_pen, items.InfoRectangle.X + 4, items.InfoRectangle.Y + 6, items.InfoRectangle.X + 10, items.InfoRectangle.Y + 6);
                g.DrawLine(m_pen, items.InfoRectangle.X + 4, items.InfoRectangle.Y + 11, items.InfoRectangle.X + 10, items.InfoRectangle.Y + 11);
                g.DrawLine(m_pen, items.InfoRectangle.X + 7, items.InfoRectangle.Y + 7, items.InfoRectangle.X + 7, items.InfoRectangle.Y + 10);
                m_pen.Width = 1;
                g.DrawRectangle(m_pen, items.InfoRectangle.X, items.InfoRectangle.Y, items.InfoRectangle.Width - 1, items.InfoRectangle.Height - 1);
            }
            else
            {
                if (items.IsLibraryRoot)
                {
                    Rectangle rect_box = new Rectangle(rect.Left - 15, rect.Top + (m_nItemHeight / 2) - 5, 11, 10);
                    g.DrawRectangle(Pens.Gray, rect_box);
                    g.DrawLine(Pens.Cyan, rect_box.X - 2, rect_box.Top, rect_box.X + 2, rect_box.Top);
                    g.DrawLine(Pens.Cyan, rect_box.X, rect_box.Y - 2, rect_box.X, rect_box.Y + 2);
                    g.DrawLine(Pens.Cyan, rect_box.Right - 2, rect_box.Bottom, rect_box.Right + 2, rect_box.Bottom);
                    g.DrawLine(Pens.Cyan, rect_box.Right, rect_box.Bottom - 2, rect_box.Right, rect_box.Bottom + 2);
                }
                else
                {
                    g.DrawRectangle(Pens.Goldenrod, new Rectangle(rect.Left - 16, rect.Top + (m_nItemHeight / 2) - 6, 8, 3));
                    g.DrawRectangle(Pens.Goldenrod, new Rectangle(rect.Left - 16, rect.Top + (m_nItemHeight / 2) - 3, 13, 9));
                }
                if (!ShowFolderCount)
                {
                    return;
                }

                m_sf.Alignment = StringAlignment.Far;
                m_brush.Color = FolderCountColor;
                rect.X -= 4;
                g.DrawString("[" + items.STNodeCount.ToString() + "]", Font, m_brush, rect, m_sf);
                m_sf.Alignment = StringAlignment.Near;
            }
        }

        #endregion

        #region public method ==========
        /// <summary>
        /// 在控件中检索STNode
        /// </summary>
        /// <param name="strText">需要检索的文本</param>
        public void Search(string strText)
        {
            if (strText == null)
            {
                return;
            }

            if (strText.Trim() == string.Empty)
            {
                return;
            }

            m_tbx.Text = strText.Trim();
        }
        /// <summary>
        /// 向控件中添加一个STNode类型
        /// </summary>
        /// <param name="stNodeType">STNode类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddNode(Type stNodeType) { return AddSTNode(stNodeType, m_items_source, null, true); }
        /// <summary>
        /// 从文件中向控件添加STNode类型
        /// </summary>
        /// <param name="strFile">指定文件路径</param>
        /// <returns>添加成功个数</returns>
        public int LoadAssembly(string strFile)
        {
            strFile = System.IO.Path.GetFullPath(strFile);
            STNodeTreeCollection items = AddAssemblyPrivate(strFile);
            if (items.STNodeCount == 0)
            {
                return 0;
            }

            items.IsLibraryRoot = true;
            m_items_source[items.Name] = items;
            return items.STNodeCount;
        }
        /// <summary>
        /// 清空控件中所有STNode类型
        /// </summary>
        public void Clear()
        {
            m_items_source.Clear();
            m_items_draw.Clear();
            m_dic_all_type.Clear();
            Invalidate();
        }
        /// <summary>
        /// 向控件中移除一个STNode类型
        /// </summary>
        /// <param name="stNodeType">STNode类型</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveNode(Type stNodeType)
        {
            if (!m_dic_all_type.ContainsKey(stNodeType))
            {
                return false;
            }

            string strPath = m_dic_all_type[stNodeType];
            STNodeTreeCollection items = m_items_source;
            if (!string.IsNullOrEmpty(strPath))
            {
                string[] strKeys = strPath.Split(m_chr_splitter);
                for (int i = 0; i < strKeys.Length; i++)
                {
                    items = items[strKeys[i]];
                    if (items == null)
                    {
                        return false;
                    }
                }
            }
            try
            {
                STNode node = (STNode)Activator.CreateInstance(stNodeType);
                if (items[node.Title] == null)
                {
                    return false;
                }

                _ = items.Remove(node.Title, true);
                _ = m_dic_all_type.Remove(stNodeType);
            }
            catch { return false; }
            Invalidate();
            return true;
        }

        #endregion
        //=================================================================================================
        /// <summary>
        /// STNodeTreeView控件中每一项的集合
        /// </summary>
        protected class STNodeTreeCollection : IEnumerable
        {
            /// <summary>
            /// 获取当前树节点显示名称
            /// </summary>
            public string Name { get; }
            /// <summary>
            /// 获取当前树节点显示名称的小写字符串
            /// </summary>
            public string NameLower { get; private set; }
            /// <summary>
            /// 获取当前树节点对应的STNode类型
            /// </summary>
            public Type STNodeType { get; internal set; }
            /// <summary>
            /// 获取当前树节点的父级树节点
            /// </summary>
            public STNodeTreeCollection Parent { get; internal set; }

            /// <summary>
            /// 获取当前树节点下拥有的STNode类型个数
            /// </summary>
            public int STNodeCount { get; internal set; }
            /// <summary>
            /// 获取当前树节点对应STNode类型在树控件中对应路径
            /// </summary>
            public string Path { get; internal set; }
            /// <summary>
            /// 获取当前或设置树节点是否为打开状态
            /// </summary>
            public bool IsOpen { get; set; }
            /// <summary>
            /// 获取当前树节点是否为加载模块的根路劲节点
            /// </summary>
            public bool IsLibraryRoot { get; internal set; }
            /// <summary>
            /// 获取当前树节点在控件中的显示区域
            /// </summary>
            public Rectangle DisplayRectangle { get; internal set; }
            /// <summary>
            /// 获取当前树节点在控件中的开关按钮区域
            /// </summary>
            public Rectangle SwitchRectangle { get; internal set; }
            /// <summary>
            /// 获取当前树节点在控件中的信息按钮区域
            /// </summary>
            public Rectangle InfoRectangle { get; internal set; }
            /// <summary>
            /// 获取当前树节点对应STNode类型的标题颜色
            /// </summary>
            public Color STNodeTypeColor { get; internal set; }
            /// <summary>
            /// 获取当前树节点所包含子节点个数
            /// </summary>
            public int Count => m_dic.Count;
            /// <summary>
            /// 获取或设置指定名称的集合
            /// </summary>
            /// <param name="strKey">指定名称</param>
            /// <returns>集合</returns>
            public STNodeTreeCollection this[string strKey]
            {
                get => string.IsNullOrEmpty(strKey) ? null : m_dic.ContainsKey(strKey) ? m_dic[strKey] : null;
                set
                {
                    if (string.IsNullOrEmpty(strKey))
                    {
                        return;
                    }

                    if (value == null)
                    {
                        return;
                    }

                    if (m_dic.ContainsKey(strKey))
                    {
                        m_dic[strKey] = value;
                    }
                    else
                    {
                        m_dic.Add(strKey, value);
                    }
                    value.Parent = this;
                }
            }

            private readonly SortedDictionary<string, STNodeTreeCollection> m_dic = new SortedDictionary<string, STNodeTreeCollection>();
            /// <summary>
            /// 构造一颗树节点集合
            /// </summary>
            /// <param name="strName">当前树节点在控件中的显示名称</param>
            public STNodeTreeCollection(string strName)
            {
                if (strName == null || strName.Trim() == string.Empty)
                {
                    throw new ArgumentNullException("显示名称不能为空");
                }

                Name = strName.Trim();
                NameLower = Name.ToLower();
            }
            /// <summary>
            /// 向当前树节点中添加一个子节点
            /// </summary>
            /// <param name="strName">节点显示名称</param>
            /// <returns>添加后的子节点集合</returns>
            public STNodeTreeCollection Add(string strName)
            {
                if (!m_dic.ContainsKey(strName))
                {
                    m_dic.Add(strName, new STNodeTreeCollection(strName) { Parent = this });
                }

                return m_dic[strName];
            }
            /// <summary>
            /// 向当前树节点中删除一个子集合
            /// </summary>
            /// <param name="strName">子集合名称</param>
            /// <param name="isAutoDelFolder">是否递归向上自动清空无用节点</param>
            /// <returns>是否删除成功</returns>
            public bool Remove(string strName, bool isAutoDelFolder)
            {
                if (!m_dic.ContainsKey(strName))
                {
                    return false;
                }

                bool b = m_dic.Remove(strName);
                STNodeTreeCollection temp = this;
                while (temp != null)
                {
                    temp.STNodeCount--;
                    temp = temp.Parent;
                }
                return isAutoDelFolder && m_dic.Count == 0 && Parent != null ? b && Parent.Remove(Name, isAutoDelFolder) : b;
            }
            /// <summary>
            /// 清空当前树节点中所有子节点
            /// </summary>
            public void Clear() { Clear(this); }

            private void Clear(STNodeTreeCollection items)
            {
                foreach (STNodeTreeCollection v in items)
                {
                    v.Clear(v);
                }

                m_dic.Clear();
            }
            /// <summary>
            /// 获取当前树节点中所有的名称数组
            /// </summary>
            /// <returns></returns>
            public string[] GetKeys() { return m_dic.Keys.ToArray(); }
            /// <summary>
            /// 拷贝当前树节点集合中所有数据
            /// </summary>
            /// <returns>拷贝的副本</returns>
            public STNodeTreeCollection Copy()
            {
                STNodeTreeCollection items = new STNodeTreeCollection("COPY");
                Copy(this, items);
                return items;
            }

            private void Copy(STNodeTreeCollection items_src, STNodeTreeCollection items_dst)
            {
                foreach (STNodeTreeCollection v in items_src)
                {
                    Copy(v, items_dst.Add(v.Name));
                }
                items_dst.Path = items_src.Path;
                items_dst.STNodeType = items_src.STNodeType;
                items_dst.IsLibraryRoot = items_src.IsLibraryRoot;
                items_dst.STNodeCount = items_src.STNodeCount;
                items_dst.STNodeTypeColor = items_src.STNodeTypeColor;
            }
            /// <summary>
            /// 返回 System.Collections.IEnumerator 的 Array
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                foreach (STNodeTreeCollection v in m_dic.Values)
                {
                    yield return v;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
