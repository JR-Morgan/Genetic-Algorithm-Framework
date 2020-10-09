using Travling_sales_problem.SearchStratergies.LocalSearch;
using Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation;
using Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.TerminalConditions;

namespace Travling_sales_problem.Solution_Stratergies
{

    class MyLocalSearch : LocalSearch
    {

        public MyLocalSearch()
            :base(RandomInitalise.Initalise, TwoOptNeighbourhood.GenerateNeighbourhood, BestNeighbourhoodStep.Step, TimeOut.SetTimeOut(1000f))
        {
            
        }
    }
}
