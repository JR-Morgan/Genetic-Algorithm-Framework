using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchStrategies
{
    public class EvolutionarySearch<S, P> : ISearchStrategy<S, P>
    {
        protected static readonly Random random = new Random();
        private S[] population;

        protected readonly IInitialise<S,P> initalisationStrategy;
        protected readonly Generation<S> generationStrategy;
        protected readonly ICostFunction<S> fitnessFunction;
        private readonly TerminateStrategy terminateStrategy;

        public EvolutionarySearch(IInitialise<S, P> initalise, Generation<S> generationStrategy, TerminateStrategy terminate, ICostFunction<S> fitnessFunction, uint populationSize, string name = "Evolution Search")
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
            //Initialise population
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = initalisationStrategy.Initalise(problem);
            }

            //TESTING ONLY
            S bestSolution = fitnessFunction.FittestP(population);
            Console.WriteLine(population[1]);


            Stopwatch timer = new Stopwatch();
            
            int generationCounter = 0;
            int itterationCounter = 0;

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = solutionsEvaluated,
                iteration = itterationCounter,
                bestSolutionFitness = fitnessFunction.Cost(bestSolution),
                bestSolution = bestSolution != null? bestSolution.ToString() : string.Empty,
            };

            timer.Start();
            OnItterationComplete?.Invoke(this, GenerateLog());

            while (!Terminate())
            {
                //Create next Generation
                IList<S> children = generationStrategy.NextGeneration(population, fitnessFunction);

                //Evaluate the new generation and replace the oldest in the population
                for (int i = 0; i < children.Count; i++)
                {
                    //Evaluate
                    solutionsEvaluated++;
                    bestSolution = fitnessFunction.FittestP(children[i], bestSolution);

                    //Replace
                    population[generationCounter] = children[i];
                    generationCounter = (generationCounter + 1) % population.Length;
                }

                //TESTING ONLY
                Console.WriteLine(children[^1]);

                OnItterationComplete?.Invoke(this, GenerateLog());

            }
            timer.Stop();
            return GenerateLog();
        }

        private string name;
        public override string ToString() => name;
    }
}
