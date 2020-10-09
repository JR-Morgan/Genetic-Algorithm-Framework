using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction
{
    interface IStep
    {
        public Route? Step(IEnumerable<Route> neighbourhood);
    }
}
