using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class AVL<T> : BST<T> where T : IComparable<T>
    {
        public AVL(BinNode<T> node) : base(node)
        {

        }

        /// <summary>
        /// 理想平衡
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool Balanced(IBinNode<T> node)
        {
            if (node.LChild != null && node.RChild != null)
            {
                return node.LChild.Height == node.RChild.Height;
            }

            return false;
        }

        /// <summary>
        /// 平衡因子
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int BalFac(IBinNode<T> node)
        {
            return (node.LChild != null ? node.LChild.Height : -1) -
                (node.RChild != null ? node.RChild.Height : -1);
        }

        /// <summary>
        /// 适度平衡
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool AvlBalanced(IBinNode<T> node)
        {
            return -2 < BalFac(node) && BalFac(node) < 2;
        }

        private IBinNode<T> TallerChild(IBinNode<T> node)
        {
            if (BalFac(node) > 0)
            {
                return node.LChild;
            }

            return node.RChild;
        }

        #region Public

        public override IBinNode<T> Insert(T data)
        {
            IBinNode<T> node = Search(data);
            if (node == null)
            {
                if (_hot.Data.CompareTo(data) < 0)
                {
                    node = _hot.InsertAsRC(data);
                }
                else
                {
                    node = _hot.InsertAsLC(data);
                }
                _size++;
                //AVL make tree is BBST, dont have this step
                //UpdateHeightAbove(node);
            }

            for (IBinNode<T> g = node.Parent; g != null; g = g.Parent)
            {
                if (!AvlBalanced(g))
                {
                    //重平衡
                    RotateAt(TallerChild(TallerChild(g)));
                    break;
                }
                else
                {
                    UpdateHeight(g);
                }
            }

            return node;
        }

        public override bool Remove(T data)
        {
            IBinNode<T> node = Search(data);
            if (node == null)
            {
                return false;
            }
            node = RemoveAt(node, ref _hot);

            for (IBinNode<T> g = _hot; g != null; g = g.Parent)
            {
                if (!AvlBalanced(g))
                {
                    RotateAt(TallerChild(TallerChild(g)));
                    UpdateHeight(g);
                }
            }

            return true;
        }

        #endregion
    }
}
