using CSP;
using CSP.Search;
using SearchStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    static class Program
    {
        private static void ItterationEventHandler(ISearchStrategy<ISolution, Problem> sender, Log log)
        {
            Console.WriteLine(log.ToString());
        }

        /// <param name="args">
        /// 0 - file path
        /// </param>
        static void Main(string[] args)
        {
            string fileName;
            if (args.Length == 0)
            {
                fileName = @"ProblemFiles\test2.txt";
            }
            else
            {
                fileName = args[0];
            }



            Console.WriteLine($"\nParsing problem from {fileName}");
            Problem problem = Problem.ParseFromFile(fileName);
            Console.WriteLine($"\nFinished parsing problem from m:{problem.Stock.Length}, c:{problem.Orders.Count}");

            while (true)
            {

                ISearchStrategy<ISolution, Problem>? solutionStrategy = null;

                StringBuilder message = new();
                message.Append("\nSelect search strategy:\n");

                List<ISearchStrategy<ISolution, Problem>> searches = SearchFactory.GenerateSearchesItterations();
                for (int i = 0; i < searches.Count; i++)
                {
                    message.Append($"({i}): {searches[i]}\n");
                }

                while (true)
                {
                    Console.WriteLine(message.ToString());
                    string? s = Console.ReadLine();
                    if (s != null)
                    {
                        int index = Int32.Parse(s);
                        if (index >= 0 && index < searches.Count)
                        {
                            solutionStrategy = searches[index];
                            break;
                        }
                    }
                }



                solutionStrategy.OnItterationComplete += ItterationEventHandler;
                solutionStrategy.Compute(problem);


            }

        }

    }
}
