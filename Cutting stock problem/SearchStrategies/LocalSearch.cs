using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchStrategies
{
    public class LocalSearch<S,P> : ISearchStrategy<S,P>
    {
        private readonly IInitialise<S,P> initalisationStrategy;
        private readonly INeighbourhood<S> neighbourhood;
        private readonly IStepFunction<S> step;
        private readonly TerminateStrategy terminateStrategy;

        private int solutionsEvaluated;

        public LocalSearch(IInitialise<S,P> initalise, INeighbourhood<S> neighbourhood, IStepFunction<S> step, TerminateStrategy terminate, string name = "Local Search")
        {
            this.initalisationStrategy = initalise;
            this.neighbourhood = neighbourhood;
            this.step = step;
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
                    bestSolutionFitness = bestSolution != null ? step.Fitness(bestSolution) : float.PositiveInfinity,
                    bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
                };


            timer.Start();

            while (!terminate())
            {
                S parent = initalisationStrategy.Initalise(problem);
                S candidate = Search(parent);

                bestSolution = bestSolution == null ? candidate : step.FittestP(candidate, bestSolution);

                OnItterationComplete?.Invoke(this, GenerateLog());
            }


            timer.Stop();


            return GenerateLog();


        }


        private S Search(S parent)
        {

            List<S> neighbourhood = this.neighbourhood.GenerateNeighbourhood(parent);
            solutionsEvaluated += neighbourhood.Count;

            S best = step.Fittest(neighbourhood);

            if (step.Fitness(best) < step.Fitness(parent)) //TODO this violates step function delegation.
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
