using System.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreType.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreType.Implement.Tests
{
	[TestClass()]
	public class VectorTests
	{
		[TestMethod()]
		public void VectorTest()
		{
			Vector<int> intVector = new Vector<int>();
			Assert.IsNotNull(intVector);

			Vector<int> v2 = new Vector<int>(50);
			Assert.IsNotNull(v2);
			Assert.IsTrue(v2.Size() == 0);
		}

		[TestMethod()]
		public void VectorTest1()
		{
			Vector<int> intVector = new Vector<int>();
			Assert.IsNotNull(intVector);

			Vector<int> v2 = new Vector<int>(intVector, 50);
			Assert.IsNotNull(v2);
		}

		[TestMethod()]
		public void VectorTest2()
		{
			Vector<int> intVector = new Vector<int>();
			Assert.IsNotNull(intVector);

			Vector<int> v2 = new Vector<int>(intVector, 20, 30);
			Assert.IsNotNull(v2);
		}

		[TestMethod()]
		public void VectorTest3()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 1, 1, 1, 1, 10 }, 3);
			Assert.IsNotNull(intVector);

			Assert.IsTrue(intVector.Size() == 3);
			Assert.IsTrue(intVector[0] == 1, "0");
			Assert.IsTrue(intVector[1] == 1, "1");
			Assert.IsTrue(intVector[2] == 1, "2");
		}

		[TestMethod()]
		public void VectorTest4()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 1, 1, 1, 1, 10 }, 0, 6);
			Assert.IsNotNull(intVector);

			Assert.IsTrue(intVector.Size() == 6);
			Assert.IsTrue(intVector[0] == 1, "0");
			Assert.IsTrue(intVector[1] == 1, "1");
			Assert.IsTrue(intVector[2] == 1, "2");
			Assert.IsTrue(intVector[5] == 10, "10");
		}

		[TestMethod()]
		public void SizeTest()
		{
			
		}

		[TestMethod()]
		public void GetTest()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 1, 1, 1, 1, 10 }, 0, 6);

			Assert.IsTrue(intVector.Get(0) == 1, "0");
			Assert.IsTrue(intVector.Get(5) == 10, "10");
		}

		[TestMethod()]
		public void PutTest()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 1, 1, 1, 1, 10 }, 0, 6);

			intVector.Put(0, 20);
			Assert.IsTrue(intVector.Get(0) == 20);
		}

		[TestMethod()]
		public void InsertTest()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 2, 3, 4, 5 }, 0, 4);

			//not expend
			for (int i = 0; i < 4; i++)
			{
				intVector.Insert(4, i);
			}
			Assert.IsTrue(intVector.Size() == 8);
			Assert.IsTrue(intVector[4] == 3, "4");
			
			//expend 
			intVector.Insert(8, 20);
			Assert.IsTrue(intVector.Size() == 8 + 1, "len");
			Assert.IsTrue(intVector[8] == 20, "8");
			Assert.IsTrue(intVector[7] == 0, "7");

			Assert.IsTrue(intVector.Search(19) == 7, "s19");
			intVector.Insert(intVector.Search(19) + 1, 19);
			Assert.IsTrue(intVector[8] == 19);

			for (int i = 5; i < 8; i++)
			{
				intVector[i] = 5;
			}
			Assert.IsTrue(intVector.Search(5) == 7, "s5");
			intVector.Insert(intVector.Search(5) + 1, 5);
			Assert.IsTrue(intVector[8] == 5);
		}

		[TestMethod()]
		public void RemoveTest()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 2, 3, 4, 5 }, 5);

			Assert.IsTrue(intVector.Remove(0) == 1, "times 1");
			Assert.IsTrue(intVector.Remove(0) == 2, "2");
			Assert.IsTrue(intVector.Size() == 5 - 2);
		}

		[TestMethod()]
		public void FindTest()
		{
			Vector<int> intVector = new Vector<int>(new int[] { 1, 2, 3, 4, 1, 5, 5 }, 7);

			Assert.IsTrue(intVector.Find(1) == 4, "times 1");
			Assert.IsTrue(intVector.Find(5) == 6, "times 2");
			Assert.IsTrue(intVector.Find(3) == 2, "times 3");
		}

		[TestMethod()]
		public void SearchTest()
		{
            //binary search1
            //Vector<int> intVector = new Vector<int>(new int[] { 1, 2, 2, 2, 4 }, 5);
            //Assert.IsTrue(intVector.Search(2) == 2, "1-2");
            //Assert.IsTrue(intVector.Search(0) == -1, "1-0");

            //fibonacci search
            //Vector<int> intVector2 = new Vector<int>(new int[] { 1, 2, 2, 2, 2, 5, 6, 7, 8 }, 9);
            //Assert.IsTrue(intVector2.Search(2) == 4, "2-2");
            //Assert.IsTrue(intVector2.Search(5) == 5, "2-5");

            //binary search2
            //Vector<int> intVector3 = new Vector<int>(new int[] { 1, 2, 2, 2, 4 }, 5);
            //Assert.IsTrue(intVector3.Search(2) == 3, "3-2");
            //Assert.IsTrue(intVector3.Search(0) == -1, "3-0");

            //binary search3
            Vector<int> intVector3 = new Vector<int>(new int[] { 1, 2, 2, 2, 2, 4 }, 6);
            Assert.IsTrue(intVector3.Search(2) == 4, "4-2");
            Assert.IsTrue(intVector3.Search(0) == -1, "4-0");
            Assert.IsTrue(intVector3.Search(5) == 5, "4-5");
        }

        [TestMethod()]
		public void DisorderedTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SortTest()
		{
			//1, 2, 5, 6, 7, 7, 8, 8, 9
			Vector<int> intVector = new Vector<int>(new int[] { 9, 8, 7, 1, 2, 5, 6, 7, 8 }, 9);
			intVector.Sort();
			Assert.IsTrue(intVector[0] == 1, "1");
			Assert.IsTrue(intVector[1] == 2, "2");
			Assert.IsTrue(intVector[2] == 5, "5");
			Assert.IsTrue(intVector[3] == 6, "6");
			Assert.IsTrue(intVector[4] == 7, "7");
			Assert.IsTrue(intVector[5] == 7, "7-2");
			Assert.IsTrue(intVector[6] == 8, "8");
			Assert.IsTrue(intVector[7] == 8, "8-2");
			Assert.IsTrue(intVector[8] == 9, "9");
		}

		[TestMethod()]
		public void DeduplicateTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void UniqufiyTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void TraverseTest()
		{
			Assert.Fail();
		}
	}
}