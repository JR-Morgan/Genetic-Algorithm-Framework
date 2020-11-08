using TSP.SearchStratergies.LocalSearch;
using TSP.SearchStratergies.LocalSearch.Crossover;
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
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: TerminateCondition.FixedItterations(100), //TerminateCondition.TimeOut(timeout),
            name: "Random Local Search"
            );


        public static ISearchStrategy LS2() => new LS(
            initalise: new GreedyInitalise(),
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: TerminateCondition.FixedItterations(1),
            name: "Greedy Local Search"
            );

        public static ISearchStrategy GN1(uint populationSize, uint k, float elitism = 0.1f, float mutationRate = 0.1f, float timeout = 1000f) => new Evolution(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            crossoverStratergy: new OrderedCrossover(),
            swap: new TwoOpt(),
            stepFunction: new LowestCost(),
            terminate: TerminateCondition.FixedItterations(100), //TerminateCondition.TimeOut(timeout),
            populationSize: populationSize,
            eliteism: elitism,
            mutationRate: mutationRate,
            name: "Tornament Search"
            );
        
    }
}
