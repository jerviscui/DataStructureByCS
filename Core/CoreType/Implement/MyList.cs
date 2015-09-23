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

        private MyListNode<T> Search1(T dest, int count, MyListNode<T> start)
        {
            while (start.Precursor != null && count-- > 0)
            {
	            start = start.Precursor;
                if (start.Data.CompareTo(dest) < 1)
                {
                    return start;
                }
            }

			return start.Precursor;
        }

        /// <summary>
        /// from the start element, sort the count's number lenth
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void SelectionSort(MyListNode<T> start, int count)
        {
            //get lastest element
            MyListNode<T> last = start;
            for (int i = 0; i < count; i++)
            {
                last = last.Succeed;
            }

            while (count > 1)
            {
                MyListNode<T>max = SelectMax(start, count--);
                last = last.Precursor;
                T data = max.Data;
                max.Data = last.Data;
                last.Data = data;
            }
        }

        private MyListNode<T> SelectMax(MyListNode<T> start, int count)
        {
            //can't use binary search, this is a disordered list

            // Painters algorithm 画家算法
            //是指后来的值会覆盖temp的值，像涂油画颜料一样
            MyListNode<T> temp = start;

            while (count-- > 1 && start.Succeed != null)
            {
                start = start.Succeed;
                //temp >= start for select the lastest max element
                if (start.CompareTo(temp) > -1)
                {
                    temp = start;
                }
            }

            return temp;
        }

		private void InsertionSort(MyListNode<T> start, int count)
	    {
			for (int i = 0; i < count; i++)
			{
				InsertAfter(Search1(start.Data, i, start), start.Data);
				start = start.Succeed;
				Remove(start.Precursor);
			}
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
            while (index++ < start)
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
                while (index-- > 0)
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
                while (index-- > 0)
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

		public T Remove(MyListNode<T> element)
		{
			element.Precursor.Succeed = element.Succeed;
			element.Succeed.Precursor = element.Precursor;
			_size--;
		    T data = element.Data;
			element.Dispose();

			return data;
		}

		public bool Disordered()
		{
			if (_size < 2)
			{
				return true;
			}

			MyListNode<T> next = First().Succeed;
			int index = 1;
			while (index++ < _size)
			{
				if (next.CompareTo(next.Precursor) == -1)
				{
					return false;
				}
			}

			return true;
		}

		public void Sort()
		{
			//SelectionSort(First(), _size);
			//return;

			InsertionSort(First(), _size);
		}

		public MyListNode<T> Find(T element)
		{
			return Find(element, _size, _trailer);
		}

		public MyListNode<T> Search(T element)
		{
			return Search1(element, _size, _trailer);
		}

		public int Deduplicate()
		{
			//if size <= 0 then return
			//and we can futher narrow range
			if (_size < 2)
			{
				return 0;
			}

			MyListNode<T> item = First();
			int rank = 1;
			int count = 0;
			while (item.Succeed != null)
			{
				item = item.Succeed;
				var temp = Find(item.Data, rank, item);
				if (temp != null)
				{
					Remove(temp);
					count++;
				}
				else
				{
					rank++;
				}
			}

			return count;
		}

		public int Uniquify()
		{
			if (_size < 2
				)
			{
				return 0;
			}

			int count = 0;
			MyListNode<T> item = First();
			MyListNode<T> next = item.Succeed;
			while (next != _trailer)
			{
				if (item.CompareTo(next) == 0)
				{
					next = next.Succeed;
					count++;
				}
				else
				{
					item.Succeed = next;
					next.Precursor = item;
					item = item.Succeed;
					next = item.Succeed;
				}
			}

			_size -= count;
			return count;
		}

		public void Traverse(Action<T> action)
		{
			MyListNode<T> next = First();
			while (next != null && next != _trailer)
			{
				action(next.Data);
				next = next.Succeed;
			}
		}

        #endregion
    }
}
