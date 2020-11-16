using System.Collections.Generic;

namespace CSP
{
    class Solution
    {
        public List<Activity> activities;

        public Solution()
        {
            activities = new();
        }

        public float Fitness()
        {
            float totalCost = 0;
            foreach(Activity a in activities)
            {
                totalCost += a.Cost();
            }
            return totalCost;
        }
    }
}
