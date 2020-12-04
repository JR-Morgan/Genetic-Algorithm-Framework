using SearchExtensions;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Initialisation
{
    class RandomInitalise : InitalisationStrategy
    {
        private const int TIMEOUT = 50;

        private static Random random = new Random(); //TODO determinism

        private List<Activity> Activities(Stock[] stock, List<float> orders)
        {
            Activity nextActivity() => new Activity(stock[random.Next(stock.Length)]);


            List<Activity> activities = new() { nextActivity() };
            int index = 0;

            foreach (float order in orders)
            {
                int counter = 0;
                while (!activities[index].Add(order))
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
            return activities;
        } 

        public override ISolution Initalise(Problem problem)
        {
            

            List<float> orders = new(problem.FlatOrders);
            orders.Shuffle(random);
            
            return new Solution(problem, Activities(problem.Stock, orders));


        }

        protected override void Repair(ISolution solution, Dictionary<float, int> missingDict)
        {
            List<float> missingOrders = Problem.FlattenDict(missingDict);
            solution.Activities.AddRange(Activities(solution.Problem.Stock, missingOrders));


        }
    }
}
