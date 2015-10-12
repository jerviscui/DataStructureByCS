using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class BTree<T> : IBTree<T> where T : IComparable<T>
    {
        //关键码总数
        protected int Size;
        //阶次
        protected int Order;
        protected BtNode<T> Root;
        //最后访问的非空节点
        protected BtNode<T> Hot;

        protected void SolveOverFlow()
        {
            //todo implement
        }

        protected void SolveUnderFlow()
        {
            //todo implement
        }

        public IBTNode<T> Search(T data)
        {
            BtNode<T> v = Root;
            Hot = null;

            while (v != null)
            {
                int rank = v.Key.Search(data);
                if (v.Key[rank].CompareTo(data) == 0)
                {
                    return v;
                }

                Hot = v;
                //注意：向量查找返回不大于的最后一个元素，所以孩子的选择rank+1
                v = v.Children[rank + 1] as BtNode<T>;
            }

            return null;
        }

        public bool Insert(T data)
        {
            IBTNode<T> s = Search(data);
            if (s != null)
            {
                return false;
            }

            int rank = Hot.Key.Search(data);

            Hot.Key.Insert(rank + 1, data);
            Hot.Children.Insert(rank + 2, null);
            Size++;
            SolveOverFlow();

            return true;
        }

        public bool Remove(T data)
        {
            IBTNode<T> s = Search(data);
            if (s == null)
            {
                return false;
            }

            int rank = s.Key.Search(data);
            if (s.Children[0] != null)
            {
                IBTNode<T> rNode = s.Children[rank + 1];
                while (rNode.Children[0] != null)
                {
                    rNode = rNode.Children[0];
                }
                T temp = s.Key[rank];
                s.Key[rank] = rNode.Key[0];
                rNode.Key[0] = temp;
                rank = 0;
                s = rNode;
            }
            s.Key.Remove(rank);
            s.Children.Remove(rank + 1);
            Size--;

            SolveUnderFlow();

            return true;
        }
    }
}
