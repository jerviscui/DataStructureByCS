/*
** all of the array interval is left closed right away
** [lo,hi)
*/

using System;
using Core.Common;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Vector<T> : IVector<T> where T : IComparable<T>
    {
        protected const int DefaultCapacity = 100;

        private int _size;
        private int _capacity;
        private T[] _element;

        /// <summary>
        /// C# 6.0
        /// </summary>
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
            _capacity = 2 * (highRank - lowRank);
            _element = new T[_capacity];
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
            while (loRank < hiRank-- && element.CompareTo(_element[hiRank]) != 0)
            {
                ;
            }

            return hiRank;
        }

        private int BinarySearch(T element, int loRank, int hiRank)
        {
            while (loRank < hiRank)
            {
                int mi = loRank + (hiRank - loRank) / 2;
                if (element.CompareTo(_element[mi]) == -1)
                {
                    hiRank = mi;
                }
                else if (_element[mi].CompareTo(element) == -1)
                {
                    loRank = mi + 1;
                }
                else
                {
                    return mi;
                }
            }

            return -1;
        }

        /// <summary>
        /// 二分查找的改进版，
        /// 最好的情况略坏，但是最坏的情况会变好（右侧分支步长每次+1）
        /// </summary>
        /// <param name="element"></param>
        /// <param name="loRank"></param>
        /// <param name="hiRank"></param>
        /// <returns></returns>
        private int BinarySearch2(T element, int loRank, int hiRank)
        {
            while (hiRank - loRank > 1)
            {
                int mi = loRank + (hiRank - loRank) / 2;
                if (element.CompareTo(_element[mi]) == -1)
                {
                    hiRank = mi;
                }
                else
                {
                    loRank = mi;
                }
            }

            return element.CompareTo(_element[loRank]) == 0 ? loRank : -1;

        }

        /// <summary>
        /// 二分查找的改进版，
        /// 再次改进，以使符合查找算法的语义
        /// 中间元素小于目标元素就舍去左边，那么剩下右边的部分一定是大于等于目标元素，
        /// 那么，在中间元素右边的部分存在全部大于目标元素或者大于和等于目标元素
        /// 直到最后一刻 low==high 扫描结束，
        /// 那么此时low位置的左边一定是小于等于目标 而右边一定是大于目标，所以找到了不大于目标元素的最右边元素的位置
        /// </summary>
        /// <param name="element"></param>
        /// <param name="loRank"></param>
        /// <param name="hiRank"></param>
        /// <returns></returns>
        private int BinarySearch3(T element, int loRank, int hiRank)
        {
            while (hiRank > loRank)
            {
                int mi = loRank + (hiRank - loRank) / 2;
                if (element.CompareTo(_element[mi]) == -1)
                {
                    hiRank = mi;
                }
                else
                {
                    loRank = mi + 1;
                }
            }

            return loRank - 1;
        }

        private int FibonacciSearch(T element, int loRank, int hiRank)
        {
            Fibonacci fib = new Fibonacci(hiRank - loRank);
            while (loRank < hiRank)
            {
                while (hiRank - loRank < fib.Get())
                {
                    fib.Prev();
                }

                int mi = loRank + fib.Get() - 1;
                if (element.CompareTo(_element[mi]) == -1)
                {
                    hiRank = mi;
                }
                else if (_element[mi].CompareTo(element) == 1)
                {
                    loRank = mi + 1;
                }
                else
                {
                    return mi;
                }
            }

            return -1;
        }

        private void SwapForSort1(int loRank, ref int hiRank)
        {
            if (loRank > hiRank || loRank < 0 || hiRank > _size)
            {
                throw new ArgumentOutOfRangeException("loRank");
            }

            for (int i = loRank; i < hiRank - 1; i++)
            {
                if (_element[i].CompareTo(_element[i + 1]) == 1)
                {
                    T temp = _element[i];
                    _element[i] = _element[i + 1];
                    _element[i + 1] = temp;
                }
            }

            hiRank--;
        }

        /// <summary>
        /// 返回标志此次比较结果是否已经是有序向量
        /// </summary>
        /// <param name="loRank"></param>
        /// <param name="hiRank"></param>
        private bool SwapForSort2(int loRank, ref int hiRank)
        {
            if (loRank > hiRank || loRank < 0 || hiRank > _size)
            {
                throw new ArgumentOutOfRangeException("loRank");
            }

            bool isSorted = true;
            for (int i = loRank; i < hiRank - 1; i++)
            {
                if (_element[i].CompareTo(_element[i + 1]) == 1)
                {
                    isSorted = false;
                    T temp = _element[i];
                    _element[i] = _element[i + 1];
                    _element[i + 1] = temp;
                }
            }

            hiRank--;
            return isSorted;
        }

        /// <summary>
        /// 修改右侧标志位，以再下次比对中可以排除有序部分
        /// </summary>
        /// <param name="loRank"></param>
        /// <param name="hiRank"></param>
        private bool SwapForSort3(int loRank, ref int hiRank)
        {
            if (loRank > hiRank || loRank < 0 || hiRank > _size)
            {
                throw new ArgumentOutOfRangeException("loRank");
            }

            bool isSorted = true;
            int rank = 0;
            for (int i = loRank; i < hiRank - 1; i++)
            {
                if (_element[i].CompareTo(_element[i + 1]) == 1)
                {
                    isSorted = false;
                    T temp = _element[i];
                    _element[i] = _element[i + 1];
                    _element[i + 1] = temp;
                    rank = i + 1;
                }
            }

            hiRank = rank;
            return isSorted;
        }

        /// <summary>
        /// 2-way merge sort
        /// </summary>
        /// <param name="loRank"></param>
        /// <param name="hiRank"></param>
        private void MergeSort(int loRank, int hiRank)
        {
            //递归基、
            if (hiRank - loRank < 2)
            {
                return;
            }

            int mi = loRank + ((hiRank - loRank) >> 1);

            MergeSort(loRank, mi);
            MergeSort(mi, hiRank);
            Merge(loRank, mi, hiRank);

        }

        private void Merge(int loRank, int middle, int hiRank)
        {
            int lLen = middle - loRank;
            int rLen = hiRank - middle;
            T[] temp = new T[lLen];
            for (int i = 0; i < lLen; i++)
            {
                temp[i] = _element[loRank + i];
            }

            int index = loRank;
            for (int i = 0, j = 0; i < lLen;)
            {
                //短路求值
                if (j >= rLen || j < rLen && temp[i].CompareTo(_element[j + middle]) < 1)
                {
                    _element[index++] = temp[i++];
                }

                if (j < rLen && i < lLen && temp[i].CompareTo(_element[j + middle]) == 1)
                {
                    _element[index++] = _element[middle + j++];
                }
            }
        }
        #endregion

        public T this[int index]
        {
            get
            {
                if (index > _size - 1 || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return _element[index];
            }
            set
            {
                if (index > _size - 1 || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                _element[index] = value;
            }
        }

        public Vector(int length = DefaultCapacity)
        {
            _element = new T[_capacity = length];
            _size = 0;
        }

        public Vector(Vector<T> sourceVector, int length)
                    : this(sourceVector, 0, length)
        {

        }

        public Vector(Vector<T> sourceVector, int lowRank, int highRank)
        {
            T[] temp = new T[highRank - lowRank];
            for (int i = 0; i < highRank - lowRank; i++)
            {
                temp[i] = sourceVector[i + lowRank];
            }

            CopyFrom(temp, 0, highRank - lowRank);
        }

        public Vector(T[] array, int lowRank, int highRank)
        {
            CopyFrom(array, lowRank, highRank);
        }

        public Vector(T[] array, int length)
                    : this(array, 0, length)
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
            if (rank > _size - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException("rank");
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
            if (rank > _size - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException("rank");
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
            if (rank > _size || rank < 0)
            {
                throw new ArgumentOutOfRangeException("rank");
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
            if (rank > _size - 1 || rank < 0)
            {
                throw new ArgumentOutOfRangeException("rank");
            }

            T temp = _element[rank];
            while (rank < _size)
            {
                _element[rank] = _element[++rank];
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
            return Find(element, 0, _size);
        }

        /// <summary>
        /// find element, if not exist return element rank which is smaller than or equal the element and the lastest only one
        /// -有序向量
        /// </summary>
        /// <returns></returns>
        public int Search(T element)
        {
            //return BinarySearch(element, 0, _size);

            //return FibonacciSearch(element, 0, _size);

            //return BinarySearch2(element, 0, _size);

            return BinarySearch3(element, 0, _size);

            //int rank = Find(element);
            //if (rank == -1)
            //{
            //    int index = _size - 1;
            //    bool has = false;
            //    for (int i = index; i > 0; i--)
            //    {
            //        var item = _element[i] as IComparable<T>;
            //        if (item != null && item.CompareTo(element) == -1)
            //        {
            //            has = true;
            //            if (item.CompareTo(_element[index]) == 1)
            //            {
            //                index = i;
            //            }
            //        }
            //    }
            //    if (has)
            //    {
            //        return index;
            //    }
            //}

            //return rank;
        }

        /// <summary>
        /// if all elements order by asc return true
        /// </summary>
        /// <returns></returns>
        public bool Disordered()
        {
            int len = _size;
            while (--len > 1)
            {
                if (_element[len].CompareTo(_element[len - 1]) == -1)
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
            int loRank = 0;
            int hiRank = _size;
            //while 循环遍历
            //while (hiRank - loRank > 1)
            //{
            //	SwapForSort1(loRank, ref hiRank);
            //}
            //return;

            //改进Sort算法，若前次已经有序则后面不需要再循环
            //while (!SwapForSort2(loRank, ref hiRank) && hiRank - loRank > 1)
            //{
            //	;
            //}
            //return;

            //再次改进，找到右侧已经有序的部分，并在后面的比对中排除
            //while (!SwapForSort3(loRank, ref hiRank) && hiRank - loRank > 1)
            //{
            //    ;
            //}
            //return;

            //merge sort
            MergeSort(loRank, _size);
            return;

            if (Disordered())
            {
                return;
            }

            //V1
            for (int i = 0; i < _size; i++)
            {
                for (int j = i + 1; j < _size; j++)
                {
                    var item = _element[i] as IComparable<T>;
                    if (item.CompareTo(_element[j]) == -1)
                    {
                        T temp = _element[i];
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
            for (int i = 1; i < _size;)
            {
                if (Find(_element[i], 0, i) != -1)
                {
                    Remove(i);
                    count++;
                }
                else
                {
                    i++;
                }
            }

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
                    if (_element[i].CompareTo(_element[i - 1]) != 0)
                    {
                        _element[index++] = _element[i];
                    }
                }

                int count = _size - index;
                _size = index;
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
