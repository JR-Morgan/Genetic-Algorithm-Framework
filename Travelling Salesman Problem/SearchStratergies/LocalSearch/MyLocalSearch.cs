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
        private const float DEFAULT_TIMEOUT = 10000f;

        public static ISearchStrategy LS1(float timeout = DEFAULT_TIMEOUT) => new LS(
            initalise: new RandomInitalise(),
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: TerminateConditions.TimeOut(timeout),
            name: "Random Local Search"
            );


        public static ISearchStrategy LS2() => new LS(
            initalise: new GreedyInitalise(),
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: TerminateConditions.FixedItterations(1),
            name: "Greedy Local Search"
            );

        public static ISearchStrategy GN1(uint populationSize, uint k, float elitism = 0.1f, float mutationRate = 0.01f, float timeout = DEFAULT_TIMEOUT) => new Evolution(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            crossoverStratergy: new OrderedCrossover(),
            swap: new TwoOpt(),
            stepFunction: new LowestCost(),
            terminate: TerminateConditions.TimeOut(timeout),
            populationSize: populationSize,
            eliteism: elitism,
            mutationRate: mutationRate,
            name: "Tornament Search"
            );
        
    }
}
