using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies.Replacement
{
    /// <summary>
    /// Replaces the population with children sequentially
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public class ReplaceOldest<S> : IReplacementStrategy<S>
    {
        private int generationCounter = 0;
        public void Replace(ref IList<S> population, IList<(S, int)> children)
        {
            for (int i = 0; i < children.Count; i++)
            {
                population[generationCounter] = children[i].Item1;
                generationCounter = (generationCounter + 1) % population.Count;
            }
        }
    }
}