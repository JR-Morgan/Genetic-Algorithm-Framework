using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly TerminateStrategy terminateStrategy;

        private int routesEvaluated;
        
        
        public LocalSearch(IInitalise initalise, INeighbourhood neighbourhood, IStepFunction step, TerminateStrategy terminate, string name = "Local Search")
        {
            this.initalisationStrategy = initalise;
            this.neighbourhood = neighbourhood;
            this.step = step;
            this.terminateStrategy = terminate;
            this.name = name;
        }

        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;

        public Log Compute(Graph graph)
        {
            TerminateCondition terminate = terminateStrategy();

            Route? bestRoute = default;

            int itterationCounter = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            
            while (!terminate())
            {
                Route parent = initalisationStrategy.Initalise(graph.nodes);
                Route candidate = Search(parent);

                bestRoute = bestRoute == null? candidate : step.CostP(candidate, bestRoute);

                OnItterationComplete?.Invoke(this, new Log() {  numberOfRoutesEvaluated = routesEvaluated,
                                                                bestRouteCost = bestRoute.Distance(),
                                                                iteration = itterationCounter++,
                                                                bestRoute = bestRoute != null ? bestRoute.ToString() : String.Empty,
                                                                timeToCompute = timer.ElapsedMilliseconds,
                });
            }

            timer.Stop();
            return new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfRoutesEvaluated = routesEvaluated,
                iteration = itterationCounter,
                bestRouteCost = bestRoute != null ? bestRoute.Distance() : float.PositiveInfinity,
                bestRoute = bestRoute != null ? bestRoute.ToString() : String.Empty,
            };

        }

        private Route Search(Route parent)
        {

            List<Route> neighbourhood = this.neighbourhood.GenerateNeighbourhood(parent);
            routesEvaluated += neighbourhood.Count;

            Route best = step.Cost(neighbourhood);

            if (best.Distance() < parent.Distance()) //TODO this violates step function delegation.
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
