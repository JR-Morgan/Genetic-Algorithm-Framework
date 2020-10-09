using System;
using System.Collections.Generic;
using Travling_sales_problem.Solution_Stratergies;

namespace Travling_sales_problem.SearchStratergies.LocalSearch
{
    internal delegate Route Initalise(List<Node> nodes);
    internal delegate List<Route> GenerateNeighbourhood(Route parent);
    internal delegate (Route best, float distance) Step(IEnumerable<Route> neighbourhood);
    internal delegate bool Terminate();

    abstract class LocalSearch : ISearchStratergy
    {

        private readonly Initalise Initalise;
        private readonly GenerateNeighbourhood Neighbourhood;
        private readonly Step Step;
        private readonly Terminate Terminate;

        private int numberOfRoutes;

        public LocalSearch(Initalise initalise, GenerateNeighbourhood neighbourhood, Step step, Terminate terminate)
        {
            this.Initalise = initalise;
            this.Neighbourhood = neighbourhood;
            this.Step = step;
            this.Terminate = terminate;
        }

        public Log Compute(Graph graph)
        {
            Route bestRoute;
            float bestDistance = float.MaxValue;

            DateTime startTime = DateTime.Now;
            while (!Terminate())
            {
                Route parent = Initalise(graph.nodes);
                var (best, distance) = Search(parent, parent.EvaluateDistance());
                //TODO use step function
                if (distance < bestDistance)
                {
                    bestRoute = best;
                    bestDistance = distance;
                };
            }
            DateTime endTime = DateTime.Now;


            return new Log()
            {
                ValidRoutes = numberOfRoutes,
                FastestRoute = bestDistance,
                TimeToCompute = (float)endTime.Subtract(startTime).TotalMilliseconds
            };

        }

        public (Route best, float distance) Search(Route parent, float parentDistance)
        {

            List<Route> neighbourhood = this.Neighbourhood(parent);
            numberOfRoutes += neighbourhood.Count;

            var (best, distance) = Step(neighbourhood);
            if (distance < parentDistance)
            {
                return Search(best, distance); ;
            }
            else
            {
                return (parent, parentDistance);
            }

        }
    }
}
