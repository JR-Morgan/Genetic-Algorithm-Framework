using System;

namespace SearchStrategies.TerminalConditions
{
    public delegate bool TerminateCondition();
    public delegate TerminateCondition TerminateStrategy();

    /// <summary>
    /// Class contains a series of terminate functions
    /// </summary>
    public static class TerminalStrategies
    {

        /// <summary>
        /// Terminate function for terminating after a set amount of time
        /// </summary>
        /// <param name="timeOut">time in ms</param>
        /// <returns></returns>
        public static TerminateStrategy TimeOut(float timeOut)
        {
            return GenerateT;

            TerminateCondition GenerateT()
            {
                if (timeOut < 0) throw new ArgumentOutOfRangeException(nameof(timeOut), "Value must be greater than 0");

                DateTime endTime = DateTime.Now.AddMilliseconds(timeOut);

                bool t() => DateTime.Now > endTime;
                return t;
            }
        }

        /// <summary>
        /// Terminate function for terminating after a set number of iterations
        /// </summary>
        /// <param name="NumberOfIterations"></param>
        /// <returns></returns>
        public static TerminateStrategy FixedItterations(int NumberOfIterations)
        {
            return GenerateT;

            TerminateCondition GenerateT()
            {
                if (NumberOfIterations < 1) throw new ArgumentOutOfRangeException(nameof(NumberOfIterations), "Value must be greater than 1");

                int counter = -1;
                bool t() => ++counter >= NumberOfIterations;
                return t;
            }
        }
    }
}
