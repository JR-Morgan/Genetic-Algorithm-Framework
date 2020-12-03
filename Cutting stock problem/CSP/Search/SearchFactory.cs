using CSP.Search.Initialisation;
using CSP.Search.Neighbourhood;
using CSP.Search.StepFunctions;
using SearchStrategies;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System.Collections.Generic;

namespace CSP.Search
{
    public static class SearchFactory
    {
        private const float DEFAULT_TIMEOUT = 100000f;
        private const int DEFAULT_ITTERATIONS = 5000;


        internal static List<ISearchStrategy<ISolution, Problem>> GenerateSearches(TerminateStrategy ts)
        {
            return new ()
            {
                LS1(ts),
            };
        }

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));


        private static ISearchStrategy<ISolution, Problem> LS1(TerminateStrategy ts) => new LocalSearch<ISolution, Problem>(
        initalise: new RandomInitalise(),
        neighbourhood: new StockSwap(true),
        step: new LowestCost(),
        terminate: ts,
        name: "Local Search - Random initialisations"
        );




    }
}
