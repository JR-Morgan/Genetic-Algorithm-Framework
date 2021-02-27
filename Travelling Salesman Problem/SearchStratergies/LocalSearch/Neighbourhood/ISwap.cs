using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.SearchStratergies.LocalSearch.Neighbourhood
{
    interface ISwap
    {
        Route Swap(Route parent);
    }
}
