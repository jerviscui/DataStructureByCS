using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreType.Define
{
    public interface IVector<T> 
    {
        int Size();

        /// <summary>
        /// get element of rank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        T Get(int rank);

        /// <summary>
        /// replace the rank with element
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="element"></param>
        void Put(int rank, T element);

        /// <summary>
        /// insert, element move backward which of after the rank 
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="element"></param>
        void Insert(int rank, T element);

        /// <summary>
        /// remove the rank element and return the deleted element
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        T Remove(int rank);

        /// <summary>
        /// all elements order by desc
        /// </summary>
        /// <returns></returns>
        bool Disordered();

        /// <summary>
        /// order by desc
        /// </summary>
        void Sort();

        /// <summary>
        /// find element, if not exist return -1
        /// -无序向量
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        int Find(T element);

        /// <summary>
        /// find element, if not exist return element rank which is the last only one smaller than the element
        /// -有序向量
        /// </summary>
        /// <returns></returns>
        int Search(T element);

        /// <summary>
        /// remove duplicate 移除-无序向量
        /// </summary>
        /// <returns></returns>
        int Deduplicate();


        /// <summary>
        /// 移除-有序向量
        /// </summary>
        /// <returns></returns>
        int Uniqufiy();

        /// <summary>
        /// 遍历 
        /// </summary>
        void Traverse(Action<T> action);
    }
}
