using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.Search.Neighbourhood
{
    class TwoOpt : INeighbourhood, ISwap
    {
        private static Random random = new Random();


        public List<ISolution> GenerateNeighbourhood(ISolution parent)
        {
            List<ISolution> solutions = new();
            int n = parent.Orders.Count;
            for (int i = 1; i < (n / 2); i++)
            {
                for (int j = i; j < n; j++)
                {
                    var swap = Swap(parent, i, j);
                    if(swap.CheckValidity(true))
                    {
                        solutions.Add(swap);
                    }
                    
                }
            }
            return solutions;
        }


        private static ISolution Swap(ISolution parent, int i, int j)
        {
            List<float> orders = new(parent.Orders);

            float temp = orders[i];
            orders[i] = orders[j];
            orders[j] = temp;

            return new ISolution(orders, parent.Stock);
        }



        public ISolution Swap(ISolution parent)
        {
            int n = parent.Orders.Count;
            return Swap(parent, random.Next(0, n), random.Next(0, n));
        }
    }
}
