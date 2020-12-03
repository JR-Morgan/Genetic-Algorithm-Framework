﻿using CSP.Search.Crossover;
using CSP.Search.Initialisation;
using CSP.Search.Neighbourhood;
using CSP.Search.Selection;
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
        step: new LowestCost(),
        terminate: ts,
        name: "Local Search - Random initialisations"
        );

        private static ISearchStrategy<ISolution, Problem> EA1(TerminateStrategy ts, uint populationSize, uint k, float elitism = 0.2f, float mutationRate = 0.04f) => new EvolutionalSearch<ISolution, Problem>(
            initalise: new RandomInitalise(),
            selectionStrategy: new TournamentSelection(k),
            crossoverStratergy: new ActivityOrderedCrossover(),
            swap: new StockRandomise(),
            stepFunction: new LowestCost(),
            terminate: ts,
            populationSize: populationSize,
            eliteism: elitism,
            mutationRate: mutationRate,
            name: "Evolutionary Search - Tournament"
            );

    }
}
