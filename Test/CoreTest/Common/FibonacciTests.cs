using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Tests
{
    [TestClass()]
    public class FibonacciTests
    {
        [TestMethod()]
        public void FibonacciTest()
        {
            //0,1,1,2,3,5,8,13,21,34,55
            Fibonacci fib = new Fibonacci(13);
            Assert.IsTrue(fib.Get() == 13, "==");

            Fibonacci fib2 = new Fibonacci(35);
            Assert.IsTrue(fib2.Get() == 55, "<");
        }

        [TestMethod()]
        public void GetTest()
        {
            
        }

        [TestMethod()]
        public void PrevTest()
        {
            Fibonacci fib = new Fibonacci(13);
            Assert.IsTrue(fib.Get() == 13, "==");
            fib.Prev();
            Assert.IsTrue(fib.Get() == 8);
            fib.Next();
            Assert.IsTrue(fib.Get() == 13);
            fib.Next();
            Assert.IsTrue(fib.Get() == 21);
        }

        [TestMethod()]
        public void NextTest()
        {

        }
    }
}