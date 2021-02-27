using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ISelectionStrategy<S>
    {
        /// <summary>
        /// Selects a subset of the <paramref name="population"/>
        /// </summary>
        /// <param name="population">The population to be selected from</param>
        /// <param name="fitnessFunction">The fitnessFunction the solutions should be evaluated against</param>
        /// <returns>An array of tuples where item 1 is the solution, and item2 is item1's index in <paramref name="population"/></returns>
        IList<(S solution, int index)> Select(IList<S> population, ICostFunction<S> fitnessFunction);
    }
}
