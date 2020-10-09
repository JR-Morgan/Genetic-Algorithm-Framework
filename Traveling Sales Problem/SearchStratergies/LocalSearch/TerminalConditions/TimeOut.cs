using System;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.TerminalConditions
{
    static class TimeOut
    {

        public static Terminate SetTimeOut(float timeOut)
        {

            DateTime endTime = DateTime.Now.AddMilliseconds(timeOut);

            bool t() => DateTime.Now > endTime;
            return t;
        }
    }
}
