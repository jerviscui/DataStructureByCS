using System;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
	public class MyList<T> : IMyList<T> where T : IComparable<T>
	{
		#region Private

		private readonly MyListNode<T> _header;

		private readonly MyListNode<T> _trailer;

		private int _size;

		private void CopyFrom(MyList<T> source, int start, int count)
		{
			MyListNode<T> next = new MyListNode<T>(source[start], _header, _trailer);
			_header.Succeed = next;
			_size++;

			for (int i = start + 1; i < count; i++)
			{
				var temp = next;
				next = new MyListNode<T>(source[i], temp, temp.Succeed);
				temp.Succeed.Precursor = next;
				temp.Succeed = next;
				_size++;
			}

			_trailer.Precursor = next;
		}

		private void CopyFrom(MyListNode<T> start, int count)
		{
			while (count-- > 0)
			{
				InsertAsLast(start.Data);
				start = start.Succeed;
			}
		}

		/// <summary>
		/// 从start的真前驱中查找符合节点
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="count"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		private MyListNode<T> Find(T dest, int count, MyListNode<T> start)
		{
			while (count-- > 0)
			{
				start = start.Precursor;
				if (start == null)
				{
					break;
				}
				if (dest.CompareTo(start.Data) == 0)
				{
					return start;
				}
			}

			return null;
		}

		#endregion

		public MyList()
		{
			_header = new MyListNode<T>(null, null, null);
			_trailer = new MyListNode<T>(null, _header, null);
			_header.Succeed = _trailer;
			_size = 0;
		}

		public MyList(MyList<T> source) : this()
		{
			CopyFrom(source, 0, source.Size());
		}

		public MyList(MyList<T> source, int count) : this()
		{
			if (count > source.Size())
			{
				throw new ArgumentOutOfRangeException("count");
			}
			CopyFrom(source, 0, count);
		}

		public MyList(MyList<T> source, int start, int count) : this()
		{
			if (start > source.Size() - 1 || start + count > source.Size())
			{
				throw new ArgumentOutOfRangeException("start", "start or count out of range");
			}
			
			int index = 0;
			MyListNode<T> first = source.First();
			while (++index < start)
			{
				first = first.Succeed;
			}

			CopyFrom(first, count);
		}

		#region Implementation

		public T this[int index]
		{
			get
			{
				if (index >= _size)
				{
					throw new ArgumentOutOfRangeException();
				}

				MyListNode<T> item = _header.Succeed;
				while (index-- > 1)
				{
					item = item.Succeed;
				}

				return item.Data;
			}

			set
			{
				if (index >= _size)
				{
					throw new ArgumentOutOfRangeException();
				}

				MyListNode<T> item = _header.Succeed;
				while (index-- > 1)
				{
					item = item.Succeed;
				}

				item.Data = value;
			}
		}

		public int Size()
		{
			return _size;
		}

		/// <summary>
		/// return the first node
		/// </summary>
		/// <returns></returns>
		public MyListNode<T> First()
		{
			if (_size > 0)
			{
				return _header.Succeed;
			}

			return null;
		}

		/// <summary>
		/// return last node
		/// </summary>
		/// <returns></returns>
		public MyListNode<T> Last()
		{
			if (_size > 0)
			{
				return _trailer.Precursor;
			}

			return null;
		}

		/// <summary>
		/// insert into header
		/// </summary>
		/// <param name="element"></param>
		public void InsertAsFirst(T element)
		{
			MyListNode<T> node = new MyListNode<T>(element, _header, _header.Succeed);
			_header.Succeed.Precursor = node;
			_header.Succeed = node;
			_size++;
		}

		/// <summary>
		/// insert into trailer
		/// </summary>
		/// <param name="element"></param>
		public void InsertAsLast(T element)
		{
			MyListNode<T> node = new MyListNode<T>(element, _trailer.Precursor, _trailer);
			_trailer.Precursor.Succeed = node;
			_trailer.Precursor = node;
			_size++;
		}

		/// <summary>
		/// insert element into source precursor
		/// </summary>
		/// <param name="source"></param>
		/// <param name="element"></param>
		public void InsertBefore(MyListNode<T> source, T element)
		{
			source.InsertAsPred(element);
			_size++;
		}

		/// <summary>
		/// insert element into source succeed
		/// </summary>
		/// <param name="source"></param>
		/// <param name="element"></param>
		public void InsertAfter(MyListNode<T> source, T element)
		{
			source.InsertAsSucc(element);
			_size++;
		}
		
		#endregion

		public int Remove(T element)
		{
			throw new NotImplementedException();
		}

		public bool Disordered()
		{
			throw new NotImplementedException();
		}

		public void Sort()
		{
			throw new NotImplementedException();
		}

		public MyListNode<T> Find(T element)
		{
			throw new NotImplementedException();
		}

		public MyListNode<T> Search(T element)
		{
			throw new NotImplementedException();
		}

		public void Deduplicate()
		{
			throw new NotImplementedException();
		}

		public void Uniquify()
		{
			throw new NotImplementedException();
		}

		public void Traverse(Action<T> action)
		{
			throw new NotImplementedException();
		}
	}
}
