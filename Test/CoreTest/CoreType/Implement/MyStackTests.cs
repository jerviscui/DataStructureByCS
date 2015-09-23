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
    public class MyStackTests
    {
        [TestMethod()]
        public void EmptyTest()
        {
            PushTest();
        }

        [TestMethod()]
        public void PopTest()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);

            Assert.IsTrue(stack.Pop() == 10, "10");
            Assert.IsTrue(stack.Empty());
        }

        [TestMethod()]
        public void TopTest()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);

            Assert.IsTrue(stack.Top() == 10, "10");
        }

        [TestMethod()]
        public void PushTest()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);

            Assert.IsFalse(stack.Empty());
        }
    }
}