using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CSP
{
    public class Problem
    {
        public Stock[] Stock { get; }

        public List<float> Orders { get; }

        public Problem(Stock[] stock) : this(stock, new()) { }
        public Problem(Stock[] stock, List<float> orders)
        {
            Stock = stock;
            orders.Sort();
            Orders = orders;
        }

        public static Problem ParseFromFile(string file)
        {
            const string stockLengthPattern = @"^Stock lengths: +([0-9]+\.?[0-9]*, +)*[0-9]+\.?[0-9]*$",
                         stockCostPattern = @"^Stock costs: +([0-9]+\.?[0-9]*, +)*[0-9]+\.?[0-9]*$",
                         orderLengthPattern = @"^Piece lengths: +([0-9]+\.?[0-9]*, +)*[0-9]+\.?[0-9]*$",
                         orderQuantityPattern = @"^Quantities: +([0-9]*, +)*[0-9]+$";


            StreamReader reader = File.OpenText(file);
            string? line;


            List<float> stockLengthContent = new(),
                        stockCostContent = new(),
                        orderLengthContent = new();
            List<int>   orderQuantityContent = new();

            while ((line = reader.ReadLine()) != null)
            {
                string[] parameters = line.Split(':');
                if(parameters.Length == 2)
                {
                    string[] elements = parameters[1].Split(',');

                    if      (Regex.IsMatch(line, stockLengthPattern))
                        foreach (string e in elements)stockLengthContent.Add(float.Parse(e));

                    else if (Regex.IsMatch(line, stockCostPattern))
                        foreach (string e in elements) stockCostContent.Add(float.Parse(e));

                    else if (Regex.IsMatch(line, orderLengthPattern))
                        foreach (string e in elements) orderLengthContent.Add(float.Parse(e));

                    else if (Regex.IsMatch(line, orderQuantityPattern))
                        foreach (string e in elements) orderQuantityContent.Add(int.Parse(e));

                }
            }


            if (stockLengthContent.Count != stockCostContent.Count
            || orderLengthContent.Count != orderQuantityContent.Count)
                throw new Exception("Failed to parse file, List have inconsistent length");

            Stock[] stock = new Stock[stockLengthContent.Count];
            List<float> order = new(orderLengthContent.Count);

            for(int i = 0; i < stockLengthContent.Count; i++)
            {
                stock[i] = new Stock(stockLengthContent[i], stockCostContent[i]);
            }

            for (int i = 0; i < orderLengthContent.Count; i++)
            {
                for(int j = 0; j< orderQuantityContent[i]; j++)
                {
                    order.Add(orderLengthContent[i]);
                }
            }



            return new Problem(stock, order);
        }
    }
}
