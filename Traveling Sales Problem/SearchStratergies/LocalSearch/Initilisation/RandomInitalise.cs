using System;
using System.Collections.Generic;
using System.Linq;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation
{
    static class RandomInitalise
    {

        public static Route Initalise(List<Node> nodes)
        {
            List<Node> routes = nodes.OrderBy(a => Guid.NewGuid()).ToList();


            return new Route()
            {
                RouteNodes = routes,
                ExpectedFinalNodeCount = nodes.Count
            };
        }
    }
}
