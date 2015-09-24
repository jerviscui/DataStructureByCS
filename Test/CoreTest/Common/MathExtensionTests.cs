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

        [TestMethod()]
        public void ArithmeticExpressionResultTest()
        {
            Assert.IsTrue(Math.Abs(MathExtension.ArithmeticExpressionResult("1+(2*3)+4") - 11) < 1e-6);

            Assert.IsTrue(Math.Abs(MathExtension.ArithmeticExpressionResult("1+(2*3)+(2+3)*5") - 32) < 1e-6);

            Assert.IsTrue(Math.Abs(MathExtension.ArithmeticExpressionResult("(1+2^3!-4)*(5!-(6-(7-(89-0!))))") - 2013) < 1e-6);
        }

        [TestMethod()]
        public void FactorialBasicTest()
        {
            Assert.IsTrue(Math.Abs(MathExtension.FactorialBasic(1) - 1) < 1e-6, "1");
            Assert.IsTrue(Math.Abs(MathExtension.FactorialBasic(3) - 6) < 1e-6);
            Assert.IsTrue(Math.Abs(MathExtension.FactorialBasic(5) - 120) < 1e-6);
        }

        [TestMethod()]
        public void GetRpnExpressionTest()
        {
			Assert.IsTrue(MathExtension.GetRpnExpression("1+(2*3)+4") == "1 2 3 * + 4 + ");

			Assert.IsTrue(MathExtension.GetRpnExpression("1+(2*3)+(2+3)*5") == "1 2 3 * + 2 3 + 5 * + ");
			Assert.IsTrue(MathExtension.GetRpnExpression("1+(23*3)+(2+3)*5") == "1 23 3 * + 2 3 + 5 * + ");
        }

		[TestMethod()]
	    public void RpnExpressionResultTest()
	    {
			Assert.IsTrue(Math.Abs(MathExtension.RpnExpressionResult(
				MathExtension.GetRpnExpression("1+(23*3)+(2+3)*5")) - 95) < 1e-6);

			Assert.IsTrue(Math.Abs(MathExtension.RpnExpressionResult(
				MathExtension.GetRpnExpression("1+(23*3)+(2^9)*5!")) - 61510) < 1e-6);
	    }
    }
}