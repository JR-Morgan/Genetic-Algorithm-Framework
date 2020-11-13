using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using TSP;
using TSP.Solution_Stratergies;
using TSP.Solution_Stratergies.LocalSearch;

namespace ConsoleUI
{
    class Program
    {

        private static void ItterationEventHandler(ISearchStrategy s, Log log)
        {
            Console.WriteLine(log.ToString());
        }

        /// <param name="args">
        /// 0 - file path
        /// 1-N - node indexes for cost evaluation 
        /// </param>
        static void Main(string[] args)
        {
            string fileName;
            if (args.Length == 0)
            {
                fileName = @"GraphFiles\ulysses16.csv";
            } else
            {
                fileName = args[0];
            }

            

            Console.WriteLine($"\nParsing graph from {fileName}");
            Graph graph = Graph.ParseGraphFromFile(fileName);

            if (args.Length > 1)
            {
                Route route = new Route(args.Length - 1);

                for (int i = 1; i < args.Length; i++)
                {
                    if(!route.Add(graph.nodes[Int32.Parse(args[i]) - 1]))
                    {
                        Console.WriteLine("\nRoute is invalid\n");
                        return;
                    }
                }
                if(route.IsCompleted)
                {
                    Console.WriteLine($"\nRoute has a cost of {route.Distance()}\n");
                    return;
                }
            }

            while (true)
            {

                ISearchStrategy? solutionStrategy = null;

                StringBuilder message = new StringBuilder();
                message.Append("\nSelect search strategy:\n");
                    
                List<ISearchStrategy> searches = MyLocalSearches.GenerateSearchesTimeOut();
                for(int i = 0; i < searches.Count; i++)
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
                solutionStrategy.Compute(graph);


            }

        }

    }
}
