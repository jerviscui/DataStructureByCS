using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class MyStack<T>: Vector<T>, IMyStack<T> where T : IComparable<T>
    {
        public bool Empty()
        {
            return this.Size() == 0;
        }

        public T Pop()
        {
            return this.Remove(this.Size() - 1);
        }

        public T Top()
        {
            return this[Size() - 1];
        }

        public void Push(T element)
        {
            Insert(Size(), element);
        }
    }
}
