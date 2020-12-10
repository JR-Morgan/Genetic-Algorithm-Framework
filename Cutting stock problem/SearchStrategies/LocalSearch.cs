﻿using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchStrategies
{
    public class LocalSearch<S,P> : ISearchStrategy<S,P>
    {
        private readonly IInitialise<S,P> initalisationStrategy;
        private readonly INeighbourhood<S> neighbourhood;
        private readonly ICostFunction<S> fitnessFunction;
        private readonly TerminateStrategy terminateStrategy;

        private int solutionsEvaluated;

        public LocalSearch(IInitialise<S,P> initalise, INeighbourhood<S> neighbourhood, ICostFunction<S> fitnessFunction, TerminateStrategy terminate, string name = "Local Search")
        {
            this.initalisationStrategy = initalise;
            this.neighbourhood = neighbourhood;
            this.fitnessFunction = fitnessFunction;
            this.terminateStrategy = terminate;
            this.name = name;
        }

        public event ISearchStrategy<S,P>.ItterationCompleteEventHandler? OnItterationComplete;


        public Log Compute(P problem)
        {
            TerminateCondition terminate = terminateStrategy();

            S? bestSolution = default;

            int itterationCounter = 0;
            Stopwatch timer = new();

            Log GenerateLog() => new Log() {
                    timeToCompute = timer.ElapsedMilliseconds,
                    numberOfSolutionsEvaluated = solutionsEvaluated,
                    iteration = itterationCounter,
                    bestSolutionFitness = bestSolution != null ? fitnessFunction.Cost(bestSolution) : float.PositiveInfinity,
                    bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
                };


            timer.Start();

            while (!terminate())
            {
                S parent = initalisationStrategy.Initialise(problem);
                S candidate = Search(parent);

                bestSolution = bestSolution == null ? candidate : fitnessFunction.FittestP(candidate, bestSolution);

                OnItterationComplete?.Invoke(this, GenerateLog());
            }


            timer.Stop();


            return GenerateLog();


        }


        private S Search(S parent)
        {

            List<S> neighbourhood = this.neighbourhood.GenerateNeighbourhood(parent);
            solutionsEvaluated += neighbourhood.Count;

            S best = fitnessFunction.Fittest(neighbourhood);

            if (fitnessFunction.Cost(best) < fitnessFunction.Cost(parent))
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
