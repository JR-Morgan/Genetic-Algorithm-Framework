using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class FitnessProportionateSelection<S> : ISelectionStrategy<S>
    {
        private static Random random = new();
        public IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            float fitnessSum = FitnessSum(population, fitnessFunction);

            float proababilty(S s) => fitnessFunction.Cost(s) / fitnessSum;


            IList<(S, int)> selection = new List<(S, int)>();

            for(int i = 0; i< population.Count; i++)
            {
                S solution = population[i];
                if (proababilty(solution) < random.NextDouble())
                {
                    selection.Add((solution, i));
                }
            }


            return selection;
        }

        private static float FitnessSum(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            float fitnessSum = 0;
            foreach (S s in population)
            {
                fitnessSum += fitnessFunction.Cost(s);
            }
            return fitnessSum;
        }
    }
}
