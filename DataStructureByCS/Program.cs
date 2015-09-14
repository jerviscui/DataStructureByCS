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

            Vector<int> intVector = new Vector<int>(new int[] { 1, 1, 1, 1, 1, 10 }, 3);
            
            var a = intVector[0];
            var b = intVector[1];
            var c = intVector[2];

            Console.ReadLine();
        }
    }
}
