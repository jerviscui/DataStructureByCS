using System;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
	public class MyListNode<T> : IMyListNode<T>, IDisposable, IComparable<MyListNode<T>> 
		where T : IComparable<T>
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
		    if (this.Precursor != null)
		    {
                this.Precursor.Succeed = node;
            }
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
		    if (this.Succeed != null)
		    {
                this.Succeed.Precursor = node;
            }
			this.Succeed = node;

			return node;
		}

	    /// <summary>
	    /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
	    /// </summary>
	    /// <returns>
	    /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
	    /// </returns>
	    /// <param name="other">An object to compare with this instance. </param>
	    public int CompareTo(MyListNode<T> other)
	    {
            return Data.CompareTo(other.Data);
        }

		public void Dispose()
		{
			Succeed = null;
			Precursor = null;
			Data = default (T);
		}
	}
}
