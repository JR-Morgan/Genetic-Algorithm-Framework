using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSP.Search.Initialisation
{
    class RandomInitalise : IInitialise
    {
        private static Random random = new Random();


        public ISolution Initalise(Problem problem)
        {
            Stock randomStock() => problem.Stock[random.Next(problem.Stock.Length)];

            List<float> orders = new(problem.Orders);
            orders.Shuffle(random);

            ISolution solution = new(orders);


            while(solution.AddStock(randomStock()) != 0) {}

            return solution;


        }
    }
}
