
using CSP.Search;
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
            EAParams p = SearchFactory.a;
            //p.n = 3,
            p.populationSize = (uint)random.Next(5, problem.MAX_POPULATION_SIZE);
            //p.comparisionProportion = (float)random.NextDouble() * problem.MAX_K_PROPORTION;
            //p.eliteProportion = (float)random.NextDouble() * problem.MAX_ELITE_PROPORTION;
            //p.mutationRate = (float)random.NextDouble() * problem.MAX_MUTATION_RATE, //0.09f;
            //p.mutationRate2 = (float)random.NextDouble() * problem.MAX_MUTATION_RATE_EP;
            //p.mutationScale = random.Next(1, problem.MAX_MUTATION_SCALE);
            //p.selectionProportion = (float)random.NextDouble() * problem.MAX_SELECTION_PROPORTION;

            return p;
        }
    }
}