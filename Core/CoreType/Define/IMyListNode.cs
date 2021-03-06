﻿using System;
using Core.CoreType.Implement;

namespace Core.CoreType.Define
{
	public interface IMyListNode<T> where T : IComparable<T>
	{
        /// <summary>
        /// insert a precursor element
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IMyListNode<T> InsertAsPred(T data);

        /// <summary>
        /// insert a succeed element
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IMyListNode<T> InsertAsSucc(T data);
	}
}
