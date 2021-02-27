using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.SearchStratergies.LocalSearch.Crossover
{
    interface ICrossover
    {

        Route CrossOver(Route parent1, Route parent2);
    }
}
