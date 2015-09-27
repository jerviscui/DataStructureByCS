using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class GraphMatrix<TV, TE> : Graph<TV, TE> where TV : IVertex<TV> where TE : IEdge<TE>
    {
        private Vector<TV> _vertexes;

        private Vector<Vector<TE>> _edges;


    }
}
