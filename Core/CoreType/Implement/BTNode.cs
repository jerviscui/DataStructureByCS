using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BTNode<T> : IBTNode<T>, IComparable<BTNode<T>> where T : IComparable<T>
    {
        private BTNode<T> _parent;

        private Vector<T> _key;

        private Vector<BTNode<T>> _children;

        public BTNode()
        {
            _key = new MyStack<T>();
            _children = new MyStack<BTNode<T>>();
        }

        public BTNode(T e, BTNode<T> lc = null, BTNode<T> rc = null) : base()
        {
            _key.Insert(0, e);
            _children.Insert(0, lc);
            _children.Insert(1, rc);
            if (lc != null)
            {
                lc._parent = this;
            }
            if (rc != null)
            {
                rc._parent = this;
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
        /// </returns>
        /// <param name="other">An object to compare with this instance. </param>
        public int CompareTo(BTNode<T> other)
        {
            throw new NotImplementedException();
        }

        public IBTNode<T> Parent
        {
            get { return _parent as IBTNode<T>; }
            set { _parent = value as BTNode<T>; }
        }

        public IVector<T> Key
        {
            get { return _key; }
            set { _key = value as Vector<T>; }
        }

        public IVector<IBTNode<T>> Children {
            get { return _children as IVector<IBTNode<T>>; }
            set { _children = value as Vector<BTNode<T>>; }
        }
    }
}
