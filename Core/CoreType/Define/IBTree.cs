using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IBTree<T>
    {
        IBTNode<T> Search(T data);

        bool Insert(T data);

        bool Remove(T data);
    }
}
