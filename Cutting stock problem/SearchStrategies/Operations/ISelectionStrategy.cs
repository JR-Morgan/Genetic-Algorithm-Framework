using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ISelectionStrategy<S>
    {
        S[] Select(S[] population, ICostFunction<S> fitnessFunction);
    }
}
