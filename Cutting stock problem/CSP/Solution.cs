using System.Collections.Generic;
using System.Text;

namespace CSP
{
    class Solution : ISolution
    {
        public Problem Problem { get; }
        public List<Activity> Activities { get; private set; }

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

    }
}
