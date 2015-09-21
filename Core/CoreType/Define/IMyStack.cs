using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IMyStack<T>
    {
        bool Empty();

        T Pop();

        T Top();

        void Push(T element);
    }
}
