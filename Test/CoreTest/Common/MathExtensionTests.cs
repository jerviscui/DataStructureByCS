using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Implement;

namespace Core.Common.Tests
{
    [TestClass()]
    public class MathExtensionTests
    {
        [TestMethod()]
        public void ConvertDecimalToBaseTest()
        {
            MyStack<string> result = new MyStack<string>();
            MathExtension.ConvertDecimalToBase(ref result, 100, 5);

            Assert.IsFalse(result.Empty());
        }
    }
}