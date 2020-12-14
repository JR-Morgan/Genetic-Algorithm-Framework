using SearchStrategies.Operations;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class StochasticUniversalSampling<S> : FitnessProportionateSelection<S>
    {
        private uint selectionSize;
        public StochasticUniversalSampling(uint selectionSize)
        {
            this.selectionSize = selectionSize;
        }
        public override IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            float fitnessSum = FitnessSum(population, fitnessFunction);

            float distance = fitnessSum / selectionSize;

            List<(S, int)> selection = new();

            float start = (float)(random.NextDouble() * distance);
            float workingSum = 0f;

            float p = start;
            for (int i = 0; i < selectionSize ; i++)
            {
                int j = 0;
                while (workingSum < p)
                {
                    i++;
                    workingSum += fitnessFunction.Cost(population[j]);
                }
                selection.Add((population[j], j));

                p = start + i * distance;
            }

            return selection;
        }

    }
}
