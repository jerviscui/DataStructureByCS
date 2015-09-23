using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class MyQueue <T>: MyList<T>, IMyQueue<T> where T : IComparable<T>
    {
        public bool Empty()
        {
            return this.Size() == 0;
        }

        public void EnQueue(T data)
        {
            this.InsertAsLast(data);
        }

        public T Rear()
        {
            return this.Last().Data;
        }

        public T DeQueue()
        {
            return this.Remove(this.First());
        }

        public T Front()
        {
            return this.First().Data;
        }
    }
}
