using LS = Travling_sales_problem.SearchStratergies.LocalSearch.LocalSearch;
using Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation;
using Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.TerminalConditions;
using Travling_sales_problem.SearchStratergies.LocalSearch;
using Travling_sales_problem.SearchStratergies.LocalSearch.Selection;

namespace Travling_sales_problem.Solution_Stratergies.LocalSearch
{

    public static class MyLocalSearches
    {

        public static ISearchStratergy LS1(float timeout = 1000f) => new LS(
            initalise: new RandomInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: new LowestCost(),
            terminate: TerminateCondition.TimeOut(timeout)
            );

        public static ISearchStratergy LS2() => new LS(
            initalise: new GreedyInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: new LowestCost(),
            terminate: TerminateCondition.FixedItterations(1)
            );

        public static ISearchStratergy GN1(uint populationSize, uint k, float timeout = 1000f) => new Evolution(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            stepFunction: new LowestCost(),
            terminate: TerminateCondition.TimeOut(timeout),
            populationSize: populationSize
            );
        
    }
}
