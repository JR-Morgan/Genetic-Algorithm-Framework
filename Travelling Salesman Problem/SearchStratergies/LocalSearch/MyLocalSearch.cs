using System.Collections.Generic;
using System.Threading;
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
        private const int DEFAULT_ITTERATIONS = 100;

        internal static List<ISearchStrategy> GenerateSearches(TerminateStrategy ts)
        {
            return new List<ISearchStrategy>()
            {
                new ExhaustiveSearch(),
                RND(ts),
                LS1(ts),
                LS2(),
                GN1(ts, 100, 20),
            };
        }

        public static List<ISearchStrategy> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));


        private static ISearchStrategy RND(TerminateStrategy ts) => new LS(
            initalise: new RandomInitalise(),
            neighbourhood: new NonNeighbourhood(),
            step: new LowestCost(),
            terminate: ts,
            name: "Random Search"
            );

        private static ISearchStrategy LS1(TerminateStrategy ts) => new LS(
            initalise: new RandomInitalise(),
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: ts,
            name: "Local Search - Random initalisation"
            );


        private static ISearchStrategy LS2() => new LS(
            initalise: new GreedyInitalise(),
            neighbourhood: new TwoOpt(),
            step: new LowestCost(),
            terminate: TerminalStrategies.FixedItterations(1),
            name: "Local Search - Greedy"
            );

        private static ISearchStrategy GN1(TerminateStrategy ts, uint populationSize, uint k, float elitism = 0.2f, float mutationRate = 0.04f) => new Evolution(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            crossoverStratergy: new OrderedCrossover(),
            swap: new TwoOpt(),
            stepFunction: new LowestCost(),
            terminate: ts,
            populationSize: populationSize,
            eliteism: elitism,
            mutationRate: mutationRate,
            name: "Evolutional Search - Tornement"
            );
        
    }
}
