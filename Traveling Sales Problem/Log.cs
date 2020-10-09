using System;
using System.Collections.Generic;
using System.Text;

namespace Travling_sales_problem
{
    struct Log
    {
        public int ValidRoutes;
        public float bestDistance;
        public float TimeToCompute;

        public override string ToString()
        {
            return $"{ValidRoutes} valid routes found\n" +
                $"Took {TimeToCompute}ms to compute\n" +
                $"fastest route is {bestDistance}\n";
                
        }

    }
}
