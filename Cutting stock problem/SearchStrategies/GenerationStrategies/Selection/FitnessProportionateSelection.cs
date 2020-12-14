using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class FitnessProportionateSelection<S> : ProportionalSelection<S>
    {
        public override IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            float fitnessSum = FitnessSum(population, fitnessFunction);

            float proababilty(S s) => fitnessFunction.Cost(s) / fitnessSum;


            IList<(S, int)> selection = new List<(S, int)>();

            for(int i = 0; i< population.Count; i++)
            {
                S solution = population[i];
                if (proababilty(solution) > random.NextDouble())
                {
                    selection.Add((solution, i));
                }
            }


            return selection;
        }

    }
}
