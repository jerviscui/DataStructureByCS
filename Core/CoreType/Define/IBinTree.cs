using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Implement;

namespace Core.CoreType.Define
{
    public interface IBinTree<T>
    {
        /// <summary>
        /// virtual
        /// </summary>
        /// <returns></returns>
        int UpdateHeight(IBinNode<T> node);

        /// <summary>
        /// update the height of all the parent nodes
        /// </summary>
        /// <param name="node"></param>
        void UpdateHeightAbove(IBinNode<T> node);

        int Size();

        int Height();

        bool Empty();

        IBinNode<T> Root();

        IBinNode<T> InsertAsLC(IBinNode<T> node, T data);

        IBinNode<T> InsertAsRC(IBinNode<T> node, T data);

        /// <summary>
        /// levelorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravLevel(Action<T> action);

        /// <summary>
        /// preorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravPre(Action<T> action);

        /// <summary>
        /// in sequence traversal
        /// </summary>
        /// <param name="action"></param>
        void TravIn(Action<T> action);

        /// <summary>
        /// postorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravPost(Action<T> action);
    }
}
