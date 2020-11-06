using System.Collections.Generic;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood
{
    public static class TwoOptNeighbourhood
    {
        public static List<Route> GenerateNeighbourhood(Route parent)
        {
            var routes = new List<Route>();
            if (parent.IsCompleted)
            {
                int n = parent.RouteNodes.Count;
                for (int i = 1; i < (n / 2); i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        var routeNodes = new List<Node>(parent.RouteNodes);
                        Node temp = routeNodes[i];
                        routeNodes[i] = routeNodes[j];
                        routeNodes[j] = temp;
                        routes.Add(new Route(routeNodes));

                    }
                }
            }
            return routes;
        }
    }
}
