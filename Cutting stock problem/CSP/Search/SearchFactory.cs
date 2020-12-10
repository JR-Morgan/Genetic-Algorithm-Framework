using CSP.ParameterOptimiser;
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
        private const float DEFAULT_TIMEOUT = 10000000f;
        private const int DEFAULT_ITTERATIONS = 1000;


        internal static List<ISearchStrategy<ISolution, Problem>> GenerateSearches(TerminateStrategy ts)
        {
            return new()
            {
                RND(ts),
                LS1(ts),
                EA2(ts),
            };
        }

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));



        public static ISearchStrategy<Parameters, AlgorithmProblem> EAOptimiser(Problem problem) => new RandomSearch<Parameters, AlgorithmProblem>(
            initalise: new RandomParameterInitialisation(),
            fitnessFunction: new AlgorithmCost(3000f, problem),
            terminate: TerminalStrategies.FixedItterations(1000000),
            name: "Random Search"
        );

        internal static ISearchStrategy<ISolution, Problem> RND(TerminateStrategy ts) => new LocalSearch<ISolution, Problem>(
            initalise: new RandomInitalise(),
            neighbourhood: new NonNeighbourhood<ISolution>(),
            fitnessFunction: new LowestCost(),
            terminate: ts,
            name: "Random Search"
        );

        internal static ISearchStrategy<ISolution, Problem> LS1(TerminateStrategy ts) => new LocalSearch<ISolution, Problem>(
        initalise: new RandomInitalise(),
        neighbourhood: new StockRandomise(true),
        fitnessFunction: new LowestCost(),
        terminate: ts,
        name: "Local Search - Random initialisations"
        );


        internal static ISearchStrategy<ISolution, Problem> EA1(TerminateStrategy ts, Parameters p) => new EvolutionarySearch<ISolution, Problem>(
            initalise: new RandomInitalise(),
            generationStrategy: new Generation<ISolution>(new ReplaceParents<ISolution>(),
                new OrderedActivityCrossover(
                    selectionStrategy: new TournamentSelection<ISolution>((uint)Math.Max(2, p.populationSize * p.comparisionProportion), (uint)Math.Max(2, p.populationSize * p.selectionProportion)),
                    repairStrategy: new RandomInitalise(),
                    elitismProportion: p.eliteProportion,
                    //next: new StockRandomise(false,  0.01f)
                    next: new SingleOrderCrossover(new ProbabilisticSelection<ISolution>(p.mutationRate, new Random()))
                    )
                ),
            fitnessFunction: new LowestCost(),
            terminate: ts,
            populationSize: p.populationSize,
            name: "Evolutionary Search - Tournament"
            );

        internal static ISearchStrategy<ISolution, Problem> EA2(TerminateStrategy ts, uint populationSize = 50) => new EvolutionarySearch<ISolution, Problem>(
            initalise: new RandomInitalise(),
            generationStrategy: new Generation<ISolution>(new ReplaceParents<ISolution>(),
                new OrderedActivityCrossover(
                    selectionStrategy: new TournamentSelection<ISolution>((uint)Math.Max(2, populationSize * 0.08f), (uint)Math.Max(2, populationSize * 0.2f)),
                    repairStrategy: new RandomInitalise(),
                    elitismProportion: 0.04f,
                    //next: new StockRandomise(false,  0.01f)
                    next: new SingleOrderCrossover(new ProbabilisticSelection<ISolution>(0.09f, new Random()))
                    )
                ),
            fitnessFunction: new LowestCost(),
            terminate: ts,
            populationSize: populationSize,
            name: "Evolutionary Search - Tournament"
            );

    }
}
