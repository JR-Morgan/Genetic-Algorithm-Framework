using System.Collections.Generic;
using TSP.SearchStratergies.LocalSearch.StepFunctions;

namespace TSP.SearchStratergies.LocalSearch.Selection
{
    interface ISelectionStrategy
    {
        Route[] Select(IEnumerable<Route> population, int selectionSize, IStepFunction stepFunction);
    }
}
