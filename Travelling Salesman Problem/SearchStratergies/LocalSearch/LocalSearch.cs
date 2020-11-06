using System;
using System.Collections.Generic;
using TSP.SearchStratergies.LocalSearch.Initilisation;
using TSP.SearchStratergies.LocalSearch.StepFunctions;
using TSP.SearchStratergies.LocalSearch.TerminalConditions;
using TSP.Solution_Stratergies;

namespace TSP.SearchStratergies.LocalSearch
{
    internal delegate List<Route> NeighbourhoodGenerator(Route parent);


    class LocalSearch : ISearchStratergy
    {

        private readonly IInitalise initalisationStratergy;
        private readonly NeighbourhoodGenerator neighbourhood;
        private readonly IStepFunction step;
        private readonly Terminate terminate;

        private int numberOfRoutes;

        public LocalSearch(IInitalise initalise, NeighbourhoodGenerator neighbourhood, IStepFunction step, Terminate terminate)
        {
            this.initalisationStratergy = initalise;
            this.neighbourhood = neighbourhood;
            this.step = step;
            this.terminate = terminate;
        }

        public event ISearchStratergy.ItterationCompleteEventHandler? OnItterationComplete;

        public void Compute(Graph graph)
        {
            Route? bestRoute = default;

            DateTime startTime = DateTime.Now;

            while (!terminate())
            {
                Route parent = initalisationStratergy.Initalise(graph.nodes);
                var candidate = Search(parent);
                //TODO use step function

                bestRoute = bestRoute == null? candidate : step.StepP(candidate, bestRoute);

                OnItterationComplete?.Invoke(this, new Log() {  numberOfRoutesEvaluated = numberOfRoutes,
                                                                bestRouteCost = bestRoute.Cost(),
                                                                timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds
                                                              });
            }


        }

        private Route Search(Route parent)
        {

            List<Route> neighbourhood = this.neighbourhood(parent);
            numberOfRoutes += neighbourhood.Count;

            Route best = step.Step(neighbourhood);

            if (best.Cost() < parent.Cost()) //TODO this violates step function delegation.
            {
                return Search(best);
            }
            else
            {
                return parent;
            }

        }
    }
}
