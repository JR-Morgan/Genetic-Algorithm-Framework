using System.Collections.Generic;

namespace TSP.SearchStratergies.LocalSearch.StepFunctions
{
    interface IStepFunction
    {
        Route Step(IEnumerable<Route> routes);

        Route StepP(params Route[] routes) => Step(routes);
    }
}
