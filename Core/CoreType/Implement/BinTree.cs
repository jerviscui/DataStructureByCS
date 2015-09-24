using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BinTree<T> : IBinTree<T>
    {
        private int _size;

        private BinNode<T> _root;

        public BinTree()
        {
            _size = 0;
        }

        #region Public Method
        /// <summary>
        /// virtual
        /// </summary>
        /// <returns></returns>
        public virtual int UpdateHeight(IBinNode<T> node)
        {
            return node.Height = 1 + Math.Max(node.LChild() != null ? node.LChild().Height : -1, 
                node.RChild() != null ? node.RChild().Height : -1);
        }

        /// <summary>
        /// update the height of all the parent nodes
        /// </summary>
        /// <param name="node"></param>
        public void UpdateHeightAbove(IBinNode<T> node)
        {
            while (node != null)
            {
                UpdateHeight(node);
                node = node.Parent();
            }
        }

        public int Size()
        {
            return _size;
        }

        public bool Empty()
        {
            return _root == null;
        }

        public IBinNode<T> Root()
        {
            return _root;
        }

        public IBinNode<T> InsertAsLC(IBinNode<T> node, T data)
        {
            node.insertAsLC(data);
            UpdateHeightAbove(node);
            _size ++;

            return node.LChild();
        }

        public IBinNode<T> InsertAsRC(IBinNode<T> node, T data)
        {
            node.insertAsRC(data);
            UpdateHeightAbove(node);
            _size ++;

            return node.LChild();
        }

        #endregion
    }
}
