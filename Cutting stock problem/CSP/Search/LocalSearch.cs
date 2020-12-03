using CSP.Search.Initialisation;
using CSP.Search.Neighbourhood;
using CSP.Search.StepFunctions;
using CSP.Search.TerminalConditions;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSP.Search
{
    class LocalSearch : ISearchStrategy
    {
        private readonly IInitialise initalisationStrategy;
        private readonly INeighbourhood neighbourhood;
        private readonly IStepFunction step;
        private readonly TerminateStrategy terminateStrategy;

        private int solutionsEvaluated;

        public LocalSearch(IInitialise initalise, INeighbourhood neighbourhood, IStepFunction step, TerminateStrategy terminate, string name = "Local Search")
        {
            this.initalisationStrategy = initalise;
            this.neighbourhood = neighbourhood;
            this.step = step;
            this.terminateStrategy = terminate;
            this.name = name;
        }

        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;


        public Log Compute(Problem problem)
        {
            TerminateCondition terminate = terminateStrategy();

            ISolution? bestSolution = default;

            int itterationCounter = 0;
            Stopwatch timer = new();

            Log GenerateLog() => new Log() {
                    timeToCompute = timer.ElapsedMilliseconds,
                    numberOfSolutionsEvaluated = solutionsEvaluated,
                    iteration = itterationCounter,
                    bestSolutionFitness = bestSolution != null ? bestSolution.Fitness() : float.PositiveInfinity,
                    bestSolution = bestSolution != null ? bestSolution.ToString() : string.Empty,
                };


            timer.Start();

            while (!terminate())
            {
                ISolution parent = initalisationStrategy.Initalise(problem);
                ISolution candidate = Search(parent);

                bestSolution = bestSolution == null ? candidate : step.FitnessP(candidate, bestSolution);

                OnItterationComplete?.Invoke(this, GenerateLog());
            }


            timer.Stop();


            return GenerateLog();


        }


        private ISolution Search(ISolution parent)
        {

            List<ISolution> neighbourhood = this.neighbourhood.GenerateNeighbourhood(parent);
            solutionsEvaluated += neighbourhood.Count;

            ISolution best = step.Fitness(neighbourhood);

            if (best.Fitness() < parent.Fitness()) //TODO this violates step function delegation.
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
