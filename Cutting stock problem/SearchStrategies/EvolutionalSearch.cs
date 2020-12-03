using SearchExtensions;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Diagnostics;

namespace SearchStrategies
{
    public class EvolutionalSearch<S, P> : ISearchStrategy<S, P>
    {
        private static readonly Random random = new Random();
        private readonly IInitialise<S,P> initalisationStrategy;
        private readonly ISelectionStrategy<S> selectionStrategy;
        private readonly ICrossover<S> crossoverStratergy;
        private readonly IStepFunction<S> step;
        private readonly ISwap<S> swap;
        private readonly TerminateStrategy terminateStrategy;

        private S[] population;
        private float eliteism;
        private float mutationRate;

        public EvolutionalSearch(IInitialise<S, P> initalise, ISelectionStrategy<S> selectionStrategy, ICrossover<S> crossoverStratergy, ISwap<S> swap, TerminateStrategy terminate, IStepFunction<S> stepFunction, uint populationSize, float eliteism, float mutationRate, string name = "Evolution Search")
        {
            this.initalisationStrategy = initalise;
            this.selectionStrategy = selectionStrategy;
            this.crossoverStratergy = crossoverStratergy;
            this.step = stepFunction;
            this.swap = swap;
            this.terminateStrategy = terminate;
            this.name = name;
            this.eliteism = eliteism;
            this.mutationRate = mutationRate;
            population = new S[populationSize];

        }

        public event ISearchStrategy<S,P>.ItterationCompleteEventHandler? OnItterationComplete;

        private S[] Recombine(S[] parents, int eliteCount)
        {
            S[] children = new S[parents.Length];

            S[] pool = (S[])parents.Clone();
            pool.Shuffle(random);

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
        public Log Compute(P problem)
        {
            
            TerminateCondition Terminate = this.terminateStrategy();
            int solutionsEvaluated = 0;
            //Initalise population
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = initalisationStrategy.Initalise(problem);
            }


            S? bestSolution = default;

            Stopwatch timer = new Stopwatch();
            
            int generationCounter = 0;
            int itterationCounter = 0;

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = solutionsEvaluated,
                iteration = itterationCounter,
                bestSolutionFitness = bestSolution != null ? step.Fitness(bestSolution) : float.PositiveInfinity,
                bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
            };

            timer.Start();

            while (!Terminate())
            {
                S[] parents = selectionStrategy.Select(population, 20, step);

                //Recombine pairs of parents
                S[] children = Recombine(parents, (int)eliteism * parents.Length);

                //Mutate the resulting offsprint
                for (int i = 0; i < children.Length; i++)
                {
                    if (random.NextDouble() < mutationRate)
                    {
                        children[i] = swap.Swap(children[i]);
                    }
                    solutionsEvaluated++;
                    bestSolution = bestSolution == null ? children[i] : step.FittestP(children[i], bestSolution);
                    population[generationCounter] = children[i];
                    generationCounter = (generationCounter + 1) % population.Length;
                }



                OnItterationComplete?.Invoke(this, GenerateLog());

            }
            timer.Stop();
            return GenerateLog();
        }

        private string name;
        public override string ToString() => name;
    }
}
