using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BST<T> : BinTree<T>, IBST<T> where T : IComparable<T>
    {
        public BST(BinNode<T> node) : base(node)
        {

        }

        private IBinNode<T> SearchIn(IBinNode<T> v, T data, ref IBinNode<T> hot)
        {
            if (v == null || v.Data.CompareTo(data) == 0)
            {
                return v;
            }
            hot = v;
            return SearchIn(v.Data.CompareTo(data) < 0 ? v.RChild : v.LChild, data, ref hot);
        }

        protected IBinNode<T> RemoveAt(IBinNode<T> node, ref IBinNode<T> hot)
        {
            hot = node.Parent;
            IBinNode<T> succ = null;

            if (node.RChild == null)
            {
                succ = node.LChild;
            }
            else if (node.LChild == null)
            {
                succ = node.RChild;
            }
            else
            {
                succ = node.Succ();
                T data = succ.Data;
                succ.Data = node.Data;
                node.Data = data;
                hot = succ.Parent;
                return RemoveAt(succ, ref hot);
            }

            if (hot.LChild == node)
            {
                hot.LChild = succ;
            }
            else
            {
                hot.RChild = succ;
            }
            if (succ != null)
            {
                succ.Parent = hot;
            }

            return succ;
        }

        #region Public
        public virtual IBinNode<T> Search(T data)
        {
            _hot = null;
            return SearchIn(Root(), data, ref _hot);
        }

        public virtual IBinNode<T> Insert(T data)
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
                UpdateHeightAbove(node);
            }

            return node;
        }

        public virtual bool Remove(T data)
        {
            IBinNode<T> node = Search(data);
            if (node == null)
            {
                return false;
            }
            node = RemoveAt(node, ref _hot);
            UpdateHeightAbove(node ?? _hot);
            _size--;

            return true;
        }

        #endregion

        protected IBinNode<T> _hot;

        /// <summary>
        /// 3节点4子树
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t0"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <returns></returns>
        protected IBinNode<T> Connect34(IBinNode<T> a, IBinNode<T> b, IBinNode<T> c,
            IBinNode<T> t0, IBinNode<T> t1, IBinNode<T> t2, IBinNode<T> t3)
        {
            a.LChild = t0;
            if (t0 != null) t0.Parent = a;
            a.RChild = t1;
            if (t1 != null) t1.Parent = a;
            UpdateHeight(a);
            c.LChild = t2;
            if (t2 != null) t2.Parent = c;
            c.RChild = t3;
            if (t3 != null) t3.Parent = c;
            UpdateHeight(c);

            b.LChild = a;
            a.Parent = b;
            b.RChild = c;
            c.Parent = b;
            UpdateHeight(b);

            return b;
        }

        protected IBinNode<T> RotateAt(IBinNode<T> node)
        {
            IBinNode<T> v = node;
            IBinNode<T> p = v.Parent;
            IBinNode<T> g = p.Parent;

            IBinNode<T> result = null;
            IBinNode<T> gg = g.Parent;

            //use 3+4
            if (g.RChild == p)
            {
                if (p.RChild == v)
                {
                    result = Connect34(g, p, v, g.LChild, p.LChild, v.LChild, v.RChild);
                }
                else
                {
                    result = Connect34(g, v, p, g.LChild, v.LChild, v.RChild, p.RChild);
                }
            }
            else
            {
                if (p.LChild == v)
                {
                    result = Connect34(v, p, g, v.LChild, v.RChild, p.RChild, g.RChild);
                }
                else
                {
                    result = Connect34(p, v, g, p.LChild, v.LChild, v.RChild, g.RChild);
                }
            }

            result.Parent = gg;
            if (gg != null && gg.LChild == g)
            {
                gg.LChild = result;
            }
            if (gg != null && gg.RChild == g)
            {
                gg.RChild = result;
            }

            return result;

            //*zag
            if (g.RChild == p)
            {
                //zag zag
                if (p.RChild == v)
                {
                    if (p.LChild != null)
                    {
                        p.LChild.Parent = g;
                    }
                    g.RChild = p.LChild;
                    p.Parent = g.Parent;
                    g.Parent = p;
                    result = p;
                }
                //zig zag
                else
                {
                    //zig
                    if (v.RChild != null)
                    {
                        v.RChild.Parent = p;
                    }
                    p.LChild = v.RChild;
                    v.Parent = p.Parent;
                    p.Parent = v;
                    //zag
                    //下面的代码和递归一次是一样的效果，因为经过上面zig之后，整个变成zagzag形式 
                    //RotateAt(g);
                    if (v.LChild != null)
                    {
                        v.LChild.Parent = g;
                    }
                    g.RChild = v.LChild;
                    v.Parent = g.Parent;
                    g.Parent = v;
                    result = v;
                }
            }
            //*zig
            else
            {
                //zig zig
                if (p.LChild == v)
                {
                    if (p.RChild != null)
                    {
                        p.RChild.Parent = g;
                    }
                    g.LChild = p.RChild;
                    p.Parent = g.Parent;
                    g.Parent = p;
                    result = p;
                }
                //zag zig
                else
                {
                    //zag
                    if (v.LChild != null)
                    {
                        v.LChild.Parent = p;
                    }
                    p.RChild = v.LChild;
                    v.Parent = p.Parent;
                    p.Parent = v;
                    //zig
                    //同上，整个变成zigzig形式 
                    //RotateAt(g);
                    if (v.RChild != null)
                    {
                        v.RChild.Parent = g;
                    }
                    g.LChild = v.RChild;
                    v.Parent = g.Parent;
                    g.Parent = v;
                    result = v;
                }
            }

            result.Parent = gg;
            if (gg != null && gg.LChild == g)
            {
                gg.LChild = result;
            }
            if (gg != null && gg.RChild == g)
            {
                gg.RChild = result;
            }

            return result;
        }
    }
}
