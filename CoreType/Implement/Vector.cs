using CoreType.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreType.Implement
{
    public class Vector<T> : IVector<T> where T : IComparable<T>
    {
        protected const int DefaultCapacity = 100;

        private int _size;
        private int _capacity;
        private T[] _element;

        //public T[] Element => _element;

        //public T[] Element
        //{
        //    get
        //    {
        //        return _element;
        //    }
        //}

        #region Private method

        private void CopyFrom(T[] array, int lowRank, int highRank)
        {
            _element = new T[2 * (highRank - lowRank)];
            _size = 0;

            while (lowRank < highRank)
            {
                _element[_size++] = array[lowRank++];
            }
        }

        private void Expend()
        {
            if (_size < _capacity)
            {
                return;
            }

            _capacity = Math.Max(_capacity, DefaultCapacity);

            T[] temp = _element;
            _element = new T[_capacity <<= 1];// => _capacity * 2

            for (int i = 0; i < _size; i++)
            {
                _element[i] = temp[i];
            }
        }

        private void Shink()
        {

        }

        private int Find(T element, int loRank, int hiRank)
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                //ruan comapre
                while (loRank < hiRank)
                {
                    var item = _element[hiRank--] as IComparable<T>;
                    if (item != null && item.CompareTo(element) == 0)
                    {
                        return hiRank;
                    }
                }
            }
            else
            {
                throw new ArgumentException(nameof(element) + " is not emplementation IComparable<>");
            }

            return -1;
        }
        #endregion

        public T this[int index]
        {
            get { return _element[index]; }
            set { _element[index] = value; }
        }

        public Vector(int length = DefaultCapacity)
        {
            _element = new T[_capacity = length];
            _size = 0;
        }

        public Vector(Vector<T> sourceVector, int length) : this(sourceVector, 0, length - 1)
        {

        }

        public Vector(Vector<T> sourceVector, int lowRank, int highRank)
        {
            T[] temp = new T[sourceVector.Size()];
            for (int i = 0; i < sourceVector.Size(); i++)
            {
                temp[i] = sourceVector[i];
            }

            CopyFrom(temp, lowRank, highRank);
        }

        public Vector(T[] array, int lowRank, int highRank)
        {
            CopyFrom(array, lowRank, highRank);
        }

        public Vector(T[] array, int length) : this(array, 0, length - 1)
        {

        }

        ~Vector()
        {
            _element = null;
        }

        #region Implementation
        public int Size()
        {
            return _size;
        }

        /// <summary>
        /// get element of rank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public T Get(int rank)
        {
            if (rank > _capacity - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            return _element[rank];
        }

        /// <summary>
        /// replace the rank with element
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="element"></param>
        public void Put(int rank, T element)
        {
            if (rank > _capacity - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            _element[rank] = element;
        }

        /// <summary>
        /// insert, element move backward which of after the rank 
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="element"></param>
        public void Insert(int rank, T element)
        {
            if (rank > _capacity - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            Expend();
            for (int i = _size; i < _size - rank; i++)
            {
                _element[i] = _element[i - 1];
            }
            _element[rank] = element;
            _size++;
        }

        /// <summary>
        /// remove the rank element and return the deleted element
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public T Remove(int rank)
        {
            if (rank > _capacity - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            T temp = _element[rank];
            for (int i = rank; i < _size; i++)
            {
                _element[rank] = _element[rank + 1];
            }
            _size--;

            return temp;
        }

        /// <summary>
        /// find element, if not exist return -1
        /// -无序向量
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int Find(T element)
        {
            return Find(element, 0, _size - 1);
        }

        /// <summary>
        /// find element, if not exist return element rank which is the last only one smaller than the element
        /// -有序向量
        /// </summary>
        /// <returns></returns>
        public int Search(T element)
        {
            int rank = Find(element);
            if (rank == -1)
            {
                int index = _size - 1;
                bool has = false;
                for (int i = index; i > 0; i--)
                {
                    var item = _element[i] as IComparable<T>;
                    if (item != null && item.CompareTo(element) == -1)
                    {
                        has = true;
                        if (item.CompareTo(_element[index]) == 1)
                        {
                            index = i;
                        }
                    }
                }
                if (has)
                {
                    return index;
                }
            }

            return rank;
        }

        /// <summary>
        /// if all elements order by desc return true
        /// </summary>
        /// <returns></returns>
        public bool Disordered()
        {
            int len = _size;
            while (--len > 1)
            {
                var item = _element[len] as IComparable<T>;
                if (item.CompareTo(_element[len - 1]) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// order by desc
        /// </summary>
        public void Sort()
        {
            if (Disordered())
            {
                return;
            }

            T temp;
            for (int i = 0; i < _size; i++)
            {
                for (int j = i + 1; j < _size; j++)
                {
                    var item = _element[i] as IComparable<T>;
                    if (item.CompareTo(_element[j]) == -1)
                    {
                        temp = _element[i];
                        _element[i] = _element[j];
                        _element[j] = temp;
                    }
                }
            }
        }


        /// <summary>
        /// remove duplicate 移除-无序向量
        /// </summary>
        /// <returns></returns>
        public int Deduplicate()
        {
            int count = 0;
            for (int i = 1; i < _size; i++)
            {
                if (Find(_element[i], 0, i) != -1)
                {
                    Remove(i);
                    count ++;
                }
            }
            _size -= count;

            return count;
        }


        /// <summary>
        /// 移除-有序向量
        /// </summary>
        /// <returns></returns>
        public int Uniqufiy()
        {
            int index = 1;
            if (Disordered())
            {
                for (int i = 1; i < _size; i++)
                {
                    var item = _element[i] as IComparable<T>;
                    if (item.CompareTo(_element[i - 1]) != 0)
                    {
                        _element[index ++] = _element[i];
                    }
                }

                int count = _size - (index + 1);
                _size = index + 1;
                return count;
            }
            else
            {
                return Deduplicate();
            }
        }

        /// <summary>
        /// 遍历 
        /// </summary>
        public void Traverse(Action<T> action)
        {
            for (int i = 0; i < _size; i++)
            {
                action(_element[i]);
            }
        }
        #endregion

    }
}
