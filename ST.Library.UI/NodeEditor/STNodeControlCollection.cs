using System;
using System.Collections;

namespace ST.Library.UI.NodeEditor
{
    public class STNodeControlCollection : IList, ICollection, IEnumerable
    {
        public int Count { get; private set; }
        private STNodeControl[] m_controls;
        private readonly STNode m_owner;

        internal STNodeControlCollection(STNode owner)
        {
            m_owner = owner ?? throw new ArgumentNullException("所有者不能为空");
            m_controls = new STNodeControl[4];
        }

        public int Add(STNodeControl control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("添加对象不能为空");
            }

            EnsureSpace(1);
            int nIndex = IndexOf(control);
            if (-1 == nIndex)
            {
                nIndex = Count;
                control.Owner = m_owner;
                m_controls[Count++] = control;
                Redraw();
            }
            return nIndex;
        }

        public void AddRange(STNodeControl[] controls)
        {
            if (controls == null)
            {
                throw new ArgumentNullException("添加对象不能为空");
            }

            EnsureSpace(controls.Length);
            foreach (STNodeControl op in controls)
            {
                if (op == null)
                {
                    throw new ArgumentNullException("添加对象不能为空");
                }

                if (-1 == IndexOf(op))
                {
                    op.Owner = m_owner;
                    m_controls[Count++] = op;
                }
            }
            Redraw();
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                m_controls[i].Owner = null;
            }

            Count = 0;
            m_controls = new STNodeControl[4];
            Redraw();
        }

        public bool Contains(STNodeControl option)
        {
            return IndexOf(option) != -1;
        }

        public int IndexOf(STNodeControl option)
        {
            return Array.IndexOf<STNodeControl>(m_controls, option);
        }

        public void Insert(int index, STNodeControl control)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("索引越界");
            }

            if (control == null)
            {
                throw new ArgumentNullException("插入对象不能为空");
            }

            EnsureSpace(1);
            for (int i = Count; i > index; i--)
            {
                m_controls[i] = m_controls[i - 1];
            }

            control.Owner = m_owner;
            m_controls[index] = control;
            Count++;
            Redraw();
        }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public void Remove(STNodeControl control)
        {
            int nIndex = IndexOf(control);
            if (nIndex != -1)
            {
                RemoveAt(nIndex);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("索引越界");
            }

            Count--;
            m_controls[index].Owner = null;
            for (int i = index, Len = Count; i < Len; i++)
            {
                m_controls[i] = m_controls[i + 1];
            }

            Redraw();
        }

        public STNodeControl this[int index]
        {
            get => index < 0 || index >= Count ? throw new IndexOutOfRangeException("索引越界") : m_controls[index];
            set => throw new InvalidOperationException("禁止重新赋值元素");
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("数组不能为空");
            }

            m_controls.CopyTo(array, index);
        }

        public bool IsSynchronized => true;

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            for (int i = 0, Len = Count; i < Len; i++)
            {
                yield return m_controls[i];
            }
        }
        /// <summary>
        /// 确认空间是否足够 空间不足扩大容量
        /// </summary>
        /// <param name="elements">需要增加的个数</param>
        private void EnsureSpace(int elements)
        {
            if (elements + Count > m_controls.Length)
            {
                STNodeControl[] arrTemp = new STNodeControl[Math.Max(m_controls.Length * 2, elements + Count)];
                m_controls.CopyTo(arrTemp, 0);
                m_controls = arrTemp;
            }
        }

        protected void Redraw()
        {
            if (m_owner != null && m_owner.Owner != null)
            {
                //m_owner.BuildSize();
                m_owner.Owner.Invalidate(m_owner.Owner.CanvasToControl(m_owner.Rectangle));
            }
        }
        //===================================================================================
        int IList.Add(object value)
        {
            return Add((STNodeControl)value);
        }

        void IList.Clear()
        {
            Clear();
        }

        bool IList.Contains(object value)
        {
            return Contains((STNodeControl)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((STNodeControl)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (STNodeControl)value);
        }

        bool IList.IsFixedSize => IsFixedSize;

        bool IList.IsReadOnly => IsReadOnly;

        void IList.Remove(object value)
        {
            Remove((STNodeControl)value);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (STNodeControl)value;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        int ICollection.Count => Count;

        bool ICollection.IsSynchronized => IsSynchronized;

        object ICollection.SyncRoot => SyncRoot;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
