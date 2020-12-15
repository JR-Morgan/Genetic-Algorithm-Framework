using SearchExtensions;
using SearchStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Island
{
    class IslandCrossover
    {
        private readonly Random random;

        public IslandCrossover(Random random)
        {
            this.random = random;
        }

        public void Crossover<P>(IList<EvolutionarySearch<ISolution,P>> islands)
        {
            islands.Shuffle(random);
            IList<ISolution>[] newPopulations = new IList<ISolution>[islands.Count];

            for(int i = 0; i < (islands.Count / 2) * 2; i+= 2)
            {
                (List<ISolution> c1, List<ISolution>c2) = Crossover(islands[i].Population, islands[i+1].Population);
                newPopulations[i] = c1;
                newPopulations[i + 1] = c2;
            }

            for(int i = 0; i < islands.Count; i++)
            {
                islands[i].Population = newPopulations[i];
            }
        }


        private (List<ISolution> c1, List<ISolution>c2) Crossover(IList<ISolution> parent1, IList<ISolution> parent2)
        {
            List<ISolution> pool = new(parent1.Count + parent2.Count);
            pool.AddRange(parent1);
            pool.AddRange(parent2);

            pool.Shuffle(random);

            List<ISolution> c1 = new(parent1.Count);
            for(int i = 0; i < parent1.Count; i++)
            {
                c1.Add(pool[i]);
            }

            List<ISolution> c2 = new(parent1.Count);
            int sum = parent1.Count + parent2.Count;
            for (int i = parent1.Count; i < sum; i++)
            {
                c2.Add(pool[i]);
            }

            return (c1, c2);
        }


    }
}
