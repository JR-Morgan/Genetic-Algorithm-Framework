using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood
{
    interface INeighbourhood
    {
        public List<Route> GenerateNeighbourhood(Route parent);
    }
}
