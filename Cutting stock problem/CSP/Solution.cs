using System.Collections.Generic;

namespace CSP
{
    class Solution : ISolution
    {
        public readonly Problem problem;
        public List<Activity> Activities { get; private set; }

        public Solution(Problem problem, List<Activity> activities)
        {
            this.problem = problem;
            Activities = activities;
        }

        public Solution(Problem problem) : this(problem, new List<Activity>()) { }

        public float Fitness()
        {
            float fitness = 0;
            foreach(Activity a in Activities)
            {
                fitness += a.Cost;
            }
            return fitness;
        }
    }
}
