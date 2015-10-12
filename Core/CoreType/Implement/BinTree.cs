using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BinTree<T> : IBinTree<T> where T : IComparable<T>
    {
        protected int _size;

        protected IBinNode<T> _root;

        public BinTree(BinNode<T> node)
        {
            _root = node;
            if (node != null)
            {
                _size = 1;
            }
        }

        #region Public Method
        /// <summary>
        /// virtual
        /// </summary>
        /// <returns></returns>
        protected virtual int UpdateHeight(IBinNode<T> node)
        {
            int height = 1 + Math.Max(node.LChild != null ? node.LChild.Height : -1,
                node.RChild != null ? node.RChild.Height : -1);

            return node.Height = height;
        }

        /// <summary>
        /// update the height of all the parent nodes
        /// </summary>
        /// <param name="node"></param>
        public void UpdateHeightAbove(IBinNode<T> node)
        {
            //可优化，如果当前节点高度无变化 即可以停止更新上层节点
            while (node != null)
            {
                int height = node.Height;
                UpdateHeight(node);
                if (height == node.Height)
                {
                    break;
                }
                node = node.Parent;
            }
        }

        public int Size()
        {
            return _size;
        }

        public int Height()
        {
            if (_root == null)
            {
                return -1;
            }

            return _root.Height;
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
            node.InsertAsLC(data);
            UpdateHeightAbove(node);
            _size ++;

            return node.LChild;
        }

        public IBinNode<T> InsertAsRC(IBinNode<T> node, T data)
        {
            node.InsertAsRC(data);
            UpdateHeightAbove(node);
            _size ++;

            return node.LChild;
        }

        /// <summary>
        /// levelorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravLevel(Action<T> action)
        {
            if (!Empty())
            {
                _root.TravLevel(action);
            }
        }

        /// <summary>
        /// preorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPre(Action<T> action)
        {
            if (!Empty())
            {
                _root.TravPre(action);
            }
        }

        /// <summary>
        /// in sequence traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravIn(Action<T> action)
        {
            if (!Empty())
            {
                _root.TravIn(action);
            }
        }

        /// <summary>
        /// postorder traversal
        /// </summary>
        /// <param name="action"></param>
        public void TravPost(Action<T> action)
        {
            if (!Empty())
            {
                _root.TravPost(action);
            }
        }

        #endregion
    }
}
