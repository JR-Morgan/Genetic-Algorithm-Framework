using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Crossover
{
    class ActivityOrderedCrossover : ICrossover<ISolution>
    {
        private static Random random = new Random(); //TODO


        //TODO This implementation is returning invalid solutions.
        public ISolution CrossOver(ISolution parent1, ISolution parent2)
        {
            parent1 = parent1.Copy();
            parent2 = parent2.Copy();

            int geneA = random.Next(parent1.Activities.Count);
            int geneB = random.Next(parent1.Activities.Count);

            int startGene = Math.Min(geneA, geneB);
            int endGene = Math.Max(geneA, geneB);

            return CrossOver(parent1, parent2, startGene , endGene);
        }

        private ISolution CrossOver(ISolution parent1, ISolution parent2, int start, int end)
        {
            List<Activity> ChildP1 = new List<Activity>();

            for (int i = start; i < end; i++)
            {
                ChildP1.Add(parent1.Activities[i]);
            }

            List<Activity> ChildP2 = new List<Activity>();
            foreach (Activity node in parent2.Activities)
            {
                if (!ChildP1.Contains(node)) ChildP2.Add(node);
            }


            ChildP1.AddRange(ChildP2);

            return new Solution(parent1.Problem, ChildP1);
        }
    }
}
