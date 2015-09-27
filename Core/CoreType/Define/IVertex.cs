using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IVertex<T> : IComparable<T> where T : IComparable<T>
    {
        T Data { get; set; }

        int InDegree { get; set; }

        int OutDegree { get; set; }

        VStatus Status { get; set; }

        int DisTime { get; set; }

        int FTime { get; set; }

        int Parent { get; set; }

        int Priority { get; set; }
    }

    public enum VStatus
    {
        Undiscovered,
        Discovered,
        Visited
    }
}
