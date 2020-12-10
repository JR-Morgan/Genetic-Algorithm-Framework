using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    /// <summary>
    /// Does not generate a neighbourhood. It simply returns the parent
    /// </summary>
    public class NonNeighbourhood<S> : INeighbourhood<S>
    {
        public List<S> GenerateNeighbourhood(S parent) => new List<S> { parent };
    }
}