using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.SearchStratergies.LocalSearch.Neighbourhood
{
    interface INeighbourhood
    {
        List<Route> GenerateNeighbourhood(Route parent);
    }
}
