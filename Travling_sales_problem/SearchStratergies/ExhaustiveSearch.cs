
using System;
using System.Collections.Generic;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunctions;

namespace Travling_sales_problem.Solution_Stratergies
{
    public class ExhaustiveSearch : ISearchStratergy
    {
        public event ISearchStratergy.ItterationCompleteEventHandler? OnItterationComplete;


        IStepFunction step;

        public ExhaustiveSearch()
        {
            step = new LowestCost();
        }


        public void Compute(Graph graph)
        {
            DateTime startTime = DateTime.Now;

            List<Route> routes = CalculateAllValidRoutes(graph);

            Route? bestRoute = default;
            foreach (Route route in routes)
            {

                bestRoute = bestRoute == null? route : step.StepP(bestRoute, route);


                OnItterationComplete?.Invoke(this, new Log()
                {
                    bestRouteCost = bestRoute.Cost(),
                    timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds,
                    numberOfRoutesEvaluated = routes.Count
                });
            }

        }

        private static List<Route> CalculateAllValidRoutes(Graph graph)
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
