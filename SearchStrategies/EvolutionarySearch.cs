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
        protected readonly Random random;
        public IList<S> Population { get; set; }

        protected readonly IInitialise<S,P> initalisationStrategy;
        protected readonly Generation<S> generationStrategy;
        protected readonly ICostFunction<S> fitnessFunction;
        protected readonly TerminateStrategy terminateStrategy;
        protected readonly uint populationSize;
        public S? BestSolution { get; set; }

        public EvolutionarySearch(IInitialise<S, P> initalise, Generation<S> generationStrategy, TerminateStrategy terminate, ICostFunction<S> fitnessFunction, uint populationSize, Random random, string name = "Evolution Search")
        {
            this.random = random;
            this.initalisationStrategy = initalise;
            this.generationStrategy = generationStrategy;
            this.fitnessFunction = fitnessFunction;
            this.terminateStrategy = terminate;
            this.populationSize = populationSize;
            this.name = name;
            this.Population = new S[populationSize];
        }



        public event ISearchStrategy<S,P>.ItterationCompleteEventHandler? OnItterationComplete;

        public void InitialisePopulation(int size, P problem) => Population = GetInitialisePopulation(size, problem, initalisationStrategy);
        protected static IList<S> GetInitialisePopulation(int size, P problem, IInitialise<S, P> initalisation)
        {
            List<S> population = new(size);
            for (int i = 0; i < size; i++)
            {
                population.Add(initalisation.Initialise(problem));
            }
            return population;
        }


        public virtual Log Compute(P problem) => Compute(problem, true);

        public Log Compute(P problem, bool initalisePop)
        {
            if (initalisePop)
            {
                InitialisePopulation((int)populationSize, problem);

            }

            //Setup Run
            int solutionsEvaluated = 0, itterationCounter = 0;
            BestSolution ??= fitnessFunction.Fittest(Population);//Assumes that atleast one member of the population is valid

            Stopwatch timer = new Stopwatch();

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = solutionsEvaluated,
                iteration = itterationCounter,
                bestSolutionFitness = fitnessFunction.Cost(BestSolution),
                bestSolution = BestSolution != null ? BestSolution.ToString() : string.Empty,
            };

            void Evaluate(S child)
            {
                solutionsEvaluated++;
                BestSolution = fitnessFunction.Fittest(child, BestSolution);
            }


            TerminateCondition Terminate = this.terminateStrategy();


           
            timer.Start();
            OnItterationComplete?.Invoke(this, GenerateLog());


           

            while (!Terminate())
            {
                //Replace generation
                Population = generationStrategy.NextGeneration(Population, fitnessFunction, Evaluate);
             




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
