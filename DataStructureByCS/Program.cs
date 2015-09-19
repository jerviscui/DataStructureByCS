using System;
using System.Collections.Generic;
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

			MyList<int> aaa = new MyList<int>();
			aaa.InsertAsFirst(10);

			var ccc = new MyList<int>(aaa, 0, 1);
			Console.WriteLine(aaa.Last().Data);

			Console.ReadLine();
        }
    }
}
