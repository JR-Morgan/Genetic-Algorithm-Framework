using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ICostFunction<S>
    {
        /// <summary>
        /// Returns a copy of the fittest valid solution
        /// </summary>
        /// <param name="solutions"></param>
        /// <returns></returns>
        S Fittest(IEnumerable<S> solutions);

        S Fittest(params S[] solutions) => Fittest((IEnumerable<S>)solutions);

        /// <summary>
        /// Returns the solution with the lowest cost. May return an invalid solution
        /// </summary>
        /// <param name="solutions"></param>
        /// <returns></returns>
        S LowestCost(IEnumerable<S> solutions);

        S LowestCost(params S[] solutions) => LowestCost((IEnumerable<S>)solutions);

        float Cost(S solution);
    }
}
