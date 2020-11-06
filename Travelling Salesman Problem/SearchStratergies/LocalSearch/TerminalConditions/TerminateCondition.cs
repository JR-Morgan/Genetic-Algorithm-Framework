using System;

namespace TSP.SearchStratergies.LocalSearch.TerminalConditions
{
    internal delegate bool Terminate();
    static class TerminateCondition
    {
        public static Terminate TimeOut(float timeOut)
        {
            if (timeOut < 0) throw new ArgumentOutOfRangeException(nameof(timeOut), "value must be greater than 0");

            DateTime endTime = DateTime.Now.AddMilliseconds(timeOut);

            bool t() => DateTime.Now > endTime;
            return t;
        }

        public static Terminate FixedItterations(int NumberOfItterations)
        {
            if (NumberOfItterations < 1) throw new ArgumentOutOfRangeException(nameof(NumberOfItterations), "value must be greater than 1");

            int counter = -1;
            bool t() => ++counter >= NumberOfItterations;
            return t;
        }
    }
}
