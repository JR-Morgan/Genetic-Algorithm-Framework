using System;
using System.Collections.Generic;
using System.Linq;

namespace CSP
{
    class Activity //TODO might have better performance being a struct
    {
        public bool IsValid => RemainingLength >= 0;
        public bool IsEmpty => Orders.Count == 0;
        public float Cost => Stock.Cost;

        public Stock Stock { get; set; }
        public List<float> Orders { get; }


        public bool Add(float length)
        {
            if(length < RemainingLength)
            {
                Orders.Add(length);
                InvalidateRemainingLength();
                return true;
            }
            return false;
        }

        public Activity Copy()
        {
            List<float> orders = new(Orders);
            orders.AddRange(Orders);
            return new Activity(Stock, orders);
        }

        #region RemainingLength

        private float? _remainingLength = null;
        public float RemainingLength
        {
            get
            {
                float CalculateRemainingLength() => Stock.Length - Orders.Sum();
                return _remainingLength ??= CalculateRemainingLength();
            }
        }
        private void InvalidateRemainingLength() => _remainingLength = null;
        #endregion


        public Activity(Stock stock) : this(stock, new()) { }

        private Activity(Stock stock, List<float> orders)
        {
            Orders = orders;
            Stock = stock;
        }


    }
}