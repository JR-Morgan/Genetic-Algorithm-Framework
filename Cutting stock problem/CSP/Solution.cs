using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSP
{
    class Solution : ISolution
    {
        public Problem Problem { get; }
        List<Activity> ISolution.Activities => Activities;
        internal List<Activity> Activities { get; private set; }

        public Solution(Problem problem, List<Activity> activities)
        {
            this.Problem = problem;
            Activities = activities;
        }

        public Solution(Problem problem) : this(problem, new List<Activity>()) { }

        public ISolution Copy()
        {
            List<Activity> activities = new List<Activity>(Activities);
            activities.AddRange(activities);

            foreach(Activity a in Activities)
            {
                activities.Add(a.Copy());
            }

            return new Solution(Problem, activities);
        }


        public float Fitness()
        {
            float fitness = 0;
            foreach(Activity a in Activities)
            {
                fitness += a.Cost;
            }
            return fitness;
        }

        public bool IsValid()
        {
            foreach(Activity activity in Activities)
            {
                if (!activity.IsValid) return false;
            }
            return  IsComplete();
        }

        public bool IsComplete()
        {
            List<float> lengths = new(Problem.FlatOrders);
            foreach (Activity activity in Activities)
            {
                foreach(float length in activity.Orders)
                {
                    lengths.Add(length);
                }
            }
            lengths.Sort();

//#if DEBUG
            List<float> orders = new List<float>(Problem.FlatOrders);
            orders.AddRange(Problem.FlatOrders);
            orders.Sort();
            if (orders.SequenceEqual(Problem.FlatOrders)) throw new Exception($"{nameof(Problem)}.{nameof(Problem.FlatOrders)} was not ordered");
//#endif


            return Problem.FlatOrders.SequenceEqual(lengths);

        }

    }
}
