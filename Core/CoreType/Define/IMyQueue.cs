using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IMyQueue<T>
    {
        bool Empty();

        void EnQueue(T data);

        T Rear();

        T DeQueue();

        T Front();
    }
}
