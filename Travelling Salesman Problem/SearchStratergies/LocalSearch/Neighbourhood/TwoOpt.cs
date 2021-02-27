using System;
using System.Collections.Generic;

namespace TSP.SearchStratergies.LocalSearch.Neighbourhood
{
    public class TwoOpt : INeighbourhood, ISwap
    {
        private static readonly Random random = new Random();

        public List<Route> GenerateNeighbourhood(Route parent)
        {
            var routes = new List<Route>();
            if (parent.IsCompleted)
            {
                int n = parent.RouteNodes.Count;
                for (int i = 1; i < (n / 2); i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        routes.Add(Swap(parent, i, j));
                    }
                }
            }
            return routes;
        }

        private static Route Swap(Route parent, int i, int j)
        {
            var routeNodes = new List<Node>(parent.RouteNodes);
            Node temp = routeNodes[i];
            routeNodes[i] = routeNodes[j];
            routeNodes[j] = temp;
            return new Route(routeNodes);
        }

        public Route Swap(Route parent)
        {
            int n = parent.RouteNodes.Count;
            return Swap(parent, random.Next(n / 2, n), random.Next(n / 2, n));
        }



    }
}
