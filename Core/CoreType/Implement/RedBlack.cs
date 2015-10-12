using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class RedBlack<T> : Bst<T> where T : IComparable<T>
    {
        private bool IsBlack(IBinNode<T> node)
        {
            return true;
        }

        public RedBlack(BinNode<T> node) : base(node)
        {

        }

        public override IBinNode<T> Search(T data)
        {
            return base.Search(data);
        }

        public override IBinNode<T> Insert(T data)
        {
            IBinNode<T> node = Search(data);
            if (node != null)
            {
                return node;
            }

            node = new BinNode<T>(data, Hot as BinNode<T>, null, null, -1);
            _size++;

            SolveDoubleRed(node);

            return node;
        }

        public override bool Remove(T data)
        {
            return base.Remove(data);
        }

        /// <summary>
        /// virtual
        /// </summary>
        /// <returns></returns>
        protected override int UpdateHeight(IBinNode<T> node)
        {
            int height = Math.Max(node.LChild != null ? node.LChild.Height : -1,
                node.RChild != null ? node.RChild.Height : -1);

            if (IsBlack(node))
            {
                node.Height++;
            }

            return node.Height;
        }

        protected void SolveDoubleRed(IBinNode<T> node)
        {
            if (Hot == null)
            {
                node.Color = NodeColor.Black;
                _root = node;
                
                return;
            }

            var p = node.Parent;
            var g = p.Parent;
            if (g == null)
            {
                //p is root
                if (p.Data.CompareTo(node.Data) < 0)
                {
                    //todo if RChild has value?
                    p.RChild = node;
                }
                else
                {
                    p.LChild = node;
                }

                return;
            }
            var u = g.LChild == p ? g.RChild : g.LChild;

            if (u.Color == NodeColor.Red)
            {
                //3+4
            }
            //else u.Color == Black
            else
            {
                g.Color = NodeColor.Red;
                p.Color = NodeColor.Black;
                u.Color = NodeColor.Black;
                
                SolveDoubleRed(g);
            }
        }

        protected void SolveDoubleBlack(IBinNode<T> node)
        {
            
        }
    }
}
