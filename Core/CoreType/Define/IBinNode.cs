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

        NodeColor Color { get; set; }

        IBinNode<T> LChild { get; set; }

        IBinNode<T> RChild { get; set; }

        IBinNode<T> Parent { get; set; }

        T Data { get; set; }

        /// <summary>
        /// insert left child 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IBinNode<T> InsertAsLC(T data);

        /// <summary>
        /// insert right child
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IBinNode<T> InsertAsRC(T data);

        /// <summary>
        /// in sequence traversal of the succeed
        /// </summary>
        /// <returns></returns>
        IBinNode<T> Succ();

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

    public enum NodeColor
    {
        Red = 1,
        Black = 2
    }
}
