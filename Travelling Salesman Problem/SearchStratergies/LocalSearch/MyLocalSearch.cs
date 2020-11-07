using TSP.SearchStratergies.LocalSearch;
using TSP.SearchStratergies.LocalSearch.Initilisation;
using TSP.SearchStratergies.LocalSearch.Neighbourhood;
using TSP.SearchStratergies.LocalSearch.Selection;
using TSP.SearchStratergies.LocalSearch.StepFunction;
using TSP.SearchStratergies.LocalSearch.TerminalConditions;
using LS = TSP.SearchStratergies.LocalSearch.LocalSearch;

namespace TSP.Solution_Stratergies.LocalSearch
{

    public static class MyLocalSearches
    {

        public static ISearchStrategy LS1(float timeout = 1000f) => new LS(
            initalise: new RandomInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: new LowestCost(),
            terminate: TerminateCondition.TimeOut(timeout),
            name: "Random Local Search"
            );

        public static ISearchStrategy LS2() => new LS(
            initalise: new GreedyInitalise(),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            step: new LowestCost(),
            terminate: TerminateCondition.FixedItterations(1),
            name: "Greedy Local Search"
            );

        public static ISearchStrategy GN1(uint populationSize, uint k, float timeout = 1000f) => new Evolution(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            neighbourhood: TwoOptNeighbourhood.GenerateNeighbourhood,
            stepFunction: new LowestCost(),
            terminate: TerminateCondition.TimeOut(timeout),
            populationSize: populationSize,
            name: "Tornament Search"
            );
        
    }
}
