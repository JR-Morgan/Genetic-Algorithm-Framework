
using SearchStrategies.Operations;
using System;

namespace CSP.ParameterOptimiser
{
    public class RandomParameterInitialisation : IInitialise<Parameters, AlgorithmProblem>
    {


        public static Random random = new();

        public Parameters Initialise(AlgorithmProblem problem)
        {

            return new Parameters()
            {
                populationSize = (uint)random.Next(5, problem.MAX_POPULATION_SIZE),
                comparisionProportion = (float)random.NextDouble() * problem.MAX_K_PROPORTION,
                eliteProportion = (float)random.NextDouble() * problem.MAX_ELITE_PROPORTION,
                mutationRate = (float)random.NextDouble() * problem.MAX_MUTATION_RATE,
                selectionProportion = (float)random.NextDouble() * problem.MAX_SELECTION_PROPORTION,
            };
        }
    }
}