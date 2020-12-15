
using SearchStrategies.Operations;
using System;

namespace CSP.ParameterOptimiser
{
    public class OInitialisation : IInitialise<EAParams, AlgorithmProblem>
    {


        public readonly Random random;

        public OInitialisation(Random random)
        {
            this.random = random;
        }

        public EAParams Initialise(AlgorithmProblem problem)
        {

            return new EAParams()
            {
                n = 3,
                populationSize = (uint)random.Next(5, problem.MAX_POPULATION_SIZE),
                comparisionProportion = (float)random.NextDouble() * problem.MAX_K_PROPORTION,
                eliteProportion = (float)random.NextDouble() * problem.MAX_ELITE_PROPORTION,
                mutationRate = (float)random.NextDouble() * problem.MAX_MUTATION_RATE, //0.09f,
                mutationRate2 = (float)random.NextDouble() * problem.MAX_MUTATION_RATE_EP,
                mutationScale = random.Next(1, problem.MAX_MUTATION_SCALE),
                selectionProportion = (float)random.NextDouble() * problem.MAX_SELECTION_PROPORTION,
            };
        }
    }
}