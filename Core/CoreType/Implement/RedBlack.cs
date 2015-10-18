using System;
using System.Collections.Generic;
using System.Data;
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
            IBinNode<T> node = Search(data);
            if (node == null)
            {
                return false;
            }

            IBinNode<T> r = RemoveAt(node, ref Hot);
            if (r.Color == NodeColor.Red || node.Color == NodeColor.Red)
            {
                //如果两个节点有一个是红色，则用r替代node后不会影响黑高度
                //整树不需要再做改变
            }
            else
            {
                SolveDoubleBlack(r);
            }

            return true;
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
                if (p == g.LChild)
                {
                    //zig zig
                    if (node == p.LChild)
                    {
                        Connect34(node, p, g, node.LChild, node.RChild, p.RChild, u);
                        var color = g.Color;
                        g.Color = p.Color;
                        p.Color = color;
                    }
                    //zig zag
                    else
                    {
                        Connect34(p, node, g, p.LChild, node.LChild, node.RChild, u);
                        var color = g.Color;
                        g.Color = node.Color;
                        node.Color = color;
                    }
                }
                else
                {
                    //zag zag
                    if (node == p.RChild)
                    {
                        Connect34(g, p, node, u, p.LChild, node.LChild, node.RChild);
                        var color = g.Color;
                        g.Color = p.Color;
                        p.Color = color;
                    }
                    //zag zig
                    else
                    {
                        Connect34(g, node, p, u, node.LChild, node.RChild, p.RChild);
                        var color = g.Color;
                        g.Color = node.Color;
                        node.Color = color;
                    }
                }
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

        /// <summary>
        /// 双黑修复
        /// </summary>
        /// <param name="node"></param>
        protected void SolveDoubleBlack(IBinNode<T> node)
        {
            IBinNode<T> p = node.Parent;
            IBinNode<T> s = node == p.LChild ? p.RChild : p.LChild;
            if (s.Color == NodeColor.Black)
            {
                //BB-1
                IBinNode<T> t = null;
                if (s.LChild != null && s.LChild.Color == NodeColor.Red)
                {
                    t = s.LChild;
                }
                else if (s.RChild != null && s.RChild.Color == NodeColor.Red)
                {
                    t = s.RChild;
                }

                //s has a child.Color == Red
                if (t != null)
                {
                    //3+4
                    if (s == p.LChild)
                    {
                        //zig zig
                        if (t == s.LChild)
                        {
                            Connect34(t, s, p, t.LChild, t.RChild, s.RChild, node);
                            s.Color = p.Color;
                            t.Color = NodeColor.Black;
                            p.Color = NodeColor.Black;
                        }
                        //zig zag
                        else
                        {
                            Connect34(s, t, p, s.LChild, t.LChild, t.RChild, node);
                            t.Color = p.Color;
                            p.Color = NodeColor.Black;
                        }
                    }
                    else
                    {
                        //zag zag
                        if (t == s.RChild)
                        {
                            Connect34(p, s, t, node, s.LChild, t.LChild, t.RChild);
                            s.Color = p.Color;
                            t.Color = NodeColor.Black;
                            p.Color = NodeColor.Black;
                        }
                        //zag zig
                        else
                        {
                            Connect34(p, t, s, node, t.LChild, t.RChild, s.RChild);
                            t.Color = p.Color;
                            p.Color = NodeColor.Black;
                        }
                    }
                }
                //BB-2R
                else if (p.Color == NodeColor.Red)
                {
                    s.Color = NodeColor.Red;
                    p.Color = NodeColor.Black;
                }
                //BB-2B
                else if(p.Color == NodeColor.Black)
                {
                    //这里为什么染红s
                    //因为双黑条件下，删除节点的分支黑高度较少了一，那么s所在分支也应该减少一的黑高度
                    //s分支所知的是s和左右孩子均为黑色，要想改变颜色后不引起s分支的犯规，只能将s染红以减少其后节点一个黑高度
                    s.Color = NodeColor.Red;
                    SolveDoubleBlack(p);
                }
            }
            //BB-3: s.Color == Red
            else
            {
                //zig
                if (s == p.LChild)
                {
                    s.Parent = p.Parent;
                    if (p.Parent.LChild == p)
                    {
                        p.Parent.LChild = s;
                    }
                    else
                    {
                        p.Parent.RChild = s;
                    }
                    p.LChild = s.RChild;
                    p.LChild.Parent = p;
                    s.RChild = p;
                    p.Parent = s;
                }
                //zag
                else
                {
                    s.Parent = p.Parent;
                    if (p.Parent.LChild == p)
                    {
                        p.Parent.LChild = s;
                    }
                    else
                    {
                        p.Parent.RChild = s;
                    }
                    p.RChild = s.LChild;
                    p.RChild.Parent = p;
                    s.LChild = p;
                    p.Parent = s;
                }
                s.Color = NodeColor.Black;
                p.Color = NodeColor.Red;

                SolveDoubleBlack(node);
            }
        }
    }
}
