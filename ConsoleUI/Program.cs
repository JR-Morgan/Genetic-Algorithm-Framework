using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using TSP;
using TSP.Solution_Stratergies;
using TSP.Solution_Stratergies.LocalSearch;

namespace ConsoleUI
{
    class Program
    {
        private static Graph ParseGraphFromFile(string file)
        {
            var nodes = new List<Node>();
            StreamReader reader = File.OpenText(file);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("\""))
                {
                    string[] elements = line.Split(",");
                    nodes.Add(new Node(
                        id: int.Parse(elements[0]),
                        position: new Vector2(float.Parse(elements[1]),
                                              float.Parse(elements[2]))
                        ));

                }
            }

            return new Graph(nodes);
        }



        private static void ItterationEventHandler(ISearchStratergy s, Log log)
        {
            Console.WriteLine(log.ToString());
        }


        static void Main(string[] args)
        {
            string fileName = "graph.csv";

            Console.WriteLine($"Parsing graph from {fileName}");
            Graph graph = ParseGraphFromFile(fileName);

            while(true)
            {


            ISearchStratergy? solutionStratergy = null;

            while(solutionStratergy == null)
            {
                Console.WriteLine($"Select search stratergy:\n" +
                                $"{nameof(ExhaustiveSearch)} (e)\n" +
                                $"{nameof(MyLocalSearches.LS1)}(rand, 2opt, best, timeout) (1)\n" +
                                $"{nameof(MyLocalSearches.LS2)}(greed, 2opt, best, allways) (2)\n" +
                                $"{nameof(MyLocalSearches.GN1)}(genetic) (3)");
                string? s = Console.ReadLine();
                switch (s)
                {
                    case "e":
                        solutionStratergy = new ExhaustiveSearch();
                        break;
                    case "1":
                        solutionStratergy = MyLocalSearches.LS1();
                        break;
                    case "2":
                        solutionStratergy = MyLocalSearches.LS2();
                        break;
                    case "3":
                        solutionStratergy = MyLocalSearches.GN1(100, 5);
                        break;
                        default:
                    Console.WriteLine($"Invalid Option \"{s}\"");
                    break;
                }
            }
                solutionStratergy.OnItterationComplete += ItterationEventHandler;
                solutionStratergy.Compute(graph);


            }

        }

    }
}
