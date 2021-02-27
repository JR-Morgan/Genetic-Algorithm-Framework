using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategies.GenerationStrategies.Replacement
{
    public interface IReplacementStrategy<S>
    {
        void Replace(ref IList<S> population, IList<(S, int)> children);
    }
}
