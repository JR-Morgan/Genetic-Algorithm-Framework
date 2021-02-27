using SearchStrategies.Operations;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class SelectAll<S> : ISelectionStrategy<S>
    {
        public IList<(S,int)> Select(IList<S> population, ICostFunction<S>? fitnessFunction = default)
        {
            List<(S, int)> selection = new();
            for(int i =0; i < population.Count; i++)
            {
                selection.Add((population[i], i));
            }
            return selection;
        }
    }
}
