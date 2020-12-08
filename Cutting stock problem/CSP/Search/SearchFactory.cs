using CSP.Search.Crossover;
using CSP.Search.Initialisation;
using CSP.Search.Neighbourhood;
using CSP.Search.Selection;
using CSP.Search.StepFunctions;
using SearchStrategies;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.GenerationStrategies.Replacement;
using SearchStrategies.GenerationStrategies.Selection;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Collections.Generic;

namespace CSP.Search
{
    public static class SearchFactory
    {
        private const float DEFAULT_TIMEOUT = 5000f;
        private const int DEFAULT_ITTERATIONS = 1000;


        internal static List<ISearchStrategy<ISolution, Problem>> GenerateSearches(TerminateStrategy ts)
        {
            return new()
            {
                LS1(ts),
                EA1(ts, 100, 20),
            };
        }

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));


        private static ISearchStrategy<ISolution, Problem> LS1(TerminateStrategy ts) => new LocalSearch<ISolution, Problem>(
        initalise: new RandomInitalise(),
        neighbourhood: new StockRandomise(true),
        fitnessFunction: new LowestCost(),
        terminate: ts,
        name: "Local Search - Random initialisations"
        );

        private static ISearchStrategy<ISolution, Problem> EA1(TerminateStrategy ts, uint populationSize, uint k) => new EvolutionarySearch<ISolution, Problem>(
            initalise: new RandomInitalise(),
            generationStrategy: new Generation<ISolution>( new ReplaceParents<ISolution>(),
                new OrderedActivityCrossover(
                    selectionStrategy: new TournamentSelection<ISolution>(k, populationSize / 5),
                    repairStrategy: new RandomInitalise(),
                    elitismProportion: 0.1f,
                    //next: new StockRandomise(false,  0.01f)
                    next: new OrderedOrderCrossover(new ProbabilisticSelection<ISolution>(0.01f, new Random()))
                    )
                ),
            fitnessFunction: new LowestCost(),
            terminate: ts,
            populationSize: populationSize,
            name: "Evolutionary Search - Tournament"
            );

    }
}
