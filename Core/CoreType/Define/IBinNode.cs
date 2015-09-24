using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IBinNode<T>
    {
        /// <summary>
        /// all children number
        /// </summary>
        /// <returns></returns>
        int Size();

        /// <summary>
        /// hight of the tree with this for root
        /// </summary>
        int Height { get; set; }

        IBinNode<T> LChild();

        IBinNode<T> RChild();

        IBinNode<T> Parent();

            /// <summary>
        /// insert left child 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IBinNode<T> insertAsLC(T data);

        /// <summary>
        /// insert right child
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IBinNode<T> insertAsRC(T data);

        /// <summary>
        /// in sequence traversal of the succeed
        /// </summary>
        /// <returns></returns>
        IBinNode<T> Succ();

        /// <summary>
        /// levelorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravLevel(Action action);

        /// <summary>
        /// preorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravPre(Action action);

        /// <summary>
        /// in sequence traversal
        /// </summary>
        /// <param name="action"></param>
        void TravIn(Action action);

        /// <summary>
        /// postorder traversal
        /// </summary>
        /// <param name="action"></param>
        void TravPost(Action action);
    }
}
