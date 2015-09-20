using System;
using Core.CoreType.Implement;

namespace Core.CoreType.Define
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

        T Remove(MyListNode<T> element);

		bool Disordered();

		void Sort();

		MyListNode<T> Find(T element);

		MyListNode<T> Search(T element);

        int Deduplicate();

        int Uniquify();

		void Traverse(Action<T> action);
	}
}
