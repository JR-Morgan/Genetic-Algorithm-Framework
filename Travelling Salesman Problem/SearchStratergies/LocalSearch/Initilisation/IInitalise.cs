using System;
using System.Collections.Generic;

namespace TSP.SearchStratergies.LocalSearch.Initilisation
{
    public interface IInitalise
    {
        public Route Initalise(List<Node> nodes) => throw new Exception();
    }
}
