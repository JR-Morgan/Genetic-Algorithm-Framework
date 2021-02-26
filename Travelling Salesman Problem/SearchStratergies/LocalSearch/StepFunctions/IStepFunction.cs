using System.Collections.Generic;

namespace TSP.SearchStratergies.LocalSearch.StepFunctions
{
    interface IStepFunction
    {
        Route Cost(IEnumerable<Route> routes);

        Route CostP(params Route[] routes) => Cost(routes);
    }
}
