using SearchStrategies.Operations;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies
{
    public interface IGenerationOperation<S>
    {
        /// <summary>
        /// Performs this operation, and any next operations.
        /// </summary>
        /// <param name="population">The population</param>
        /// <param name="fitnessFunction">The fitness function to be used by the operation(s)</param>
        /// <returns>The children of this generation</returns>
        IList<(S solution, int index)> Operate(IList<S> population, ICostFunction<S> fitnessFunction);
    }
}
