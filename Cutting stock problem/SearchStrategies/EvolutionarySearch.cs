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
        private IList<S> population;

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
            //Initialise population
            for (int i = 0; i < population.Count; i++)
            {
                population[i] = initalisationStrategy.Initialise(problem);
            }



            //Setup Run
            int solutionsEvaluated = 0, itterationCounter = 0;

            S bestSolution = fitnessFunction.Fittest(population);

            Stopwatch timer = new Stopwatch();

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = solutionsEvaluated,
                iteration = itterationCounter,
                bestSolutionFitness = fitnessFunction.Cost(bestSolution),
                bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
            };

            void Evaluate(S child)
            {
                solutionsEvaluated++;
                bestSolution = fitnessFunction.FittestP(child, bestSolution);
            }


            TerminateCondition Terminate = this.terminateStrategy();


           
            timer.Start();
            OnItterationComplete?.Invoke(this, GenerateLog());


           

            while (!Terminate())
            {
                //Replace generation
                population = generationStrategy.NextGeneration(population, fitnessFunction, Evaluate);

                //TESTING
                Console.WriteLine(population[1]);




                itterationCounter++;
                OnItterationComplete?.Invoke(this, GenerateLog());

            }
            timer.Stop();
            return GenerateLog();
        }

        private string name;
        public override string ToString() => name;
    }
}
