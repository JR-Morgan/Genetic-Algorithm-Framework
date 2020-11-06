using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.StepFunctions
{
    interface IStepFunction
    {
        Route Step(IEnumerable<Route> routes);

        Route StepP(params Route[] routes) => Step(routes);
    }
}
