using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TSP.SearchStratergies.LocalSearch.Initilisation;
using TSP.SearchStratergies.LocalSearch.Neighbourhood;
using TSP.SearchStratergies.LocalSearch.StepFunctions;
using TSP.SearchStratergies.LocalSearch.TerminalConditions;
using TSP.Solution_Stratergies;

namespace TSP.SearchStratergies.LocalSearch
{

    class LocalSearch : ISearchStrategy
    {

        private readonly IInitalise initalisationStrategy;
        private readonly INeighbourhood neighbourhood;
        private readonly IStepFunction step;
        private readonly TerminateStrategy terminateCondition;

        private int numberOfRoutes;
        
        
        public LocalSearch(IInitalise initalise, INeighbourhood neighbourhood, IStepFunction step, TerminateStrategy terminate, string name = "Local Search")
        {
            this.initalisationStrategy = initalise;
            this.neighbourhood = neighbourhood;
            this.step = step;
            this.terminateCondition = terminate;
            this.name = name;
        }

        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;

        public void Compute(Graph graph)
        {
            TerminateCondition terminate = terminateCondition();

            Route? bestRoute = default;

            DateTime startTime = DateTime.Now;

            while (!terminate())
            {
                Route parent = initalisationStrategy.Initalise(graph.nodes);
                var candidate = Search(parent);

                bestRoute = bestRoute == null? candidate : step.StepP(candidate, bestRoute);

                OnItterationComplete?.Invoke(this, new Log() {  numberOfRoutesEvaluated = numberOfRoutes,
                                                                bestRouteCost = bestRoute.Cost(),
                                                                timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds
                                                              });
            }


        }

        private Route Search(Route parent)
        {

            List<Route> neighbourhood = this.neighbourhood.GenerateNeighbourhood(parent);
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

        private readonly string name;
        public override string ToString() => name;
    }
}
