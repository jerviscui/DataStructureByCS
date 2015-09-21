using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.CoreType.Implement.Tests
{
	[TestClass]
	public class MyListTests
	{
		[TestMethod]
		public void MyListTest()
		{
			MyList<int> list = new MyList<int>();
			Assert.IsTrue(list != null);
		}

		[TestMethod]
		public void MyListTest1()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			MyList<int> list2 = new MyList<int>(list1);
			Assert.IsTrue(list2 != null);

			Assert.IsTrue(list2.First().Data == 10, "10");
			Assert.IsTrue(list2.First().Succeed.Data == 11, "11");
			Assert.IsTrue(list2.First().Succeed.Succeed.Data == 12, "12");
		}

		[TestMethod]
		public void MyListTest2()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			MyList<int> list2 = new MyList<int>(list1, 2);
			Assert.IsTrue(list2 != null);
			Assert.IsTrue(list2.Size() == 2);
			Assert.IsTrue(list2.First().Data == 10, "10");
			Assert.IsTrue(list2.First().Succeed.Data == 11, "11");
		}

		[TestMethod]
		public void MyListTest3()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			MyList<int> list2 = new MyList<int>(list1, 1, 2);
			Assert.IsTrue(list2 != null);
			Assert.IsTrue(list2.Size() == 2);
			Assert.IsTrue(list2.First().Data == 11, "11");
			Assert.IsTrue(list2.First().Succeed.Data == 12, "12");
		}

		[TestMethod]
		public void SizeTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			Assert.IsTrue(list1.Size() == 3);
		}

		[TestMethod]
		public void FirstTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			Assert.IsTrue(list1.First().Data == 10);
		}

		[TestMethod]
		public void LastTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			Assert.IsTrue(list1.Last().Data == 12);
		}

		[TestMethod]
		public void InsertAsFirstTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsFirst(10);
			list1.InsertAsFirst(11);
			list1.InsertAsFirst(12);

			Assert.IsTrue(list1.Size() == 3);
			Assert.IsTrue(list1.First().Data == 12, "10");
			Assert.IsTrue(list1.First().Succeed.Data == 11, "11");
			Assert.IsTrue(list1.First().Succeed.Succeed.Data == 10, "12");
		}

		[TestMethod]
		public void InsertAsLastTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			Assert.IsTrue(list1.Size() == 3);
			Assert.IsTrue(list1.First().Data == 10, "10");
			Assert.IsTrue(list1.First().Succeed.Data == 11, "11");
			Assert.IsTrue(list1.First().Succeed.Succeed.Data == 12, "12");
		}

		[TestMethod]
		public void InsertBeforeTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertBefore(list1.First(), 11);

			Assert.IsTrue(list1.Size() == 2);
			Assert.IsTrue(list1.First().Data == 11);
		}

		[TestMethod]
		public void InsertAfterTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAfter(list1.First(), 11);

			Assert.IsTrue(list1.Size() == 2);
			Assert.IsTrue(list1.Last().Data == 11);
		}

		[TestMethod]
		public void RemoveTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsFirst(10);
			list1.InsertAsFirst(11);
			list1.InsertAsFirst(12);

			list1.Remove(list1.First().Succeed);

			Assert.IsTrue(list1.Size() == 2);
			Assert.IsTrue(list1.First().Succeed.Data == 10, "10");
		}

		[TestMethod]
		public void DisorderedTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsFirst(10);
			list1.InsertAsFirst(11);
			list1.InsertAsFirst(12);

			Assert.IsFalse(list1.Disordered());
		}

		[TestMethod]
		public void SortTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsFirst(10);
			list1.InsertAsFirst(11);
			list1.InsertAsFirst(12);

			list1.Sort();
			Assert.IsTrue(list1.First().Data == 10, "10");
			Assert.IsTrue(list1.First().Succeed.Data == 11, "11");
			Assert.IsTrue(list1.First().Succeed.Succeed.Data == 12, "12");
		}

		[TestMethod]
		public void FindTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsFirst(12);
			list1.InsertAsFirst(10);
			list1.InsertAsFirst(11);
			list1.InsertAsFirst(12);

			Assert.IsTrue(list1.Find(12) == list1.Last());
		}

		[TestMethod]
		public void SearchTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);
			//ordered
			//10,11,12

			Assert.IsTrue(list1.Search(9) == list1.First().Precursor, "head");

			list1.InsertAfter(list1.Search(13), 13);
			Assert.IsTrue(list1.Size() == 4);
			Assert.IsTrue(list1.Last().Data == 13, "13-1");
			Assert.IsTrue(list1[3] == 13, "13");
		}

		[TestMethod]
		public void DeduplicateTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(12);
			list1.InsertAsLast(11);
			list1.InsertAsLast(10);

			Assert.IsTrue(list1.Deduplicate() == 1);
			Assert.IsTrue(list1.Size() == 3, "size");
			Assert.IsTrue(list1.First().Data == 12, "12");
			Assert.IsTrue(list1.First().Succeed.Data == 11, "11");
			Assert.IsTrue(list1.Last().Data == 10, "10");
		}

		[TestMethod]
		public void UniquifyTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);
			list1.InsertAsLast(12);

			Assert.IsTrue(list1.Uniquify() == 2);
			Assert.IsTrue(list1[0] == 10, "10");
			Assert.IsTrue(list1[1] == 11, "11");
			Assert.IsTrue(list1[2] == 12, "12");
		}

		[TestMethod]
		public void TraverseTest()
		{
			MyList<int> list1 = new MyList<int>();
			list1.InsertAsLast(10);
			list1.InsertAsLast(11);
			list1.InsertAsLast(12);

			int sum = 0;
			list1.Traverse(o => sum += o);
			Assert.IsTrue(sum == 33);
		}
	}
}
