using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IBTNode<T>
    {
        IBTNode<T> Parent { get; set; }

        IVector<T> Key { get; set; }

        IVector<IBTNode<T>> Children { get; set; }
    }
}
