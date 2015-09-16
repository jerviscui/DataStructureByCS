using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreType.Implement;

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

            Vector<int> intVector3 = new Vector<int>(new int[] { 1, 2, 2, 2, 4 }, 5);
            bool a = intVector3.Search(2) == 4;
            bool b = intVector3.Search(0) == -1;

            Console.ReadLine();
        }
    }
}
