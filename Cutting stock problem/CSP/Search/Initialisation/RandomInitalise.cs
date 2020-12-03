using SearchExtensions;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Initialisation
{
    class RandomInitalise : IInitialise<ISolution, Problem>
    {
        private const int TIMEOUT = 50;

        private static Random random = new Random(); //TODO determinism

        public ISolution Initalise(Problem problem)
        {
            Activity nextActivity() => new Activity(problem.Stock[random.Next(problem.Stock.Length)]);

            List<float> orders = new(problem.Orders);
            orders.Shuffle(random);
            List<Activity> activities = new() { nextActivity() };
            int index = 0;

            foreach(float order in orders)
            {
                int counter = 0;
                while(!activities[index].Add(order))
                {
                    if (activities[index].IsEmpty)
                    {
                        activities[index] = nextActivity();
                    }
                    else
                    {
                        activities.Add(nextActivity());
                        index++;
                    }
                    
                    if (++counter >= TIMEOUT) throw new Exception($"Could not initalise a valid solution after {counter} tries");
                }
            }

            return new Solution(problem, activities);


        }
    }
}
