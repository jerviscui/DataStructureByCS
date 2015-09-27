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
    public class BinTreeTests
    {
        [TestMethod()]
        public void BinTreeTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            Assert.IsTrue(tree != null);
        }

        [TestMethod()]
        public void UpdateHeightTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            Assert.IsTrue(tree.Root().LChild().Height == 0, "lchild");
            tree.InsertAsLC(tree.Root().LChild(), 16);
            Assert.IsTrue(tree.UpdateHeight(tree.Root().LChild()) == 1);
        }

        [TestMethod()]
        public void UpdateHeightAboveTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            Assert.IsTrue(tree.Root().LChild().Height == 0, "lchild");
            tree.InsertAsLC(tree.Root().LChild(), 16);
            Assert.IsTrue(tree.Root().Height == 2);
        }

        [TestMethod()]
        public void SizeTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            Assert.IsTrue(tree.Size() == 2, "2");

            tree = new BinTree<int>(null);
            Assert.IsTrue(tree.Size() == 0);
        }

        [TestMethod()]
        public void HeightTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            Assert.IsTrue(tree.Height() == 1);
        }

        [TestMethod()]
        public void EmptyTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            Assert.IsFalse(tree.Empty());
        }

        [TestMethod()]
        public void RootTest()
        {
            return;
        }

        [TestMethod()]
        public void InsertAsLCTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);

            Assert.IsTrue(tree.Root().LChild().Data() == 15);
        }

        [TestMethod()]
        public void InsertAsRCTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsRC(tree.Root(), 20);

            Assert.IsTrue(tree.Root().RChild().Data() == 20);
        }

        [TestMethod()]
        public void TravLevelTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            tree.InsertAsLC(tree.Root().LChild(), 16);
            tree.InsertAsRC(tree.Root().LChild(), 17);
            tree.InsertAsRC(tree.Root(), 20);
            tree.InsertAsLC(tree.Root().RChild(), 21);
            
            int sum = 0;
            tree.TravLevel(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravPreTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            tree.InsertAsLC(tree.Root().LChild(), 16);
            tree.InsertAsRC(tree.Root().LChild(), 17);
            tree.InsertAsRC(tree.Root(), 20);
            tree.InsertAsLC(tree.Root().RChild(), 21);

            int sum = 0;
            tree.TravPre(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravInTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            tree.InsertAsLC(tree.Root().LChild(), 16);
            tree.InsertAsRC(tree.Root().LChild(), 17);
            tree.InsertAsRC(tree.Root(), 20);
            tree.InsertAsLC(tree.Root().RChild(), 21);

            int sum = 0;
            tree.TravIn(o => sum += o);
            Assert.IsTrue(sum == 99);
        }

        [TestMethod()]
        public void TravPostTest()
        {
            BinTree<int> tree = new BinTree<int>(new BinNode<int>(10));
            tree.InsertAsLC(tree.Root(), 15);
            tree.InsertAsLC(tree.Root().LChild(), 16);
            tree.InsertAsRC(tree.Root().LChild(), 17);
            tree.InsertAsRC(tree.Root(), 20);
            tree.InsertAsLC(tree.Root().RChild(), 21);

            int sum = 0;
            tree.TravPost(o => sum += o);
            Assert.IsTrue(sum == 99);
        }
    }
}