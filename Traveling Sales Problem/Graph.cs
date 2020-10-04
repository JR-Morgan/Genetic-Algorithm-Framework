using System;
using System.Collections.Generic;
using System.Linq;

namespace Travling_sales_problem
{
    class Graph
    {
        private readonly List<Node> nodes;
        private Node StartNode => nodes.First();

        public Graph(List<Node> nodes)
        {
            this.nodes = nodes;
        }


        public Log Compute()
        {
            

            DateTime startTime = DateTime.Now;

            List<Route> routes = this.CalculateAllValidRoutes();

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

        private List<Route> CalculateAllValidRoutes()
        {
            List<Route> routes = new List<Route>();

            CalculateRoute(new Route(StartNode, nodes.Count), routes, nodes);

            return routes;
        }


        private static void CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
        {
            if(lastRoute.IsCompleted)
            {
                routes.Add(lastRoute);
            }
            else
            {
                foreach (Node node in neighbours)
                {
                    if(lastRoute.AddCheck(node))
                    {
                        Route currentRoute = lastRoute.Copy();
                        currentRoute.AddUnchecked(node);
                        CalculateRoute(currentRoute, routes, neighbours);
                    }

                }
            }

        }

        private static void _CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
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

        }




        /*private static void CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
        {
            
            foreach(Node node in neighbours)
            {
                Route currentRoute = lastRoute.Copy();
                if(currentRoute.Add(node))
                {
                    if (currentRoute.IsCompleted)
                    {
                        routes.Add(currentRoute);
                    }
                    else
                    {
                        Route nextRoute = currentRoute;
                        CalculateRoute(nextRoute, routes, neighbours);
                    }
                }
            }
        }*/

    }
}
