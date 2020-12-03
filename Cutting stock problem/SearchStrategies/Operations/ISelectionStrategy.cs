using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ISelectionStrategy<S>
    {
        S[] Select(IEnumerable<S> population, int selectionSize, IStepFunction<S> stepFunction);
    }
}
