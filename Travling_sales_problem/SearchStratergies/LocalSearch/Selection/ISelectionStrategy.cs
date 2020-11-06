using System.Collections.Generic;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunctions;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Selection
{
    interface ISelectionStrategy
    {
        Route[] Select(IEnumerable<Route> population, uint selectionSize, IStepFunction stepFunction);
    }
}
