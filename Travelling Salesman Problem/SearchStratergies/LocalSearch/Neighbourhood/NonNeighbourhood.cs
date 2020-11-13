using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.SearchStratergies.LocalSearch.Neighbourhood
{
    /// <summary>
    /// Does not generate a neighbourhood. It simply returns the parent
    /// </summary>
    class NonNeighbourhood : INeighbourhood
    {

        public List<Route> GenerateNeighbourhood(Route parent)
        {
            return new List<Route> { parent };
        }
    }
}
