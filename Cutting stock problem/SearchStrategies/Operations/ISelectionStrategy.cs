namespace SearchStrategies.Operations
{
    public interface ISelectionStrategy<S>
    {
        S[] Select(S[] population, int selectionSize, IStepFunction<S> stepFunction);
    }
}
