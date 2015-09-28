using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IEdge<T> where T : IComparable<T>
    {
        T Data { get; set; }

        int Weight { get; set; }

        EStatus Status { get; set; }
    }

    public enum EStatus
    {
        Undetermined,
        Tree,
        Cross,
        Forward,
        Backward
    }
}
