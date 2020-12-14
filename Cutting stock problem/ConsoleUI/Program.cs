using CSP;
using CSP.Search;
using CSP.ParameterOptimiser;
using SearchStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleUI
{
    static class Program
    {

        private static void ItterationEventHandler<S, P>(ISearchStrategy<S, P> sender, Log log)
        {
            Console.WriteLine(log.ToString());
        }


        private static void AverageEA(ISearchStrategy<ISolution, Problem> sender, Log log)
        {
            var ea = (EvolutionarySearch<ISolution, Problem>)sender;

            float averageCost = 0;
            float averageActivities = 0;
            float proportionValid = 1f;
            float proportionComplete = 1f;
            int counter = 0;

            float set(float val, float prop) => (val * counter + prop) / (counter + 1);

            foreach (ISolution s in ea.population)
            {
                averageCost = set(averageCost, s.Cost());
                averageActivities = set(averageActivities, s.ActivitiesCount);
                proportionValid = set(proportionValid, s.IsValid()? 1f : 0f);
                proportionValid = set(proportionComplete, s.IsComplete() ? 1f : 0f);

                counter++;
            }
            Console.WriteLine("Avg:");
            Console.WriteLine($"\tACost: {averageCost},\n\tAActiv: {averageActivities},\n\tPV: {proportionValid},\n\tPC {proportionComplete}");

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
            Console.WriteLine($"\nFinished parsing problem from m:{problem.Stock.Length}, c:{problem.FlatOrders.Count}");

            while (true)
            {

                ISearchStrategy<ISolution, Problem>? solutionStrategy = null;

                StringBuilder message = new();
                message.Append("\nSelect search strategy:\n");

                List<ISearchStrategy<ISolution, Problem>> searches = SearchFactory.GenerateSearchesTimeOut();
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
                        if (s.StartsWith('o'))
                        {
                            for (int i = 0; i < 6; i++) 
                            {
                                var optimisation = SearchFactory.EAOptimiser(problem);
                                optimisation.OnItterationComplete += ItterationEventHandler;
                                Thread.Sleep(10);
                                Task.Run(() => optimisation.Compute(new AlgorithmProblem()));
                            }

                        }
                        else
                        {
                            int index = int.Parse(s);
                            if (index >= 0 && index < searches.Count)
                            {
                                solutionStrategy = searches[index];
                                break;
                            }
                        }
                    }
                }



                solutionStrategy.OnItterationComplete += AverageEA;
                solutionStrategy.Compute(problem);


            }

        }

    }
}