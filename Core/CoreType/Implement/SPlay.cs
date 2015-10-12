using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Splay<T> : Bst<T> where T : IComparable<T>
    {
        public Splay(BinNode<T> node) : base(node)
        {
        }

        private bool IsLChild(IBinNode<T> node)
        {
            return node.Parent != null && node.Parent.LChild == node;
        }

        private bool IsRChild(IBinNode<T> node)
        {
            return node.Parent != null && node.Parent.RChild == node;
        }

        private void AttachAsLChild(IBinNode<T> node, IBinNode<T> child)
        {
            node.LChild = child;
            child.Parent = node;
        }

        private void AttachAsRChild(IBinNode<T> node, IBinNode<T> child)
        {
            node.RChild = child;
            child.Parent = node;
        }

        private IBinNode<T> MakeSplay(IBinNode<T> v)
        {
            if (v == null)
            {
                return null;
            }

            IBinNode<T> p = null;
            IBinNode<T> g = null;

            while ((p = v.Parent) != null && (g = p.Parent) != null)
            {
                IBinNode<T> gg = g.Parent;
                bool isL = IsLChild(g);
                bool isR = IsRChild(g);

                if (IsLChild(v))
                {
                    //zig zig
                    if (IsLChild(p))
                    {
                        AttachAsLChild(g, p.RChild);
                        AttachAsRChild(p, g);
                        AttachAsLChild(p, v.RChild);
                        AttachAsRChild(v, p);
                    }
                    //zig zag
                    else
                    {
                        AttachAsLChild(p, v.RChild);
                        AttachAsRChild(v, p);
                        AttachAsRChild(g, v.LChild);
                        AttachAsLChild(v, g);
                    }
                }
                else
                {
                    //zag zag
                    if (IsRChild(p))
                    {
                        AttachAsRChild(g, p.LChild);
                        AttachAsLChild(p, g);
                        AttachAsRChild(p, v.LChild);
                        AttachAsLChild(v, p);
                    }
                    //zag zig
                    else
                    {
                        AttachAsLChild(p, v.RChild);
                        AttachAsRChild(v, p);
                        AttachAsRChild(g, v.LChild);
                        AttachAsLChild(v, g);
                    }
                }

                v.Parent = gg;
                if (isL)
                {
                    gg.LChild = v;
                }
                if (isR)
                {
                    gg.RChild = v;
                }

                //note the update order!!!
                UpdateHeight(g);
                UpdateHeight(p);
                UpdateHeight(v);
            }

            if (p != null)
            {
                if (IsLChild(v))
                {
                    AttachAsLChild(p, v.RChild);
                    AttachAsRChild(v, p);
                }
                else
                {
                    AttachAsRChild(p, v.LChild);
                    AttachAsLChild(v, p);
                }
            }

            return v;
        }

        #region Public

        public override IBinNode<T> Search(T data)
        {
            IBinNode<T> node =  base.Search(data);

            _root = MakeSplay(node ?? Hot);
            //注意：失败的时候，返回了一个近似的节点
            return _root;
        }

        public override IBinNode<T> Insert(T data)
        {
            IBinNode<T> node = Search(data);
            if (node.Data.CompareTo(data) == 0)
            {
                return node;
            }

            _root = new BinNode<T>(data);
            if (node.Data.CompareTo(data) < 0)
            {
                _root.LChild = node;
                node.Parent = _root;
                _root.RChild = node.RChild;
                _root.RChild.Parent = node;
                node.RChild = null;
            }
            else
            {
                _root.LChild = node.LChild;
                node.LChild.Parent = _root;
                node.LChild = null;
                _root.RChild = node;
                node.Parent = _root;
            }
            UpdateHeightAbove(node);

            return _root;
        }

        public override bool Remove(T data)
        {
            if (Root().Data.CompareTo(data) != 0)
            {
                return false;
            }

            if (Root().LChild != null && Root().RChild != null)
            {
                IBinNode<T> rNode = Root().RChild;
                while (rNode.LChild != null)
                {
                    rNode = rNode.LChild;
                }
                IBinNode<T> newLeft = rNode.RChild;
                rNode.Parent.LChild = newLeft;
                if (newLeft != null)
                {
                    newLeft.Parent = rNode.Parent;
                }
                rNode.LChild = _root.LChild;
                _root.LChild.Parent = rNode;
                rNode.RChild = _root.RChild;
                _root.RChild.Parent = rNode;
                rNode.Parent = null;
                _root = rNode;

                UpdateHeightAbove(newLeft);
            }
            else if (Root().LChild == null)
            {
                _root = _root.RChild;
                _root.Parent = null;
            }
            else if (Root().RChild == null)
            {
                _root = _root.LChild;
                _root.Parent = null;
            }
            else
            {
                _root = null;
            }

            return true;
        }

        #endregion
    }
}
