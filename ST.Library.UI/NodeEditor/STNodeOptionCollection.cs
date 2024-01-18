using System;

using System.Collections;

namespace ST.Library.UI.NodeEditor
{
    public class STNodeOptionCollection : IList, ICollection, IEnumerable
    {
        public int Count { get; private set; }
        private STNodeOption[] m_options;
        private readonly STNode m_owner;

        private readonly bool m_isInput;     //当前集合是否是存放的是输入点

        internal STNodeOptionCollection(STNode owner, bool isInput)
        {
            m_owner = owner ?? throw new ArgumentNullException("所有者不能为空");
            m_isInput = isInput;
            m_options = new STNodeOption[4];
        }

        public STNodeOption Add(string strText, Type dataType, bool bSingle)
        {
            //not do this code -> out of bounds
            //return m_options[this.Add(new STNodeOption(strText, dataType, bSingle))];
            int nIndex = Add(new STNodeOption(strText, dataType, bSingle));
            return m_options[nIndex];
        }

        public int Add(STNodeOption option)
        {
            if (option == null)
            {
                throw new ArgumentNullException("添加对象不能为空");
            }

            EnsureSpace(1);
            int nIndex = option == STNodeOption.Empty ? -1 : IndexOf(option);
            if (-1 == nIndex)
            {
                nIndex = Count;
                option.Owner = m_owner;
                option.IsInput = m_isInput;
                m_options[Count++] = option;
                Invalidate();
            }
            return nIndex;
        }

        public void AddRange(STNodeOption[] options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("添加对象不能为空");
            }

            EnsureSpace(options.Length);
            foreach (STNodeOption op in options)
            {
                if (op == null)
                {
                    throw new ArgumentNullException("添加对象不能为空");
                }

                if (-1 == IndexOf(op))
                {
                    op.Owner = m_owner;
                    op.IsInput = m_isInput;
                    m_options[Count++] = op;
                }
            }
            Invalidate();
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                m_options[i].Owner = null;
            }

            Count = 0;
            m_options = new STNodeOption[4];
            Invalidate();
        }

        public bool Contains(STNodeOption option)
        {
            return IndexOf(option) != -1;
        }

        public int IndexOf(STNodeOption option)
        {
            return Array.IndexOf<STNodeOption>(m_options, option);
        }

        public void Insert(int index, STNodeOption option)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("索引越界");
            }

            if (option == null)
            {
                throw new ArgumentNullException("插入对象不能为空");
            }

            EnsureSpace(1);
            for (int i = Count; i > index; i--)
            {
                m_options[i] = m_options[i - 1];
            }

            option.Owner = m_owner;
            m_options[index] = option;
            Count++;
            Invalidate();
        }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public void Remove(STNodeOption option)
        {
            int nIndex = IndexOf(option);
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
            m_options[index].Owner = null;
            for (int i = index, Len = Count; i < Len; i++)
            {
                m_options[i] = m_options[i + 1];
            }

            Invalidate();
        }

        public STNodeOption this[int index]
        {
            get => index < 0 || index >= Count ? throw new IndexOutOfRangeException("索引越界") : m_options[index];
            set => throw new InvalidOperationException("禁止重新赋值元素");
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("数组不能为空");
            }

            m_options.CopyTo(array, index);
        }

        public bool IsSynchronized => true;

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            for (int i = 0, Len = Count; i < Len; i++)
            {
                yield return m_options[i];
            }
        }
        /// <summary>
        /// 确认空间是否足够 空间不足扩大容量
        /// </summary>
        /// <param name="elements">需要增加的个数</param>
        private void EnsureSpace(int elements)
        {
            if (elements + Count > m_options.Length)
            {
                STNodeOption[] arrTemp = new STNodeOption[Math.Max(m_options.Length * 2, elements + Count)];
                m_options.CopyTo(arrTemp, 0);
                m_options = arrTemp;
            }
        }

        protected void Invalidate()
        {
            if (m_owner != null && m_owner.Owner != null)
            {
                m_owner.BuildSize(true, true, true);
                //m_owner.Invalidate();//.Owner.Invalidate();
            }
        }
        //===================================================================================
        int IList.Add(object value)
        {
            return Add((STNodeOption)value);
        }

        void IList.Clear()
        {
            Clear();
        }

        bool IList.Contains(object value)
        {
            return Contains((STNodeOption)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((STNodeOption)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (STNodeOption)value);
        }

        bool IList.IsFixedSize => IsFixedSize;

        bool IList.IsReadOnly => IsReadOnly;

        void IList.Remove(object value)
        {
            Remove((STNodeOption)value);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (STNodeOption)value;
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

        public STNodeOption[] ToArray()
        {
            STNodeOption[] ops = new STNodeOption[Count];
            for (int i = 0; i < ops.Length; i++)
            {
                ops[i] = m_options[i];
            }

            return ops;
        }
    }
}