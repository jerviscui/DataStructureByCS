using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreType.Implement;

namespace CoreType.Define
{
	public interface IMyList<T> where T : IComparable<T>
	{
		int Size();

		/// <summary>
		/// return the first node
		/// </summary>
		/// <returns></returns>
		MyListNode<T> First();

		/// <summary>
		/// return last node
		/// </summary>
		/// <returns></returns>
		MyListNode<T> Last();

		/// <summary>
		/// insert into header
		/// </summary>
		/// <param name="element"></param>
		void InsertAsFirst(T element);

		/// <summary>
		/// insert into trailer
		/// </summary>
		/// <param name="element"></param>
		void InsertAsLast(T element);

		/// <summary>
		/// insert element into source precursor
		/// </summary>
		/// <param name="source"></param>
		/// <param name="element"></param>
		void InsertBefore(MyListNode<T> source, T element);

		/// <summary>
		/// insert element into source succeed
		/// </summary>
		/// <param name="source"></param>
		/// <param name="element"></param>
		void InsertAfter(MyListNode<T> source, T element);

		int Remove(T element);

		bool Disordered();

		void Sort();

		MyListNode<T> Find(T element);

		MyListNode<T> Search(T element);

		void Deduplicate();

		void Uniquify();

		void Traverse(Action<T> action);
	}
}
