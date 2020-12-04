using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.Search.Initialisation
{
    abstract class InitalisationStrategy : IInitialise<ISolution, Problem>
    {

        protected abstract void Repair(ISolution solution, Dictionary<float, int> missingDict);
        public void Repair(ISolution solution)
        {
            Dictionary<float, int> ordersDict = new(solution.Problem.Orders);

            foreach (Activity a in solution.Activities)
            {
                for(int i = 0; i< a.Orders.Count; i++)
                {
                    float length = a.Orders[i];
                    if (ordersDict[length] <= 0)
                    {
                        a.Remove(length);
                    }
                    else
                    {
                        ordersDict[length]--;
                    }
                }
            }

            Repair(solution, ordersDict);
        }

        public abstract ISolution Initalise(Problem problem);
    }
}
