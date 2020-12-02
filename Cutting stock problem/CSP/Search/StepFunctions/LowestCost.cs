using System;
using System.Collections.Generic;

namespace CSP.Search.StepFunctions
{
    class LowestCost : IStepFunction
    {
        public ISolution Fitness(IEnumerable<ISolution> solutions)
        {
            ISolution? bestRoute = null;
            float bestRouteCost = float.MaxValue;

            foreach (ISolution s in solutions)
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
    }
}
