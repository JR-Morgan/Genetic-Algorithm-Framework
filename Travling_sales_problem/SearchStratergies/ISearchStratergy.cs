using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.Solution_Stratergies
{
    public interface ISearchStratergy
    {
        public delegate void ItterationCompleteEventHandler(ISearchStratergy sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        public void Compute(Graph graph);
    }
}
