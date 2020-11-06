using System;
using TSP.SearchStratergies.LocalSearch.Initilisation;
using TSP.SearchStratergies.LocalSearch.Selection;
using TSP.SearchStratergies.LocalSearch.StepFunctions;
using TSP.SearchStratergies.LocalSearch.TerminalConditions;
using TSP.Solution_Stratergies;

namespace TSP.SearchStratergies.LocalSearch
{

    class Evolution : ISearchStratergy
    {

        private readonly IInitalise InitalisationStratergy;
        private readonly ISelectionStrategy selectionStrategy;
        private readonly IStepFunction step;
        private readonly NeighbourhoodGenerator Neighbourhood;
        private readonly Terminate Terminate;

        private uint selectionSize; //TODO set and might not need
        private Route[] population;

        public Evolution(IInitalise initalise, ISelectionStrategy selectionStrategy, NeighbourhoodGenerator neighbourhood, Terminate terminate, IStepFunction stepFunction, uint populationSize)
        {
            this.InitalisationStratergy = initalise;
            this.selectionStrategy = selectionStrategy;
            this.step = stepFunction;
            this.Neighbourhood = neighbourhood;
            this.Terminate = terminate;

            population = new Route[populationSize];

        }

        public event ISearchStratergy.ItterationCompleteEventHandler? OnItterationComplete;

        public Log Compute(Graph graph)
        {
            int numberOfRoutes = 0;
            //Initalise population
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = InitalisationStratergy.Initalise(graph.nodes);
            }


            Route? bestRoute = default;

            DateTime startTime = DateTime.Now;

            while (!Terminate())
            {
                Route[] parents = selectionStrategy.Select(population, selectionSize, step);

                //Recombine pairs of parents



                //Mutate the resulting offsprint

                //Evaluate new candidates

                //Select individuals for the next generation






                OnItterationComplete?.Invoke(this, new Log()
                {
                    numberOfRoutesEvaluated = numberOfRoutes,
                    bestRouteCost = bestRoute != null? bestRoute.Cost() : float.MaxValue,
                    timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds
                });

            }

            DateTime endTime = DateTime.Now;

            /*
            if (bestDistance != float.MaxValue)
            {
                Console.Write("{");
                foreach (Node n in bestRoute.RouteNodes)
                    Console.Write(n.id + ", ");
                Console.WriteLine("}");
            }

            return new Log()
            {
                ValidRoutes = numberOfRoutes,
                bestDistance = bestDistance,
                TimeToCompute = (float)endTime.Subtract(startTime).TotalMilliseconds
            };*/
            throw new NotImplementedException();
        }

        void ISearchStratergy.Compute(Graph graph)
        {
            throw new NotImplementedException();
        }
    }
}
