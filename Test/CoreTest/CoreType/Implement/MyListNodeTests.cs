using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.CoreType.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Implement.Tests
{
    [TestClass()]
    public class MyListNodeTests
    {
        [TestMethod()]
        public void MyListNodeTest()
        {
            MyListNode<int> node1 = new MyListNode<int>(10, null, null);
            MyListNode<int> node2 = new MyListNode<int>(20, node1, null);
            node1.Succeed = node2;

            Assert.IsTrue(node1.Data == 10);
            Assert.IsTrue(node1.Succeed.Data == 20);
            Assert.IsTrue(node1.Succeed.Precursor.Data == 10);
        }

        [TestMethod()]
        public void InsertAsPredTest()
        {
            MyListNode<int> node1 = new MyListNode<int>(10, null, null);
            node1.InsertAsPred(20);

            Assert.IsTrue(node1.Precursor.Data == 20, "pred");
            Assert.IsTrue(node1.Precursor.Succeed.Data == 10, "succ");
        }

        [TestMethod()]
        public void InsertAsSuccTest()
        {
            MyListNode<int> node1 = new MyListNode<int>(10, null, null);
            node1.InsertAsSucc(20);

            Assert.IsTrue(node1.Succeed.Data == 20);
            Assert.IsTrue(node1.Succeed.Precursor.Data == 10);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            MyListNode<int> node1 = new MyListNode<int>(10, null, null);
            MyListNode<int> node2 = new MyListNode<int>(20, node1, null);

            Assert.IsTrue(node1.CompareTo(node2) == -1);
        }
    }
}