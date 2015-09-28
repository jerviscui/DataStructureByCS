using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Vertex<T> : IVertex<T>, IComparable<Vertex<T>> where T : IComparable<T>
    {
        public Vertex(T data)
        {
            Data = data;
            InDegree = 0;
            OutDegree = 0;
            Status = VStatus.Undiscovered;
            DisTime = -1;
            FTime = -1;
            Parent = -1;
            Priority = int.MaxValue;
        }

        #region Public
        public T Data { get; set; }
        public int InDegree { get; set; }
        public int OutDegree { get; set; }
        public VStatus Status { get; set; }
        public int DisTime { get; set; }
        public int FTime { get; set; }
        public int Parent { get; set; }
        public int Priority { get; set; }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
        /// </returns>
        /// <param name="other">An object to compare with this instance. </param>
        public int CompareTo(Vertex<T> other)
        {
            return this.Data.CompareTo(other.Data);
        }
        #endregion
    }
}
