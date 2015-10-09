using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IBST<T> : IBinTree<T>
    {
        IBinNode<T> Search(T data);

        IBinNode<T> Insert(T data);

        bool Remove(T data);
    }
}
