using System;

namespace TSP.SearchStratergies.LocalSearch.TerminalConditions
{
    internal delegate bool TerminateCondition();
    internal delegate TerminateCondition TerminateStrategy();
    static class TerminateConditions
    {


        public static TerminateStrategy TimeOut(float timeOut)
        {
            return GenerateT;

            TerminateCondition GenerateT()
            {
                if (timeOut < 0) throw new ArgumentOutOfRangeException(nameof(timeOut), "value must be greater than 0");

                DateTime endTime = DateTime.Now.AddMilliseconds(timeOut);

                bool t() => DateTime.Now > endTime;
                return t;
            }
        }

        public static TerminateStrategy FixedItterations(int NumberOfItterations)
        {
            return GenerateT;

            TerminateCondition GenerateT()
            {
                if (NumberOfItterations < 1) throw new ArgumentOutOfRangeException(nameof(NumberOfItterations), "value must be greater than 1");

                int counter = -1;
                bool t() => ++counter >= NumberOfItterations;
                return t;
            }
        }
    }
}
