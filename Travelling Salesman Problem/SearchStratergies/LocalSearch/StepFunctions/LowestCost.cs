using System;
using System.Collections.Generic;
using TSP.SearchStratergies.LocalSearch.StepFunctions;

namespace TSP.SearchStratergies.LocalSearch.StepFunction
{

    public class LowestCost : IStepFunction
    {
        /// <summary>
        /// Returns the <see cref="Route"/> with the lowest cost. 
        /// </summary>
        /// <seealso cref="Route.Distance()"/>
        /// <param name="routes"></param>
        /// <returns></returns>
        public Route Cost(IEnumerable<Route> routes)
        {
            Route? bestRoute = null;
            float bestRouteCost = float.MaxValue;

            foreach (Route route in routes)
            {
                float cost = route.Distance();
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
