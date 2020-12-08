using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategies.GenerationStrategies.Replacement
{
    public class ReplaceParents<S> : IReplacementStrategy<S>
    {
        public void Replace(ref IList<S> population, IList<(S, int)> children)
        {
            foreach((S s, int i) in children)
            {
                population[i] = s;
            }
        }
    }
}
