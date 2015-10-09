using Core.CoreType.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.CoreType.Implement.Tests
{
    [TestClass]
    public class BinNodeTests
    {
        [TestMethod()]
        public void BinNodeTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            Assert.IsTrue(node != null);
        }

        [TestMethod()]
        public void SizeTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(20);
            Assert.IsTrue(node.Size() == 2);
        }

        [TestMethod()]
        public void LChildTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(20);
            Assert.IsTrue(node.LChild.Data == 20);
        }

        [TestMethod()]
        public void RChildTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(20);
            Assert.IsTrue(node.LChild.Data == 20);
        }

        [TestMethod()]
        public void ParentTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(20);
            Assert.IsTrue(node.LChild.Parent.Data == 10);
        }

        [TestMethod()]
        public void InsertAsLCTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(20);
            Assert.IsTrue(node.LChild != null);
        }

        [TestMethod()]
        public void InsertAsRCTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            Assert.IsFalse(node.RChild != null);
            node.InsertAsRC(20);
            Assert.IsTrue(node.RChild != null);
        }

        [TestMethod()]
        public void SuccTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.InsertAsRC(20);

            Assert.IsTrue(node.Succ().Data == 20);
            Assert.IsTrue(node.LChild.Succ() == node);
            Assert.IsTrue(node.RChild.Succ() == null);
        }

        [TestMethod()]
        public void TravLevelTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.LChild.InsertAsLC(16);
            node.LChild.InsertAsRC(17);
            node.InsertAsRC(20);
            node.RChild.InsertAsLC(21);

            int sum = 0;
            node.TravLevel(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravPreTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.LChild.InsertAsLC(16);
            node.LChild.InsertAsRC(17);
            node.InsertAsRC(20);
            node.RChild.InsertAsLC(21);

            int sum = 0;
            node.TravPre(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravInTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.LChild.InsertAsLC(16);
            node.LChild.InsertAsRC(17);
            node.InsertAsRC(20);
            node.RChild.InsertAsRC(21);

            int sum = 0;
            node.TravIn(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravPostTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.LChild.InsertAsLC(16);
            node.LChild.InsertAsRC(17);
            node.InsertAsRC(20);
            node.RChild.InsertAsRC(21);

            int sum = 0;
            node.TravPost(o => sum += o);
            Assert.IsTrue(sum == 99);

            node = new BinNode<int>(10);
            node.InsertAsLC(15);
            node.LChild.InsertAsLC(16);
            node.LChild.InsertAsRC(17);
            node.InsertAsRC(20);
            node.RChild.InsertAsLC(21);

            sum = 0;
            node.TravPost(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            BinNode<int> node1 = new BinNode<int>(10);
            Assert.IsTrue(node.CompareTo(node1) == 0);
        }

        [TestMethod()]
        public void DataTest()
        {
            BinNode<int> node = new BinNode<int>(10);
            Assert.IsTrue(node.Data == 10);
        }
    }
}
