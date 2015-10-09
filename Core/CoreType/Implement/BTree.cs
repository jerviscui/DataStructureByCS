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
        protected int _size;
        //阶次
        protected int _order;
        protected BTNode<T> _root;
        //最后访问的非空节点
        protected BTNode<T> _hot;

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
            BTNode<T> v = _root;
            _hot = null;

            while (v != null)
            {
                int rank = v.Key.Search(data);
                if (v.Key[rank].CompareTo(data) == 0)
                {
                    return v;
                }

                _hot = v;
                //注意：向量查找返回不大于的最后一个元素，所以孩子的选择rank+1
                v = v.Children[rank + 1] as BTNode<T>;
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

            int rank = _hot.Key.Search(data);

            _hot.Key.Insert(rank + 1, data);
            _hot.Children.Insert(rank + 2, null);
            _size++;
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
            _size--;

            SolveUnderFlow();

            return true;
        }
    }
}
