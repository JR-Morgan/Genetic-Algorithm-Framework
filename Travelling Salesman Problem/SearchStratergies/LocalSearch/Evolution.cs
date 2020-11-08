using System;
using TSP.SearchStratergies.LocalSearch.Crossover;
using TSP.SearchStratergies.LocalSearch.Initilisation;
using TSP.SearchStratergies.LocalSearch.Neighbourhood;
using TSP.SearchStratergies.LocalSearch.Selection;
using TSP.SearchStratergies.LocalSearch.StepFunctions;
using TSP.SearchStratergies.LocalSearch.TerminalConditions;
using TSP.Solution_Stratergies;

namespace TSP.SearchStratergies.LocalSearch
{

    class Evolution : ISearchStrategy
    {
        private static readonly Random random = new Random();
        private readonly IInitalise InitalisationStrategy;
        private readonly ISelectionStrategy selectionStrategy;
        private readonly ICrossover crossoverStratergy;
        private readonly IStepFunction step;
        private readonly ISwap swap;
        private readonly Terminate Terminate;

        private Route[] population;
        private float eliteism;
        private float mutationRate;

        public Evolution(IInitalise initalise, ISelectionStrategy selectionStrategy, ICrossover crossoverStratergy, ISwap swap, Terminate terminate, IStepFunction stepFunction, uint populationSize, float eliteism, float mutationRate, string name = "Evolution Search")
        {
            this.InitalisationStrategy = initalise;
            this.selectionStrategy = selectionStrategy;
            this.crossoverStratergy = crossoverStratergy;
            this.step = stepFunction;
            this.swap = swap;
            this.Terminate = terminate;
            this.name = name;
            this.eliteism = eliteism;
            this.mutationRate = mutationRate;
            population = new Route[populationSize];

        } 

        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;

        private Route[] Recombine(Route[] parents, int eliteCount)
        {
            Route[] children = new Route[parents.Length];

            Route[] pool = (Route[])parents.Clone();
            pool.Shuffle();

            for (int i = 0; i < eliteCount; i++)
            {
                children[i] = parents[i];
            }

            for (int i = eliteCount; i < parents.Length; i++)
            {
                children[i] = crossoverStratergy.CrossOver(parents[i], pool[parents.Length - i - 1]);
            }

            return children;

        }
        public void Compute(Graph graph)
        {
            int numberOfRoutes = 0;
            //Initalise population
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = InitalisationStrategy.Initalise(graph.nodes);
            }


            Route? bestRoute = default;

            DateTime startTime = DateTime.Now;
            int counter = 0;

            while (!Terminate())
            {
                Route[] parents = selectionStrategy.Select(population, 20, step);

                //Recombine pairs of parents
                Route[] children = Recombine(parents, (int)eliteism * parents.Length);

                //Mutate the resulting offsprint
                for (int i = 0; i < children.Length; i++)
                {
                    if (random.NextDouble() < mutationRate)
                    {
                        children[i] = swap.Swap(children[i]);
                    }

                    bestRoute = bestRoute == null ? children[i] : step.StepP(children[i], bestRoute);
                    population[counter] = children[i];
                    counter = (counter + 1) % population.Length;
                }
                


                OnItterationComplete?.Invoke(this, new Log()
                {
                    numberOfRoutesEvaluated = numberOfRoutes,
                    bestRouteCost = bestRoute != null? bestRoute.Cost() : float.MaxValue,
                    timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds
                });

            }

        }

        private string name;
        public override string ToString() => name;
    }
}
