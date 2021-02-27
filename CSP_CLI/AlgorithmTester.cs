using CSP;
using CSP.Search;
using SearchStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSP_CLI
{
    static class AlgorithmTester
    {
        private static float TIME = 7000;
        private const int STARTING_SEED = 255;
        private const int PROBLEM_SEED = 99999;
        private const int NUMBER_OF_ITTERATIONS = 30;
        private const int NUMBER_OF_RUNS = 1;

        private static int programSeed = STARTING_SEED;


        private readonly static string verboseFile = $"E:\\\\Verbose_{runID}.csv";
        private readonly static long runID = DateTime.Now.ToFileTime();
        private static string ResultsFile() => $"E:\\\\Results_Problem1_{TIME}_{runID}.csv";
        private async static Task<Log[]> RunAsync<S,P>(P problem, params ISearchStrategy<S, P>[] searches)
        {
            List<Task<Log>> tasks = new(searches.Length);

            foreach (var search in searches)
            {
                tasks.Add(Task.Run(() => search.Compute(problem)));
            }
            return await Task.WhenAll(tasks);
        }

        public static void Run(Problem problem)
        {
            File.Create(ResultsFile()).Close();
            File.AppendAllLines(ResultsFile(), new string[] { "A Mean, B Mean, A STD, B STD" });

            for (int i = 0; i < NUMBER_OF_RUNS; i++)
            {
                //List<float> aCosts = new(NUMBER_OF_RUNS);
                //List<float> bCosts = new(NUMBER_OF_RUNS);


                for (int j = 0; j < NUMBER_OF_ITTERATIONS; j++)
                {
                    var A = SearchFactory.AlgorithmA(TIME, programSeed);
                    var B = SearchFactory.AlgorithmB(TIME, programSeed);


                    Task.WaitAll(RunAsync(problem, A, B));

                    WriteCosts(A.BestSolution.Cost(), B.BestSolution.Cost());
                }
                //TIME += 50;
                /*
                float aMean = aCosts.Sum() / NUMBER_OF_ITTERATIONS;
                float bMean = bCosts.Sum() / NUMBER_OF_ITTERATIONS;

                float stdSum = 0;
                foreach(float x in aCosts)
                {
                    stdSum += (x - aMean) * (x - aMean);
                }
                float aStd = (float)Math.Sqrt(stdSum / NUMBER_OF_ITTERATIONS);

                stdSum = 0;
                foreach (float x in bCosts)
                {r
                    stdSum += (x - bMean) * (x - bMean);
                }
                float bStd = (float)Math.Sqrt(stdSum / NUMBER_OF_ITTERATIONS);
                 */
                //WriteCosts(aMean, bMean, aStd, bStd);
                programSeed += STARTING_SEED;
            }



        }

        private static void WriteLogs(Log a, Log b)
        {
            File.AppendAllLines(verboseFile, new string[] { a.ToCSV(), b.ToCSV() });
        }

        private static void WriteCosts(float a, float b)
        {
            File.AppendAllLines(ResultsFile(), new string[] { $"{a}, {b}" });
            Console.WriteLine($"{a}, {b}");
        }



        #region Problem Constants
        const int m = 8;
        const int stockLengthUpperBound = 100;
        const int stockLengthLowerBound = 50;

        const int stockCostUpperBound = 90;
        const int stockCostLowerBound = 60;

        const int orderLengthUpperBound = 50;
        const int orderLengthLowerBound = 20;

        const int scaleFactor = 50; 

        const int orderTypesUpperBound = 25;
        const int orderTypesLowerBound = 15;

        const int orderQuantityUpperBound = 20;
        const int orderQuantityLowerBound = 1;

        #endregion

        private static Random Random = new Random(PROBLEM_SEED);
        /*private static Problem NextProblem()
        {
            
            Stock[] stock = new Stock[m];
            {
                float[] lengths = new float[m];
                for (int i = 0; i < m; i++)
                {
                    lengths[i] = Random.Next(stockLengthLowerBound, stockLengthUpperBound) * scaleFactor;
                }

                float[] costs = new float[m];
                for (int i = 0; i < m; i++)
                {
                    costs[i] = Random.Next(stockCostLowerBound, stockCostUpperBound);
                }

                for (int i = 0; i < m; i++)
                {
                    stock[i] = new Stock(lengths[i], costs[i]);
                }
            }

            int n = Random.Next(orderTypesLowerBound, orderTypesUpperBound);
            Dictionary<float, int> orders = new(n);
            {
                for(int i = 0; i < n; i++)
                {
                    float length = Random.Next(orderLengthLowerBound, orderLengthUpperBound) * scaleFactor;
                    int quantity = Random.Next(orderQuantityLowerBound, orderQuantityUpperBound);
                    if(orders.ContainsKey(length))
                    {
                        i--; 
                    }
                    else
                    {
                        orders.Add(length, quantity);
                    }
                   
                }
            }

            return new Problem(stock, orders);
        }*/
    }
}
