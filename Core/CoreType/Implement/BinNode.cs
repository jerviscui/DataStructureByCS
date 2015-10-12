using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BinNode<T> : IBinNode<T>, IComparable<BinNode<T>> where T : IComparable<T>
    {
        private BinNode<T> _lChild;

        private BinNode<T> _rChild;

        private BinNode<T> _parent;

        private T _data;

        public BinNode(T data, BinNode<T> parent = null)
        {
            _data = data;
            _parent = parent;
            Height = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public BinNode(T data, BinNode<T> parent, BinNode<T> lChild, BinNode<T> rChild, int height)
        {
            _lChild = lChild;
            _rChild = rChild;
            _parent = parent;
            _data = data;
            Height = height;
        }

        #region Private Method

        private void VistAlongLeftBranch(BinNode<T> node, Action<T> action, ref MyStack<BinNode<T>> stack)
        {
            while (node != null)
            {
                action(node._data);
                if (node._rChild != null)
                {
                    stack.Push(node._rChild);
                }
                node = node._lChild;
            }
        }

        private void GoAlongLeftBranch(BinNode<T> node, ref MyStack<BinNode<T>> stack)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node._lChild;
            }
        }

        private void GoAlongLeftAndRight(BinNode<T> node, ref MyStack<BinNode<T>> stack)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node._lChild;
            }
        }

        private BinNode<T> GetLeftestNode(BinNode<T> node)
        {
            while (node._lChild != null)
            {
                node = node._lChild;
            }

            return node;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// all children number
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            int size = 1;
            if (_lChild != null)
            {
                size += _lChild.Size();
            }
            if (_rChild != null)
            {
                size += _rChild.Size();
            }

            return size;
        }

        /// <summary>
        /// hight of the tree with this for root
        /// </summary>
        public int Height { get; set; }

        public NodeColor Color { get; set; }

        public IBinNode<T> LChild
        {
            get
            {
                return _lChild;
            }
            set { _lChild = value as BinNode<T>; }
        }

        public IBinNode<T> RChild
        {
            get
            {
                return _rChild;
            }
            set { _rChild = value as BinNode<T>; }
        }

        public IBinNode<T> Parent
        {
            get
            {
                return _parent;
            }
            set { _parent = value as BinNode<T>; }
        }

        public T Data
        {
            get {return _data;}
            set { _data = value; }
        }

        /// <summary>
        /// insert left child 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IBinNode<T> InsertAsLC(T data)
        {
            if (_lChild != null)
            {
                throw new NotSupportedException("left child alread has value");
            }

            return _lChild = new BinNode<T>(data, this);
        }

        /// <summary>
        /// insert right child
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IBinNode<T> InsertAsRC(T data)
        {
            if (_rChild != null)
            {
                throw new NotSupportedException("right child alread has value");
            }

            return _rChild = new BinNode<T>(data, this);
        }

        /// <summary>
        /// in sequence traversal of the succeed
        /// 按照中序遍历的排列顺序找出该节点下一个元素
        /// 如果有右孩子，返回右孩子树左链端点
        /// 如果父节点是爷爷节点的左孩子，返回爷爷节点
        /// 如果父节点是右孩子，溯源到祖先是左孩子的情况，返回祖先的父节点 
        /// </summary>
        /// <returns></returns>
        public IBinNode<T> Succ()
        {
            //has right 
            if (_rChild != null)
            {
                return GetLeftestNode(_rChild);
            }

            //is left child
            if (_parent != null && _parent._lChild == this)
            {
                return _parent;
            }

            //is right child
            if (_parent != null)
            {
                BinNode<T> node = _parent;
                while (node._parent != null)
                {
                    if (node._parent._lChild == node)
                    {
                        break;
                    }

                    node = node._parent;
                }

                return node._parent;
            }

            return null;
        }

        /// <summary>
        /// levelorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravLevel(Action<T> action)
        {
            MyQueue<BinNode<T>> queue = new MyQueue<BinNode<T>>();
            queue.EnQueue(this);

            while (!queue.Empty())
            {
                BinNode<T> node = queue.DeQueue();
                action(node._data);
                if (node._lChild != null)
                {
                    queue.EnQueue(node._lChild);
                }
                if (node._rChild != null)
                {
                    queue.EnQueue(node._rChild);
                }
            }
        }

        /// <summary>
        /// preorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPre(Action<T> action)
        {
            MyStack<BinNode<T>> stack = new MyStack<BinNode<T>>();

            //by left branch
            VistAlongLeftBranch(this, action, ref stack);
            while (!stack.Empty())
            {
                VistAlongLeftBranch(stack.Pop(), action, ref stack);
            }
            return;

            stack.Push(this);
            while (!stack.Empty())
            {
                BinNode<T> node = stack.Pop();
                action(node._data);
                if (node._rChild != null)
                {
                    stack.Push(node._rChild);
                }
                if (node._lChild != null)
                {
                    stack.Push(node._lChild);
                }
            }
            return;

            action(_data);
            if (_lChild != null)
            {
                _lChild.TravPre(action);
            }
            if (_rChild != null)
            {
                _rChild.TravPre(action);
            }
        }

        /// <summary>
        /// in sequence traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravIn(Action<T> action)
        {
            MyStack<BinNode<T>> stack = new MyStack<BinNode<T>>();
            BinNode<T> node = this;
            while (true)
            {
                GoAlongLeftBranch(node, ref stack);
                if (stack.Empty())
                {
                    break;
                }
                node = stack.Pop();
                action(node._data);
                node = node._rChild;
            }
            return;

            if (this._lChild != null)
            {
                this._lChild.TravIn(action);
            }
            action(this._data);
            if (this._rChild != null)
            {
                this._rChild.TravIn(action);
            }
        }

        /// <summary>
        /// postorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPost(Action<T> action)
        {
            MyStack<BinNode<T>> stack = new MyStack<BinNode<T>>();
            //从右侧分支返回时，标记前一次操作对象是否是当前节点的右孩子
            BinNode<T> right = this;
            GoAlongLeftBranch(this, ref stack);
            while (!stack.Empty())
            {
                BinNode<T> node = stack.Top();
                //has right
                if (node._rChild != null && node._rChild != right)
                {
                    GoAlongLeftBranch(node._rChild, ref stack);
                    continue;
                }

                //has not left and right
                //this is middle
                node = stack.Pop();
                action(node._data);
                right = node;

                //go to right brother if the node is left child
                if (node._parent != null && node._parent._rChild != node)
                {
                    GoAlongLeftBranch(node._parent._rChild, ref stack);
                }
            }
            return;

            if (this._lChild != null)
            {
                this._lChild.TravPost(action);
            }
            if (this._rChild != null)
            {
                this._rChild.TravPost(action);
            }
            action(this._data);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
        /// </returns>
        /// <param name="other">An object to compare with this instance. </param>
        public int CompareTo(BinNode<T> other)
        {
            return this._data.CompareTo(other._data);
        }

        #endregion


    }
}
