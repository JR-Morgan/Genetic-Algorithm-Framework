using CSP.Search.Initialisation;
using CSP.Search.Neighbourhood;
using CSP.Search.StepFunctions;
using CSP.Search.TerminalConditions;
using System.Collections.Generic;

namespace CSP.Search
{
    public static class SearchFactory
    {
        private const float DEFAULT_TIMEOUT = 100000f;
        private const int DEFAULT_ITTERATIONS = 500;


        internal static List<ISearchStrategy> GenerateSearches(TerminateStrategy ts)
        {
            return new List<ISearchStrategy>()
            {
                LS1(ts),
            };
        }

        public static List<ISearchStrategy> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));


        private static ISearchStrategy LS1(TerminateStrategy ts) => new LocalSearch(
        initalise: new RandomInitalise(),
        neighbourhood: new TwoOpt(),
        step: new LowestCost(),
        terminate: ts,
        name: "Local Search - Random initialisations"
        );




    }
}
