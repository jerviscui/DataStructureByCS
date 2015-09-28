using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Graph<TV, TE> : IGraph<TV, TE>
    {
        /// <summary>
        /// return vertex size
        /// </summary>
        /// <returns></returns>
        private int Length()
        {
            return 0;
        }

        private void Reset()
        {
            for (int i = 0; i < Length(); i++)
            {
                //vertexes infos reset
                //Status DisTime FTime Parent Priority
                for (int j = 0; j < Length(); j++)
                {
                    //edges infos reset
                    //Status
                }
            }
        }
    }
}
