using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.StepFunctions
{
    class LowestCost : ICostFunction<ISolution>
    {
        public ISolution Fittest(IEnumerable<ISolution> solutions)
        {
            ISolution? bestSolution = null;
            float bestSolutionCost = float.PositiveInfinity;

            foreach (ISolution s in solutions)
            {
                float cost = s.Cost();
                if (cost < bestSolutionCost)
                {
                    bestSolution = s;
                    bestSolutionCost = cost;
                }
            }

            return bestSolution ?? throw new Exception("Could not find best solution");
        }

        public float Cost(ISolution solution) => solution.Cost();
    }
}
