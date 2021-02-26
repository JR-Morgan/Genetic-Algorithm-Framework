
using System;
using System.Collections.Generic;
using TSP.SearchStratergies.LocalSearch.StepFunction;
using TSP.SearchStratergies.LocalSearch.StepFunctions;

namespace TSP.Solution_Stratergies
{
    /// <summary>
    /// Exhaustive search will check all possible permutations for the <see cref="Route"/> with the lowest cost.
    /// </summary>
    public class ExhaustiveSearch : ISearchStrategy
    {
        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;
        public override string ToString() => "Exhaustive Search";

        readonly IStepFunction step;

        public ExhaustiveSearch() : this(new LowestCost())
        { }

        internal ExhaustiveSearch(IStepFunction step)
        {
            this.step = step;
        }


        public Log Compute(Graph graph)
        {
            DateTime startTime = DateTime.Now;

            List<Route> routes = CalculateAllValidRoutes(graph);
            int routesEvaluated = 0;


            Route? bestRoute = default;
            foreach (Route route in routes)
            {
                routesEvaluated++;
                bestRoute = bestRoute == null? route : step.CostP(bestRoute, route);


                OnItterationComplete?.Invoke(this, new Log()
                {
                    timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds,
                    numberOfRoutesEvaluated = routesEvaluated,
                    iteration = routesEvaluated,
                    bestRouteCost = bestRoute != null ? bestRoute.Distance() : float.MaxValue,
                    bestRoute = bestRoute != null ? bestRoute.ToString() : string.Empty,
                });
            }

            return new Log()
            {
                timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds,
                numberOfRoutesEvaluated = routesEvaluated,
                iteration = routesEvaluated,
                bestRouteCost = bestRoute != null ? bestRoute.Distance() : float.MaxValue,
                bestRoute = bestRoute != null ? bestRoute.ToString() : string.Empty,
            };

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
