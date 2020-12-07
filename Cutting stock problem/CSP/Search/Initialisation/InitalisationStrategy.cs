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
            List<Activity> activitiesToRemove = new();

            foreach (Activity a in solution.Activities)
            {
                for (int i = 0; i< a.Orders.Count; i++)
                {
                    float length = a.Orders[i];
                    if (ordersDict[length] <= 0)
                    {
                        if(a.Orders.Count == 1)
                        {
                            activitiesToRemove.Add(a);
                        } else
                        {
                            a.Remove(length);
                            i--;
                        }
                    }
                    else
                    {
                        ordersDict[length]--;
                    }
                }
            }

            foreach(Activity b in activitiesToRemove) solution.Activities.Remove(b);



            Repair(solution, ordersDict);

            if (!solution.IsComplete())
            {
                throw new Exception("Solution still incomplete after repair");
            }
                

        }

        public abstract ISolution Initalise(Problem problem);
    }
}
