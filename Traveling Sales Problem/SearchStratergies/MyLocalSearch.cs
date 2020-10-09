using Travling_sales_problem.SearchStratergies.LocalSearch;
using Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation;
using Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.TerminalConditions;

namespace Travling_sales_problem.Solution_Stratergies
{

    static class MyLocalSearches
    {

        public static LocalSearch LS1() => new LocalSearch(
            initalise: RandomInitalise.Initalise,
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: BestNeighbourhoodStep.Step,
            terminate: TerminateCondition.TimeOut(1000f)
            );

        public static LocalSearch LS2() => new LocalSearch(
            initalise: GreedyInitalise.Initalise,
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: BestNeighbourhoodStep.Step,
            terminate: TerminateCondition.FixedItterations(1)
            );
    }
}
