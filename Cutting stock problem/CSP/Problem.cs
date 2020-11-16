using System;
using System.Collections.Generic;

namespace CSP
{
    public class Problem
    {
        public List<(float length, float cost)> Stock { get; }

        public List<(float length, int quanitity)> Order { get; }

        public Problem()
        {
            Stock = new();
            Order = new();
        }

        public static Problem ParseFromFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
