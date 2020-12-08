using SearchStrategies.Operations;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class SelectAll<S> : ISelectionStrategy<S>
    {
        public S[] Select(S[] population, ICostFunction<S>? fitnessFunction = default) => population;
    }
}
