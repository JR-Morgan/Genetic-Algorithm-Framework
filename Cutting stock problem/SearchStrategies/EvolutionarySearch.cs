using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Diagnostics;

namespace SearchStrategies
{
    public class EvolutionarySearch<S, P> : ISearchStrategy<S, P>
    {
        protected static readonly Random random = new Random();
        private S[] population;

        protected readonly IInitialise<S,P> initalisationStrategy;
        protected readonly IGenerationStrategy<S> generationStrategy;
        protected readonly IFitnessFunction<S> fitnessFunction;
        private readonly TerminateStrategy terminateStrategy;

        public EvolutionarySearch(IInitialise<S, P> initalise, Generation<S> generationStrategy, TerminateStrategy terminate, IFitnessFunction<S> fitnessFunction, uint populationSize, string name = "Evolution Search")
        {
            this.initalisationStrategy = initalise;
            this.generationStrategy = generationStrategy;
            this.fitnessFunction = fitnessFunction;
            this.terminateStrategy = terminate;
            this.name = name;
            population = new S[populationSize];

        }

        public event ISearchStrategy<S,P>.ItterationCompleteEventHandler? OnItterationComplete;

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
                bestSolutionFitness = bestSolution != null ? fitnessFunction.Fitness(bestSolution) : float.PositiveInfinity,
                bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
            };

            timer.Start();

            while (!Terminate())
            {
                //Create next Generation
                S[] children = generationStrategy.NextGeneration(population, fitnessFunction);

                //Evaluate the new generation
                for (int i = 0; i < children.Length; i++)
                {
                    solutionsEvaluated++;
                    bestSolution = bestSolution == null ? children[i] : fitnessFunction.FittestP(children[i], bestSolution);
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
