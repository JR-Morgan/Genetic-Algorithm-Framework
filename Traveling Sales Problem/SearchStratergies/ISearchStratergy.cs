using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.Solution_Stratergies
{
    interface ISearchStratergy
    {
        public Log Compute(Graph graph);
    }
}
