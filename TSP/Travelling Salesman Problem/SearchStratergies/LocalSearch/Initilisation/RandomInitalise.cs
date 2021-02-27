using System.Collections.Generic;

namespace TSP.SearchStratergies.LocalSearch.Initilisation
{
    class RandomInitalise : IInitalise
    {

        public Route Initalise(List<Node> nodes)
        {
            List<Node> routes = new List<Node>(nodes);
            routes.RemoveAt(0);
            routes.Shuffle();
            routes.Add(nodes[0]);
            routes[^1] = routes[0];
            routes[0] = nodes[0];


            return new Route(routes);
        }

    }
}
