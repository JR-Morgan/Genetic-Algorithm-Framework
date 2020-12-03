using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CSP
{
    class SolutionWithoutActivities
    {
        public Problem Problem { get; }
        public List<Stock> Stock { get; }
        public List<float> Orders { get; }
        private int LastUnfulfillOrderIndex;

        public bool IsCompleted => LastUnfulfillOrderIndex == Orders.Count;

        public SolutionWithoutActivities(Problem problem, List<float> orders, List<Stock> stock)
        {
            Problem = problem;
            Stock = stock;
            Orders = orders;
            LastUnfulfillOrderIndex = 0;
        }

        public SolutionWithoutActivities(Problem problem, List<float> orders) : this(problem, orders, new()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>The number of unsatisfied orders after the add operation has taken place</returns>
        public int AddStock(Stock stock)
        {
            float remainingLength = stock.Length;

            Stock.Add(stock);
            while(LastUnfulfillOrderIndex < Orders.Count)
            {
                if (remainingLength - Orders[LastUnfulfillOrderIndex] < 0)
                {
                    break; 
                }
                remainingLength -= Orders[LastUnfulfillOrderIndex];

                LastUnfulfillOrderIndex++;
            }

            return Orders.Count - LastUnfulfillOrderIndex;
        }










        public bool CheckValidity(bool trim = false) //TODO there might be a bug here
        {
            int stockIndex = 0;
            float lengthCounter;
            void ResetCounter() => lengthCounter = Stock[stockIndex].Length;

            ResetCounter();
            foreach (float order in Orders)
            {
                while(lengthCounter < order)
                {
                    stockIndex++;
                    if (stockIndex > Stock.Count - 1) return false;
                    ResetCounter();
                }

                lengthCounter -= order;


            }

            //Trim Excess
            if (trim)
            {
                Stock.RemoveRange(stockIndex, Stock.Count - stockIndex);
            }
            return true;
        }



        public float Fitness()
        {
            float totalCost = 0;
            foreach(Stock s in Stock)
            {
                totalCost += s.Cost;
            }
            return totalCost;
        }


        //TODO ToString();
    }
}
