using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Implement;

namespace Core.Common
{
    public static class MathExtension
    {
        public static void ConvertDecimalToBase(ref MyStack<string> result, int num, int @base)
        {
            if (@base > 16)
            {
                throw new ArgumentException("base overstep 16");
            }

            if (result == null || !result.Empty())
            {
                throw new ArgumentException("result is null or not empty");
            }

            string[] items = new []{"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "A", "B", "C", "D", "E", "F"};

            while (num > 0)
            {
                result.Push(items[num % @base]);
                num /= @base;
            }
        }
    }
}
