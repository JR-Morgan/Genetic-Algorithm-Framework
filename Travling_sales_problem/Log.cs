using System;
using System.Collections.Generic;
using System.Text;

namespace Travling_sales_problem
{
    public struct Log
    {
        public int numberOfRoutesEvaluated;
        public float bestRouteCost;
        public float timeToCompute;

        public override string ToString()
        {
            return $"{numberOfRoutesEvaluated} valid routes found\n" +
                $"Took {timeToCompute}ms to compute\n" +
                $"fastest route is {bestRouteCost}\n";
                
        }

    }
}
