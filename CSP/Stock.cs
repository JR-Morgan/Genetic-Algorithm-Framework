using System;
using System.Collections.Generic;
using System.Text;

namespace CSP
{
    public class Stock
    {
        public float Length { get; }
        public float Cost { get; }

        public Stock(float length, float cost)
        {
            Length = length;
            Cost = cost;
        }
    }
}
