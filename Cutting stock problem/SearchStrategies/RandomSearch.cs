using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchStrategies
{
    public class RandomSearch<S, P> : ISearchStrategy<S, P>
    {
        private readonly IInitialise<S, P> initalisationStrategy;
        private readonly ICostFunction<S> fitnessFunction;
        private readonly TerminateStrategy terminateStrategy;

        public RandomSearch(IInitialise<S, P> initalise, ICostFunction<S> fitnessFunction, TerminateStrategy terminate, string name = "random Search")
        {
            this.initalisationStrategy = initalise;
            this.fitnessFunction = fitnessFunction;
            this.terminateStrategy = terminate;
            this.name = name;
        }

        public event ISearchStrategy<S, P>.ItterationCompleteEventHandler? OnItterationComplete;


        public Log Compute(P problem)
        {
            TerminateCondition terminate = terminateStrategy();

            S? bestSolution = default;
            float bestCost = float.PositiveInfinity;

            int itterationCounter = 0;
            Stopwatch timer = new();

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = itterationCounter,
                iteration = itterationCounter,
                bestSolutionFitness = bestCost,
                bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
            };


            timer.Start();

            while (!terminate())
            {
                S parent = initalisationStrategy.Initialise(problem);
                //S candidate = Search(parent);


                itterationCounter++;
                float cost = fitnessFunction.Cost(parent);
                if (cost < bestCost)
                {
                    bestSolution = parent;
                    bestCost = cost;
                }

                System.Console.WriteLine(parent + " | " + cost);
                //OnItterationComplete?.Invoke(this, GenerateLog());
            }


            timer.Stop();


            return GenerateLog();


        }


        private readonly string name;
        public override string ToString() => name;
    }
}