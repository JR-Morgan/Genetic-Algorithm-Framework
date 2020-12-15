using CSP.Island;
using CSP.ParameterOptimiser;
using CSP.Search.Crossover;
using CSP.Search.Initialisation;
using CSP.Search.Mutation;
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

        private static readonly EAParams pa = new EAParams()
        {
            n = 2,
            populationSize = 50,
            selectionProportion = 0.2f,
            comparisionProportion = 0.08f,
            eliteProportion = 0.04f,
            mutationRate = 0.09f,
            mutationRate2 = 0.15f,
            mutationScale = 5,
        };

        private static readonly EAParams a = new EAParams()
        {
            n = 3,
            populationSize = 50,
            selectionProportion = 0.2f,
            comparisionProportion = 0.08f,
            eliteProportion = 0.04f,
            mutationRate = 0.12f,
            mutationRate2 = 0.02f,
            mutationScale = 2,
        };

        private static readonly EAParams b = new EAParams()
        {
            n = 3,
            populationSize = 75,
            selectionProportion = 0.2f,
            comparisionProportion = 0.08f,
            eliteProportion = 0.04f,
            mutationRate = 0.12f,
            mutationRate2 = 0.02f,
            mutationScale = 2,
        };

        private static Random Random(int? seed = null) => seed != null ? new Random((int)seed) : new Random();

        internal static List<ISearchStrategy<ISolution, Problem>> GenerateSearches(TerminateStrategy ts)
        {
            return new()
            {
                RND(ts),
                LS1(ts),
                EAO(ts, a, 255),
                EAA(ts, b, 255),
                EAB(ts, b, 255),
            };
        }

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesTimeOut(float TimeOut = DEFAULT_TIMEOUT) => GenerateSearches(TerminalStrategies.TimeOut(TimeOut));

        public static List<ISearchStrategy<ISolution, Problem>> GenerateSearchesItterations(int numberOfItterations = DEFAULT_ITTERATIONS) => GenerateSearches(TerminalStrategies.FixedItterations(numberOfItterations));



        public static ISearchStrategy<EAParams, AlgorithmProblem> EAOptimiser(Problem problem, int? seed = null) => new RandomSearch<EAParams, AlgorithmProblem>(
            initalise: new OInitialisation(Random(seed)),
            fitnessFunction: new AlgorithmCost(7000f, problem),
            terminate: TerminalStrategies.FixedItterations(int.MaxValue),
            name: "EA optimiser"
        );

        internal static ISearchStrategy<ISolution, Problem> RND(TerminateStrategy ts, int? seed = null) => new LocalSearch<ISolution, Problem>(
            initalise: new RandomInitalise(Random(seed)),
            neighbourhood: new NonNeighbourhood<ISolution>(),
            fitnessFunction: new LowestCostFunction(),
            terminate: ts,
            name: "Random Search"
        );

        internal static ISearchStrategy<ISolution, Problem> LS1(TerminateStrategy ts, int? seed = null)
        {
            Random random = Random(seed);
            return new LocalSearch<ISolution, Problem>(
                initalise: new RandomInitalise(random),
                neighbourhood: new StockRandomise(random, true),
                fitnessFunction: new LowestCostFunction(),
                terminate: ts,
                name: "Local Search - Random initialisations"
            );
        }


        internal static ISearchStrategy<ISolution, Problem> EAO(TerminateStrategy ts, EAParams p, int? seed = null)
        {
            Random random = Random(seed);

            return new EvolutionarySearch<ISolution, Problem>(
                initalise: new RandomInitalise(random),
                generationStrategy: new Generation<ISolution>(new ReplaceParents<ISolution>(),
                    new ActivityNPointCrossover(p.n,
                        //selectionStrategy: new FitnessProportionateSelection<ISolution>(),
                        //selectionStrategy: new StochasticUniversalSampling<ISolution>(p.selectionSize),
                        selectionStrategy: new TournamentSelection<ISolution>(p.K, p.selectionSize, random),
                        repairStrategy: new RandomInitalise(random),
                        random: random,
                        elitismProportion: p.eliteProportion,
                            //Next:
                            new OrderCrossover(new ProbabilisticSelection<ISolution>(p.mutationRate, new Random()), random, p.mutationScale),
                            new StockRandomise(random, true, p.mutationRate2)
                        )
                    //new OrderCrossover(new ProbabilisticSelection<ISolution>(p.mutationRate2, new Random()))
                    ),
                fitnessFunction: new LowestCostFunction(),
                terminate: ts,
                populationSize: p.populationSize,
                random: random,
                name: "Evolutionary Search - O"
            );;;
        }




        internal static ISearchStrategy<ISolution, Problem> EAA(TerminateStrategy ts, EAParams p, int seed)
        {
            Random random = Random(seed);

            return new IslandSearch(
                p: p,
                terminate: ts,
                fitnessFunction: new LowestCostFunction(),
                populationSize: p.populationSize,
                numberOfIslands: 6,
                seed: seed);
        }

        internal static ISearchStrategy<ISolution, Problem> EAB(TerminateStrategy ts, EAParams p, int? seed = null)
        {
            Random random = Random(seed);

            return new EvolutionarySearch<ISolution, Problem>(
                initalise: new RandomInitalise(random),
                generationStrategy: new Generation<ISolution>(new ReplaceParents<ISolution>(),
                    new CustomCrossover(//p.n,
                        selectionStrategy: new TournamentSelection<ISolution>(p.K, p.selectionSize, random),
                        repairStrategy: new RandomInitalise(random),
                        random: random,
                        elitismProportion: p.eliteProportion,
                        next: new OrderCrossover(new ProbabilisticSelection<ISolution>(p.mutationRate, new Random()), random, p.mutationScale)
                        )
                    ),
                fitnessFunction: new LowestCostFunction(),
                terminate: ts,
                populationSize: b.populationSize,
                random: random,
                name: "Evolutionary Search - B"
            );
        }
    }
}
