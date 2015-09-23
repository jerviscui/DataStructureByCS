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
    public class StringExtensionTests
    {
        [TestMethod()]
        public void ElementsMatchingTest()
        {
            Assert.IsTrue(StringExtension.ElementsMatching(new[] { "(" }, new[] { ")" }, "()"));
            Assert.IsFalse(StringExtension.ElementsMatching(new[] { "(" }, new[] { ")" }, "())"));

            Assert.IsFalse(StringExtension.ElementsMatching(new[] { "(" }, new[] { ")" }, "((())"));

            Assert.IsTrue(StringExtension.ElementsMatching(
                new[] { "(", "{", "[" }, new[] { ")", "}", "]" }, "{[()]}"));
            Assert.IsFalse(StringExtension.ElementsMatching(
                new[] { "(", "{", "[" }, new[] { ")", "}", "]" }, "{{[()]}"));

            Assert.IsTrue(StringExtension.ElementsMatching(
                new[] { "<node>", "<page>", "<html>" }, new[] { "</node>", "</page>", "</html>" }, 
                "<html><node>a</node><node><node>b</node>c</node><page>1</page></html>"));
            Assert.IsFalse(StringExtension.ElementsMatching(
                new[] { "<node>", "<page>", "<html>" }, new[] { "</node>", "</page>", "</html>" },
                "<html><node>a</node><node><node>b</node>c</node>1</page></html>"));
        }
    }
}