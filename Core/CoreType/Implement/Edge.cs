using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Edge<T> : IEdge<T>, IComparable<Edge<T>> where T : IComparable<T>
    {
        public Edge(T data, int weight)
        {
            Data = data;
            Weight = weight;
            Status = EStatus.Undetermined;
        }

        #region Public
        public T Data { get; set; }
        public int Weight { get; set; }
        public EStatus Status { get; set; }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
        /// </returns>
        /// <param name="other">An object to compare with this instance. </param>
        public int CompareTo(Edge<T> other)
        {
            return this.Data.CompareTo(other.Data);
        }
        #endregion
    }
}
