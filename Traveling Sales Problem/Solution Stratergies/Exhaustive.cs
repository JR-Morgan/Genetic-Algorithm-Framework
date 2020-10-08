using System;
using System.Collections.Generic;
using System.Text;

namespace Travling_sales_problem.Solution_Stratergies
{
    class Exhaustive : ISolution
    {

        public Log Compute(Graph graph)
        {


            DateTime startTime = DateTime.Now;

            List<Route> routes = this.CalculateAllValidRoutes(graph);

            Route bestRoute;
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

            return new Log()
            {
                FastestRoute = bestDistance,
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

        /*private static void _CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
        {
            if (lastRoute.IsCompleted)
            {
                routes.Add(lastRoute);
            }
            else
            {
                foreach (Node node in neighbours)
                {
                    Route currentRoute = lastRoute.Copy();
                    if (currentRoute.Add(node))
                    {
                        CalculateRoute(currentRoute, routes, neighbours);
                    }

                }
            }

        }*/

    }
}
