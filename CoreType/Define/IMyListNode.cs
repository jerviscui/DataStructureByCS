using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreType.Implement;

namespace CoreType.Define
{
	public interface IMyListNode<T> where T : IComparable<T>
	{
		/// <summary>
		/// insert a precursor element
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		MyListNode<T> InsertAsPred(T data);

		/// <summary>
		/// insert a succeed element
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		MyListNode<T> InsertAsSucc(T data);
	}
}
