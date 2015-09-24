using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Core.CoreType.Implement;

namespace DataStructureByCS
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;

            while (++i > 0 && i < 5)
            {
                Console.WriteLine(i);
            }

            MyStack<string> result = new MyStack<string>();
            MathExtension.ConvertDecimalToBase(ref result, 100, 5);
            StringBuilder builder = new StringBuilder();
            while (result.Size() > 0)
            {
                builder.Append(result.Pop());
            }
            Console.WriteLine("5x: " + builder.ToString());
            
            MathExtension.ConvertDecimalToBase(ref result, 100, 10);
            builder.Clear();
            while (result.Size() > 0)
            {
                builder.Append(result.Pop());
            }
            Console.WriteLine("10x: " + builder.ToString());

            MathExtension.ConvertDecimalToBase(ref result, 100, 16);
            builder.Clear();
            while (result.Size() > 0)
            {
                builder.Append(result.Pop());
            }
			Console.WriteLine(MathExtension.GetRpnExpression("1+(23*3)+(2^9)*5!"));

            Console.ReadLine();
        }
    }
    
}
