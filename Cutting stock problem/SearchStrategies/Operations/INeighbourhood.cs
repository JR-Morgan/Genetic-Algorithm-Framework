using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface INeighbourhood<S>
    {
        List<S> GenerateNeighbourhood(S parent);
    }
}
