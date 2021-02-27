using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Initialisation
{
    abstract class InitalisationStrategy : IInitialise<ISolution, Problem>
    {

        protected abstract void Repair(ISolution solution, Dictionary<float, int> missingDict);
        public void Repair(ISolution solution)
        {
            Dictionary<float, int> ordersDict = new(solution.Problem.Orders);

            for (int i = 0; i < solution.Activities.Count; i++)
            {
                Activity a = solution.Activities[i];
                for (int j = 0; j< a.Orders.Count; j++)
                {
                    float length = a.Orders[j];
                    if (ordersDict[length] <= 0)
                    {
                        if(a.Orders.Count == 1)
                        {
                            //TODO you would have thought that the two below statements are the same, but they are not.
                            //solution.Activities.RemoveAt(i);
                            if (solution.Activities.Remove(a))
                                i--;
                            
                            
                        } else
                        {
                            a.Remove(length);
                            j--;
                        }
                    }
                    else
                    {
                        ordersDict[length]--;
                    }
                }
            }



            Repair(solution, ordersDict);

            if (!solution.IsComplete())
            {
                throw new Exception("Solution still incomplete after repair");
            }
                

        }

        public abstract ISolution Initialise(Problem problem);
    }
}
