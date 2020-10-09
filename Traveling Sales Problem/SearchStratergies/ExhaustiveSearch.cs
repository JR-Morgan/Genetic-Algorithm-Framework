
using System;
using System.Collections.Generic;

namespace Travling_sales_problem.Solution_Stratergies
{
    class ExhaustiveSearch : ISearchStratergy
    {
        public Log Compute(Graph graph)
        {
            DateTime startTime = DateTime.Now;

            List<Route> routes = this.CalculateAllValidRoutes(graph);

            Route bestRoute = default;
            float bestDistance = float.MaxValue;
            foreach (Route route in routes)
            {
                float distance = route.EvaluateDistance();
                if (distance < bestDistance)
                {
                    bestRoute = route;
                    bestDistance = distance;
                }
            }
            DateTime endTime = DateTime.Now;

            if (bestRoute.IsCompleted)
            {
                Console.Write("{");
                foreach (Node n in bestRoute.RouteNodes)
                    Console.Write(n.id + ", ");
                Console.WriteLine("}");
            }
            
            return new Log()
            {
                bestDistance = bestDistance,
                TimeToCompute = (float)endTime.Subtract(startTime).TotalMilliseconds,
                ValidRoutes = routes.Count
            };


        }

        private List<Route> CalculateAllValidRoutes(Graph graph)
        {
            List<Route> routes = new List<Route>();

            CalculateRoute(new Route(graph.StartNode, graph.nodes.Count), routes, graph.nodes);

            return routes;
        }


        private static void CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
        {
            if (lastRoute.IsCompleted)
            {
                routes.Add(lastRoute);
            }
            else
            {
                foreach (Node node in neighbours)
                {
                    if (lastRoute.AddCheck(node))
                    {
                        Route currentRoute = lastRoute.Copy();
                        currentRoute.AddUnchecked(node);
                        CalculateRoute(currentRoute, routes, neighbours);
                    }

                }
            }

        }
    }
}
