using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Implement;

namespace Core.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lElements"></param>
        /// <param name="rElements"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool ElementsMatching(string[] lElements, string[] rElements, string content)
        {
            if (lElements.Count() != rElements.Count())
            {
                throw new ArgumentException("the element pairs count is not matching");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            MyStack<int> stack = new MyStack<int>();
            while (content.Length >= 0)
            {
                int rIndex;
                int lIndex = rIndex = content.Length;
                int element1 = -1;
                int element2 = -1;
                for (int i = 0; i < lElements.Length; i++)
                {
                    int index1 = content.IndexOf(lElements[i], StringComparison.Ordinal);
                    if (index1 > -1 && index1 < lIndex)
                    {
                        lIndex = index1;
                        element1 = i;
                    }
                    int index2 = content.IndexOf(rElements[i], StringComparison.Ordinal);
                    if (index2 > -1 && index2 < rIndex)
                    {
                        rIndex = index2;
                        element2 = i;
                    }
                }

                if (element1 == -1 && element2 == -1)
                {
                    return stack.Empty();
                }

                if (lIndex < rIndex)
                {
                    //push
                    stack.Push(element1);
                    content = content.Substring(lIndex + lElements[element1].Length);
                }
                else if (lIndex > rIndex)
                {
                    //pop
                    if (stack.Top() != element2)
                    {
                        return false;
                    }
                    stack.Pop();
                    content = content.Substring(rIndex + rElements[element2].Length);
                }
                else
                {
                    return stack.Empty();
                }
            }

            return false;
        }
    }
}
