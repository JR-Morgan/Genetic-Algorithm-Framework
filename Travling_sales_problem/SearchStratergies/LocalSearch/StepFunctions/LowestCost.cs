using System;
using System.Collections.Generic;
using System.ComponentModel;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunctions;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction
{
    public class LowestCost : IStepFunction
    {
        public Route Step(IEnumerable<Route> routes)
        {
            Route? bestRoute = null;
            float bestRouteCost = float.MaxValue;

            foreach (Route route in routes)
            {
                float cost = route.Cost();
                if (cost < bestRouteCost)
                {
                    bestRoute = route;
                    bestRouteCost = cost;
                }
            }

            return bestRoute ?? throw new Exception("Could not find best route");
        }

    }
}
