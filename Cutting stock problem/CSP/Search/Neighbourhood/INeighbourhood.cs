using System.Collections.Generic;

namespace CSP.Search.Neighbourhood
{
    interface INeighbourhood
    {
        List<ISolution> GenerateNeighbourhood(ISolution parent);
    }
}
