using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BinNode<T> : IBinNode<T>
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

        public IBinNode<T> LChild()
        {
            return _lChild;
        }

        public IBinNode<T> RChild()
        {
            return _rChild;
        }

        public IBinNode<T> Parent()
        {
            return _parent;
        }

        /// <summary>
        /// insert left child 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IBinNode<T> insertAsLC(T data)
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
        public IBinNode<T> insertAsRC(T data)
        {
            return _rChild = new BinNode<T>(data, this);
        }

        /// <summary>
        /// in sequence traversal of the succeed
        /// </summary>
        /// <returns></returns>
        public IBinNode<T> Succ()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// levelorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravLevel(Action action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// preorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPre(Action action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// in sequence traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravIn(Action action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// postorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPost(Action action)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
