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

            List<ISolution> pool = new(parent1.Count / 2 + parent2.Count / 2);

            //c1 = half of parent 1, half random
            List<ISolution> c1 = new(parent1.Count);
            //Add the first half
            for (int i = 0; i < parent1.Count / 2; i++)
            {
                c1.Add(parent1[i]);
            }

            for (int i = parent1.Count / 2; i < parent1.Count; i++)
            {
                pool.Add(parent1[i]);
            }

            //c2 = half of parent 2, half random
            List<ISolution> c2 = new(parent1.Count);
            //Add the first half
            for (int i = 0; i < parent2.Count / 2; i++)
            {
                c2.Add(parent2[i]);
            }

            for (int i = parent2.Count / 2; i < parent2.Count; i++)
            {
                pool.Add(parent2[i]);
            }


            //Add the random
            pool.Shuffle(random);

            for (int i = 0; i < parent1.Count / 2; i++)
            {
                c1.Add(pool[i]);
            }

            for (int i = pool.Count - (parent2.Count / 2); i < pool.Count; i++)
            {
                c2.Add(pool[i]);
            }



            return (c1, c2);
        }


    }
}
