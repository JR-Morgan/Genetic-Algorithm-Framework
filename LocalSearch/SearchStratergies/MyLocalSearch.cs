using Travling_sales_problem.SearchStratergies.LocalSearch;
using Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation;
using Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.TerminalConditions;

namespace Travling_sales_problem.Solution_Stratergies
{

    public static class MyLocalSearches
    {

        public static ISearchStratergy LS1(float timeout = 1000f) => new LocalSearch(
            initalise: new RandomInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: BestNeighbourhoodStep.Step,
            terminate: TerminateCondition.TimeOut(timeout)
            );

        public static ISearchStratergy LS2() => new LocalSearch(
            initalise: new GreedyInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: BestNeighbourhoodStep.Step,
            terminate: TerminateCondition.FixedItterations(1)
            );
    }
}
