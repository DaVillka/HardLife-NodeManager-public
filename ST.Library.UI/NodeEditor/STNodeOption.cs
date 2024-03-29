﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ST.Library.UI.NodeEditor
{
    public class STNodeOption
    {
        #region Properties

        public static readonly STNodeOption Empty = new STNodeOption();

        private STNode _Owner;
        /// <summary>
        /// 获取当前 Option 所属的 Node
        /// </summary>
        public STNode Owner
        {
            get => _Owner;
            internal set
            {
                if (value == _Owner)
                {
                    return;
                }

                if (_Owner != null)
                {
                    DisConnectionAll();        //当所有者变更时 断开当前所有连接
                }

                _Owner = value;
            }
        }

        /// <summary>
        /// 获取当前 Option 是否仅能被连接一次
        /// </summary>
        public bool IsSingle { get; }

        /// <summary>
        /// 获取当前 Option 是否是输入选项
        /// </summary>
        public bool IsInput { get; internal set; }

        private Color _TextColor = Color.White;
        /// <summary>
        /// 获取或设置当前 Option 文本颜色
        /// </summary>
        public Color TextColor
        {
            get => _TextColor;
            internal set
            {
                if (value == _TextColor)
                {
                    return;
                }

                _TextColor = value;
                Invalidate();
            }
        }

        private Color _DotColor = Color.Transparent;
        /// <summary>
        /// 获取或设置当前 Option 连接点的颜色
        /// </summary>
        public Color DotColor
        {
            get => _DotColor;
            internal set
            {
                if (value == _DotColor)
                {
                    return;
                }

                _DotColor = value;
                Invalidate();
            }
        }

        private string _Text;
        /// <summary>
        /// 获取或设置当前 Option 显示文本
        /// 当AutoSize被设置时 无法修改此属性
        /// </summary>
        public string Text
        {
            get => _Text;
            internal set
            {
                if (value == _Text)
                {
                    return;
                }

                _Text = value;
                if (_Owner == null)
                {
                    return;
                }

                _Owner.BuildSize(true, true, true);
            }
        }

        /// <summary>
        /// 获取当前 Option 连接点的左边坐标
        /// </summary>
        public int DotLeft { get; internal set; }

        /// <summary>
        /// 获取当前 Option 连接点的上边坐标
        /// </summary>
        public int DotTop { get; internal set; }

        /// <summary>
        /// 获取当前 Option 连接点的宽度
        /// </summary>
        public int DotSize { get; protected set; }

        private Rectangle _TextRectangle;
        /// <summary>
        /// 获取当前 Option 文本区域
        /// </summary>
        public Rectangle TextRectangle
        {
            get => _TextRectangle;
            internal set => _TextRectangle = value;
        }

        private object _Data;
        /// <summary>
        /// 获取或者设置当前 Option 所包含的数据
        /// </summary>
        public object Data
        {
            get => _Data;
            set
            {
                if (value != null)
                {
                    if (DataType == null)
                    {
                        return;
                    }

                    Type t = value.GetType();
                    if (t != DataType && !t.IsSubclassOf(DataType))
                    {
                        throw new ArgumentException("无效数据类型 数据类型必须为指定的数据类型或其子类");
                    }
                }
                _Data = value;
            }
        }

        /// <summary>
        /// 获取当前 Option 数据类型
        /// </summary>
        public Type DataType { get; set; }

        //private Rectangle _DotRectangle;
        /// <summary>
        /// 获取当前 Option 连接点的区域
        /// </summary>
        public Rectangle DotRectangle => new Rectangle(DotLeft, DotTop, DotSize, DotSize);
        /// <summary>
        /// 获取当前 Option 被连接的个数
        /// </summary>
        public int ConnectionCount => m_hs_connected.Count;
        /// <summary>
        /// 获取当前 Option 所连接的 Option 集合
        /// </summary>
        internal HashSet<STNodeOption> ConnectedOption => m_hs_connected;

        #endregion Properties
        /// <summary>
        /// 保存已经被连接的点
        /// </summary>
        protected HashSet<STNodeOption> m_hs_connected;

        #region Constructor

        private STNodeOption() { }

        /// <summary>
        /// 构造一个 Option
        /// </summary>
        /// <param name="strText">显示文本</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="bSingle">是否为单连接</param>
        public STNodeOption(string strText, Type dataType, bool bSingle)
        {
            DotSize = 10;
            m_hs_connected = new HashSet<STNodeOption>();
            DataType = dataType ?? throw new ArgumentNullException("指定的数据类型不能为空");
            _Text = strText;
            IsSingle = bSingle;
        }

        #endregion Constructor

        #region Event

        /// <summary>
        /// 当被连接时候发生
        /// </summary>
        public event STNodeOptionEventHandler Connected;
        /// <summary>
        /// 当连接开始发生时发生
        /// </summary>
        public event STNodeOptionEventHandler Connecting;
        /// <summary>
        /// 当连接断开时候发生
        /// </summary>
        public event STNodeOptionEventHandler DisConnected;
        /// <summary>
        /// 当连接开始断开时发生
        /// </summary>
        public event STNodeOptionEventHandler DisConnecting;
        /// <summary>
        /// 当有数据传递时候发生
        /// </summary>
        public event STNodeOptionEventHandler DataTransfer;

        #endregion Event

        #region protected
        /// <summary>
        /// 重绘整个控件
        /// </summary>
        protected void Invalidate()
        {
            if (_Owner == null)
            {
                return;
            }

            _Owner.Invalidate();
        }
        /*
         * 开始我认为应当只有输入类型的选项才具有事件 因为输入是被动的 而输出则是主动的
         * 但是后来发现 比如在STNodeHub中输出节点就用到了事件
         * 以防万一所以这里代码注释起来了 也并不是很有问题 输出选项不注册事件也是一样的效果
         */
        protected internal virtual void OnConnected(STNodeOptionEventArgs e)
        {
            Connected?.Invoke(this, e);
        }
        protected internal virtual void OnConnecting(STNodeOptionEventArgs e)
        {
            Connecting?.Invoke(this, e);
        }
        protected internal virtual void OnDisConnected(STNodeOptionEventArgs e)
        {
            DisConnected?.Invoke(this, e);
        }
        protected internal virtual void OnDisConnecting(STNodeOptionEventArgs e)
        {
            DisConnecting?.Invoke(this, e);
        }
        protected internal virtual void OnDataTransfer(STNodeOptionEventArgs e)
        {
            DataTransfer?.Invoke(this, e);
        }
        protected void STNodeEidtorConnected(STNodeEditorOptionEventArgs e)
        {
            if (_Owner == null)
            {
                return;
            }

            if (_Owner.Owner == null)
            {
                return;
            }

            _Owner.Owner.OnOptionConnected(e);
        }
        protected void STNodeEidtorDisConnected(STNodeEditorOptionEventArgs e)
        {
            if (_Owner == null)
            {
                return;
            }

            if (_Owner.Owner == null)
            {
                return;
            }

            _Owner.Owner.OnOptionDisConnected(e);
        }
        /// <summary>
        /// 当前 Option 开始连接目标 Option
        /// </summary>
        /// <param name="op">需要连接的 Option</param>
        /// <returns>是否允许继续操作</returns>
        protected virtual bool ConnectingOption(STNodeOption op)
        {
            if (_Owner == null)
            {
                return false;
            }

            if (_Owner.Owner == null)
            {
                return false;
            }

            STNodeEditorOptionEventArgs e = new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.Connecting);
            _Owner.Owner.OnOptionConnecting(e);
            OnConnecting(new STNodeOptionEventArgs(true, op, ConnectionStatus.Connecting));
            op.OnConnecting(new STNodeOptionEventArgs(false, this, ConnectionStatus.Connecting));
            return e.Continue;
        }
        /// <summary>
        /// 当前 Option 开始断开目标 Option
        /// </summary>
        /// <param name="op">需要断开的 Option</param>
        /// <returns>是否允许继续操作</returns>
        protected virtual bool DisConnectingOption(STNodeOption op)
        {
            if (_Owner == null)
            {
                return false;
            }

            if (_Owner.Owner == null)
            {
                return false;
            }

            STNodeEditorOptionEventArgs e = new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.DisConnecting);
            _Owner.Owner.OnOptionDisConnecting(e);
            OnDisConnecting(new STNodeOptionEventArgs(true, op, ConnectionStatus.DisConnecting));
            op.OnDisConnecting(new STNodeOptionEventArgs(false, this, ConnectionStatus.DisConnecting));
            return e.Continue;
        }

        #endregion protected

        #region public
        /// <summary>
        /// 当前 Option 连接目标 Option
        /// </summary>
        /// <param name="op">需要连接的 Option</param>
        /// <returns>连接结果</returns>
        public virtual ConnectionStatus ConnectOption(STNodeOption op)
        {
            if (!ConnectingOption(op))
            {
                STNodeEidtorConnected(new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.Reject));
                return ConnectionStatus.Reject;
            }

            ConnectionStatus v = CanConnect(op);
            if (v != ConnectionStatus.Connected)
            {
                STNodeEidtorConnected(new STNodeEditorOptionEventArgs(op, this, v));
                return v;
            }
            v = op.CanConnect(this);
            if (v != ConnectionStatus.Connected)
            {
                STNodeEidtorConnected(new STNodeEditorOptionEventArgs(op, this, v));
                return v;
            }
            _ = op.AddConnection(this, false);
            _ = AddConnection(op, true);
            ControlBuildLinePath();

            STNodeEidtorConnected(new STNodeEditorOptionEventArgs(op, this, v));
            return v;
        }
        /// <summary>
        /// 检测当前 Option 是否可以连接目标 Option
        /// </summary>
        /// <param name="op">需要连接的 Option</param>
        /// <returns>检测结果</returns>
        public virtual ConnectionStatus CanConnect(STNodeOption op)
        {
            if (this == STNodeOption.Empty || op == STNodeOption.Empty)
            {
                return ConnectionStatus.EmptyOption;
            }

            if (IsInput == op.IsInput)
            {
                return ConnectionStatus.SameInputOrOutput;
            }

            if (op.Owner == null || _Owner == null)
            {
                return ConnectionStatus.NoOwner;
            }

            if (op.Owner == _Owner)
            {
                return ConnectionStatus.SameOwner;
            }

            if (_Owner.LockOption || op._Owner.LockOption)
            {
                return ConnectionStatus.Locked;
            }

            if (IsSingle && m_hs_connected.Count == 1)
            {
                return ConnectionStatus.SingleOption;
            }

            if (op.IsInput && STNodeEditor.CanFindNodePath(op.Owner, _Owner))
            {
                return ConnectionStatus.Loop;
            }

            return m_hs_connected.Contains(op)
                ? ConnectionStatus.Exists
                : IsInput && op.DataType != DataType && !op.DataType.IsSubclassOf(DataType)
                ? ConnectionStatus.ErrorType
                : ConnectionStatus.Connected;
        }
        /// <summary>
        /// 当前 Option 断开目标 Option
        /// </summary>
        /// <param name="op">需要断开的 Option</param>
        /// <returns></returns>
        public virtual ConnectionStatus DisConnectOption(STNodeOption op)
        {
            if (!DisConnectingOption(op))
            {
                STNodeEidtorDisConnected(new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.Reject));
                return ConnectionStatus.Reject;
            }

            if (op.Owner == null)
            {
                return ConnectionStatus.NoOwner;
            }

            if (_Owner == null)
            {
                return ConnectionStatus.NoOwner;
            }

            if (op.Owner.LockOption && _Owner.LockOption)
            {
                STNodeEidtorDisConnected(new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.Locked));
                return ConnectionStatus.Locked;
            }
            _ = op.RemoveConnection(this, false);
            _ = RemoveConnection(op, true);
            ControlBuildLinePath();

            STNodeEidtorDisConnected(new STNodeEditorOptionEventArgs(op, this, ConnectionStatus.DisConnected));
            return ConnectionStatus.DisConnected;
        }
        /// <summary>
        /// 断开当前 Option 的所有连接
        /// </summary>
        public void DisConnectionAll()
        {
            if (DataType == null)
            {
                return;
            }

            STNodeOption[] arr = m_hs_connected.ToArray();
            foreach (STNodeOption v in arr)
            {
                _ = DisConnectOption(v);
            }
        }
        /// <summary>
        /// 获取当前 Option 所连接的 Option 集合
        /// </summary>
        /// <returns>如果为null 则表示不存在所有者 否则返回集合</returns>
        public List<STNodeOption> GetConnectedOption(bool force = false)
        {
            if (DataType == null)
            {
                return null;
            }

            if (!IsInput)
            {
                return m_hs_connected.ToList();
            }
            //force - мой прикол, не понимаю почему нельзя получить так же просто инпут ноды
            if (force)
            {
                return m_hs_connected.ToList();
            }

            List<STNodeOption> lst = new List<STNodeOption>();
            if (_Owner == null)
            {
                return null;
            }

            if (_Owner.Owner == null)
            {
                return null;
            }

            foreach (ConnectionInfo v in _Owner.Owner.GetConnectionInfo())
            {
                if (v.Output == this)
                {
                    lst.Add(v.Input);
                }
            }
            return lst;
        }
        /// <summary>
        /// 向当前 Option 所连接的所有 Option 投递数据
        /// </summary>
        public void TransferData()
        {
            if (DataType == null)
            {
                return;
            }

            foreach (STNodeOption v in m_hs_connected)
            {
                v.OnDataTransfer(new STNodeOptionEventArgs(true, this, ConnectionStatus.Connected));
            }
        }
        /// <summary>
        /// 向当前 Option 所连接的所有 Option 投递数据
        /// </summary>
        /// <param name="data">需要投递的数据</param>
        public void TransferData(object data)
        {
            if (DataType == null)
            {
                return;
            }

            Data = data;   //不是this._Data
            foreach (STNodeOption v in m_hs_connected)
            {
                v.OnDataTransfer(new STNodeOptionEventArgs(true, this, ConnectionStatus.Connected));
            }
        }
        /// <summary>
        /// 向当前 Option 所连接的所有 Option 投递数据
        /// </summary>
        /// <param name="data">需要投递的数据</param>
        /// <param name="bDisposeOld">是否释放旧数据</param>
        public void TransferData(object data, bool bDisposeOld)
        {
            if (bDisposeOld && _Data != null)
            {
                if (_Data is IDisposable)
                {
                    ((IDisposable)_Data).Dispose();
                }

                _Data = null;
            }
            TransferData(data);
        }

        #endregion public

        #region internal

        private bool AddConnection(STNodeOption op, bool bSponsor)
        {
            if (DataType == null)
            {
                return false;
            }

            bool b = m_hs_connected.Add(op);
            OnConnected(new STNodeOptionEventArgs(bSponsor, op, ConnectionStatus.Connected));
            if (IsInput)
            {
                OnDataTransfer(new STNodeOptionEventArgs(bSponsor, op, ConnectionStatus.Connected));
            }

            return b;
        }

        private bool RemoveConnection(STNodeOption op, bool bSponsor)
        {
            if (DataType == null)
            {
                return false;
            }

            bool b = false;
            if (m_hs_connected.Contains(op))
            {
                b = m_hs_connected.Remove(op);
                if (IsInput)
                {
                    OnDataTransfer(new STNodeOptionEventArgs(bSponsor, op, ConnectionStatus.DisConnected));
                }

                OnDisConnected(new STNodeOptionEventArgs(bSponsor, op, ConnectionStatus.Connected));
            }
            return b;
        }

        #endregion internal

        #region private

        private void ControlBuildLinePath()
        {
            if (Owner == null)
            {
                return;
            }

            if (Owner.Owner == null)
            {
                return;
            }

            Owner.Owner.BuildLinePath();
        }

        #endregion
    }
}
