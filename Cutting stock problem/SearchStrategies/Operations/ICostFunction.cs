using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ICostFunction<S>
    {
        S Fittest(IEnumerable<S> solutions);

        S FittestP(params S[] solutions) => Fittest(solutions);

        float Cost(S solution);
    }
}
