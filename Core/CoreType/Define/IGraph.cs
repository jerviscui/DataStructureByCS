using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreType.Define
{
    public interface IGraph<TV, TE>
        where TV : IVertex<TV>
        where TE : IEdge<TE>
    {

    }
}
