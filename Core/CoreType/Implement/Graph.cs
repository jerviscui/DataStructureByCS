using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Graph<TV, TE> : IGraph<TV, TE>
        where TV : IVertex<TV> 
        where TE : IEdge<TE> 
    {
    }
}
