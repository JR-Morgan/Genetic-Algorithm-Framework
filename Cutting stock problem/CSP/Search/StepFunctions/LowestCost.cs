using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.StepFunctions
{
    class LowestCost : IStepFunction<ISolution>
    {
        public ISolution Fittest(IEnumerable<ISolution> solutions)
        {
            Solution? bestRoute = null;
            float bestRouteCost = float.MaxValue;

            foreach (Solution s in solutions)
            {
                float cost = s.Fitness();
                if (cost < bestRouteCost)
                {
                    bestRoute = s;
                    bestRouteCost = cost;
                }
            }

            return bestRoute ?? throw new Exception("Could not find best route");
        }

        public float Fitness(ISolution solution) => solution.Fitness();
    }
}
