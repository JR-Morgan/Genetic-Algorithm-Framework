
using SearchStrategies.Operations;
using System;

namespace CSP.ParameterOptimiser
{
    public class OInitialisation : IInitialise<EAParams, AlgorithmProblem>
    {


        public Random random = new();

        public EAParams Initialise(AlgorithmProblem problem)
        {

            return new EAParams()
            {
                n = 2,
                populationSize = 140,//(uint)random.Next(5, problem.MAX_POPULATION_SIZE),
                comparisionProportion = 0.05f,//(float)random.NextDouble() * problem.MAX_K_PROPORTION,
                eliteProportion = 0.04f,//(float)random.NextDouble() * problem.MAX_ELITE_PROPORTION,
                mutationRate = (float)random.NextDouble() * problem.MAX_MUTATION_RATE, //0.09f,
                mutationRate2 = (float)random.NextDouble() * problem.MAX_MUTATION_RATE_EP,
                mutationScale = 2,//random.Next(1, problem.MAX_MUTATION_SCALE),
                selectionProportion = 0.3f// (float)random.NextDouble() * problem.MAX_SELECTION_PROPORTION,
            };
        }
    }
}