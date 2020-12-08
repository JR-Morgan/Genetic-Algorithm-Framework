using SearchStrategies.Operations;
using System;
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

        public S[] Select(S[] population, ICostFunction<S>? fitnessFunction = default)
        {
            S[] selection = population.Where(function).ToArray();

            return selection;
        }
    }
}
