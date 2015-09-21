using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			Test aaa = new Test();
	        aaa.Data = 10;
	        Add(aaa);
			Console.WriteLine(aaa.Data);
			Add2(aaa);
			Console.WriteLine(aaa.Data);

            Console.ReadLine();
        }

	    private static void Add(Test a)
	    {
		    a.Data = 11;
	    }

		private static void Add2(Test a)
		{
			a = new Test();
			a.Data = 12;
		}
    }

	public class Test
	{
		public int Data { get; set; }
	}
}
