using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CSP
{
    public class Problem
    {
        public Stock[] Stock { get; }

        public List<float> FlatOrders { get; }

        public Dictionary<float, int> Orders { get; }

        public Problem(Stock[] stock) : this(stock, new()) { }
        public Problem(Stock[] stock, Dictionary<float, int> orders)
        {
            Stock = stock;
            Orders = orders;
            FlatOrders = FlattenDict(Orders);
        }

        public static List<float> FlattenDict(Dictionary<float, int> orders)
        {
            List<float> ordersFlat = new();

            foreach((float order, int quanitity) in orders)
            {
                for(int i = 0; i < quanitity; i++)
                {
                    ordersFlat.Add(order);
                }
            }

            return ordersFlat;
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
            Dictionary<float, int> orders = new(orderLengthContent.Count);

            for(int i = 0; i < stockLengthContent.Count; i++)
            {
                stock[i] = new Stock(stockLengthContent[i], stockCostContent[i]);
            }

            for (int i = 0; i < orderLengthContent.Count; i++)
            {
                orders.Add(orderLengthContent[i], orderQuantityContent[i]);
            }



            return new Problem(stock, orders);
        }
    }
}
