using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public abstract class ProportionalSelection<S> : ISelectionStrategy<S>
    {
        protected Random random = new();

        public abstract IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S> fitnessFunction);

        protected static float FitnessSum(IList<S> population, ICostFunction<S> fitnessFunction)
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
