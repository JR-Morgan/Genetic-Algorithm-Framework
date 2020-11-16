using CSP.Search.TerminalConditions;
using System.Collections.Generic;

namespace CSP.Search
{
    public static class SearchFactory
    {
        private const float DEFAULT_TIMEOUT = 2000f;
        private const int DEFAULT_ITTERATIONS = 100;


        internal static List<ISearchStrategy> GenerateSearches(TerminateStrategy ts)
        {
            return new List<ISearchStrategy>()
            {
                //new ExhaustiveSearch(),
            };
        }

        public static List<ISearchStrategy> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));
    }
}
