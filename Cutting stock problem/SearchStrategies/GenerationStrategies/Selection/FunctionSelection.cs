using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchStrategies.GenerationStrategies.Selection
{

    public class FunctionSelection<S> : ISelectionStrategy<S>
    {
        protected readonly Func<S, bool> function;

        public FunctionSelection(Func<S, bool> function)
        {
            this.function = function;
        }

        public IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S>? fitnessFunction = default)
        {
            List<(S, int)> selection = new();

            for(int i = 0; i < population.Count; i++)
            {
                S s = population[i];
                if (function.Invoke(s))
                {
                    selection.Add((s, i));
                }
            }

            return selection.ToArray();
        }
    }
}
