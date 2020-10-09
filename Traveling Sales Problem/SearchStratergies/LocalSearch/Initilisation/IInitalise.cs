using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation
{
    interface IInitalise
    {
        public Route Initalise(List<Node> nodes);
    }
}
