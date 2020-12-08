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



        public override string ToString()
        {
            return $"Solution f = {Cost()}, ac = {Activities.Count}, av = {AreActivitiesValid()}, com = { IsComplete()}";
        }


        public Solution(Problem problem, List<Activity> activities)
        {
            this.Problem = problem;
            Activities = activities;
        }

        public Solution(Problem problem) : this(problem, new List<Activity>()) { }

        public ISolution Copy()
        {
            List<Activity> activities = new List<Activity>(Activities.Count);

            foreach(Activity a in Activities)
            {
                activities.Add(a.Copy());
            }

            return new Solution(Problem, activities);
        }


        public float Cost()
        {
            float totalCost = 0;
            foreach(Activity a in Activities)
            {
                totalCost += a.Cost;
            }
            return totalCost;
        }

        public bool AreActivitiesValid()
        {
            foreach (Activity activity in Activities)
            {
                if (!activity.IsValid)
                    return false;
            }
            return true;
        }

        public bool IsValid()
        {
            return AreActivitiesValid() && IsComplete(); 
        }

        public bool IsComplete()
        {
            List<float> lengths = new(Problem.FlatOrders.Count);
            foreach (Activity activity in Activities)
            {
                foreach(float length in activity.Orders)
                {
                    lengths.Add(length);
                }
            }
            lengths.Sort();

            return Problem.FlatOrders.SequenceEqual(lengths);

        }

    }
}
