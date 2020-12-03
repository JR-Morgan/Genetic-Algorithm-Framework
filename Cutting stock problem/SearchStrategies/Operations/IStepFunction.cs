using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface IStepFunction<S>
    {
        S Fittest(IEnumerable<S> solutions);

        S FittestP(params S[] solutions) => Fittest(solutions);

        float Fitness(S solution);
    }
}
