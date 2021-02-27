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
using System.Diagnostics;
using System.Threading.Tasks;

namespace CSP.Island
{
    class IslandSearch : ISearchStrategy<ISolution, Problem>
    {

        private IList<EvolutionarySearch<ISolution, Problem>> islands;
        private TerminateStrategy externalTerminate;
        private IslandCrossover islandCrossover;
        private uint populationSize;
        private ICostFunction<ISolution> fitnessFunction;

        public ISolution? BestSolution { get; private set; }

        public event ISearchStrategy<ISolution, Problem>.ItterationCompleteEventHandler? OnItterationComplete;


        public IslandSearch(EAParams p, TerminateStrategy terminate, float internalTimeOut, ICostFunction<ISolution> fitnessFunction, uint numberOfIslands, int seed, string name = "Island")
        {
            islandCrossover = new IslandCrossover(new Random(seed));
            this.externalTerminate = terminate;
            this.populationSize = p.populationSize;
            this.fitnessFunction = fitnessFunction;

            islands = new List<EvolutionarySearch<ISolution, Problem>>((int)numberOfIslands);
            for(int i = 0; i < numberOfIslands; i++)
            {
                Random random = new Random(seed + 1 + i);
                islands.Add(new EvolutionarySearch<ISolution, Problem>(
                    initalise: new RandomInitalise(random),
                    generationStrategy: new Generation<ISolution>(new ReplaceParents<ISolution>(),
                        new ActivityNPointCrossover(p.n,
                            selectionStrategy: new TournamentSelection<ISolution>(p.K, p.selectionSize, random),
                            repairStrategy: new RandomInitalise(random),
                            random: random,
                            elitismProportion: p.eliteProportion,
                            next: new OrderCrossover(new ProbabilisticSelection<ISolution>(p.mutationRate, new Random()), random, p.mutationScale)
                            )
                        ),
                fitnessFunction: new LowestCostFunction(),
                terminate: TerminalStrategies.TimeOut(internalTimeOut),
                populationSize: p.populationSize,
                random: random,
                name: "Evolutionary Search - A"
                ));
            }

        }

        private async static Task<Log[]> RunIslandsAsync<S, P>(IList<EvolutionarySearch<S, P>> searches, P problem)
        {
            List<Task<Log>> tasks = new(searches.Count);

            foreach (var search in searches)
            {
                tasks.Add(Task.Run(() => search.Compute(problem, false)));
            }
            return await Task.WhenAll(tasks);
        }


        public Log Compute(Problem problem)
        { 

            for(int i = 0; i < islands.Count; i++)
            {
                islands[i].InitialisePopulation((int)populationSize, problem);
            }

            TerminateCondition terminate = externalTerminate();

            int solutionsEvaluated = 0, itterationCounter = 0;
            Stopwatch timer = new();

            Log GenerateLog() => new Log()
            {
                timeToCompute = timer.ElapsedMilliseconds,
                numberOfSolutionsEvaluated = solutionsEvaluated,
                iteration = itterationCounter,
                bestSolutionFitness = BestSolution != null ? fitnessFunction.Cost(BestSolution) : float.PositiveInfinity,
                bestSolution = BestSolution != null ? BestSolution.ToString() : string.Empty,
            };

            timer.Start();
            while (!terminate())
            {
                Task.WaitAll(RunIslandsAsync(islands, problem));


                List<ISolution> solutions = new();
                foreach(var s in islands)
                {
                    if(s.BestSolution != null)
                        solutions.Add(s.BestSolution);
                }

                BestSolution = fitnessFunction.Fittest(solutions);

                islandCrossover.Crossover(islands);

                OnItterationComplete?.Invoke(this, GenerateLog());
            }          



            return GenerateLog();

        }


    }
}
