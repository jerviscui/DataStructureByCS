using System;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
	public class MyListNode<T> : IMyListNode<T>, IComparable<T> where T : IComparable<T>
	{
		public MyListNode<T> Precursor { get; set; }

		public MyListNode<T> Succeed { get; set; }

		public T Data { get; set; }

		public MyListNode(object data, 
			MyListNode<T> pred, MyListNode<T> succ)
		{
			if (data is T)
			{
				Data = (T)data;
			}
			Precursor = pred;
			Succeed = succ;
		}

		/// <summary>
		/// insert a precursor element
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public MyListNode<T> InsertAsPred(T data)
		{
			MyListNode<T> node = new MyListNode<T>(data, this.Precursor, this);
			this.Precursor.Succeed = node;
			this.Precursor = node;

			return node;
		}

		/// <summary>
		/// insert a succeed element
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public MyListNode<T> InsertAsSucc(T data)
		{
			MyListNode<T> node = new MyListNode<T>(data, this, this.Succeed);
			this.Succeed.Precursor = node;
			this.Succeed = node;

			return node;
		}

		public int CompareTo(T other)
		{
			return Data.CompareTo(other);
		}
	}
}
