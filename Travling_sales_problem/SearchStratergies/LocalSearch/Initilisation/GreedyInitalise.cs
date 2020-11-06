using System;
using System.Collections.Generic;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation
{
    public class GreedyInitalise : IInitalise
    {

        public Route Initalise(List<Node> nodes)
        {
            List<Node> possibleNodes = new List<Node>(nodes);
            possibleNodes.Remove(nodes[0]);
            Route greedyRoute = new Route(nodes[0], nodes.Count);


            while (!greedyRoute.IsCompleted) {
                Node nextNode = ClosestNode(greedyRoute.RouteNodes[^1], possibleNodes);
                possibleNodes.Remove(nextNode);
                if (!greedyRoute.Add(nextNode)) throw new Exception("Not supposed to be invalid");
            }

            return greedyRoute;
        }

        public static Node ClosestNode(Node startNode, IEnumerable<Node> neighbours)
        {
            Node? bestNode = null;
            float bestDistance = float.MaxValue;
            foreach(Node neighbour in neighbours)
            {
                float distance = startNode.DistanceTo(neighbour);
                if (distance < bestDistance)
                {
                    bestNode = neighbour;
                    bestDistance = distance;
                }
            }

            return bestNode ?? throw new ArgumentException("Collection was empty", nameof(neighbours));
        }

    }
}
