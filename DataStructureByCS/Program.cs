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

            BST<int> bst = new BST<int>(new BinNode<int>(15));
            bst.InsertAsLC(bst.Root(), 10);
            bst.InsertAsRC(bst.Root(), 20);
            var aaa = bst.Search(21);
            bst.Insert(21);

            Console.ReadLine();
        }
    }
    
}
