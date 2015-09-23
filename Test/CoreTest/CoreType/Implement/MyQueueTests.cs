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
    public class MyQueueTests
    {
        [TestMethod()]
        public void EmptyTest()
        {
            MyQueue<int> queue = new MyQueue<int>();
            Assert.IsTrue(queue.Empty());
        }

        [TestMethod()]
        public void EnQueueTest()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.EnQueue(10);

            Assert.IsTrue(queue.Size() == 1);
        }

        [TestMethod()]
        public void RearTest()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.EnQueue(10);

            Assert.IsTrue(queue.Rear() == 10);
        }

        [TestMethod()]
        public void DeQueueTest()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.EnQueue(10);
            queue.EnQueue(20);

            Assert.IsTrue(queue.DeQueue() == 10);
        }

        [TestMethod()]
        public void FrontTest()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.EnQueue(10);
            queue.EnQueue(20);

            Assert.IsTrue(queue.Front() == 10);
        }
    }
}