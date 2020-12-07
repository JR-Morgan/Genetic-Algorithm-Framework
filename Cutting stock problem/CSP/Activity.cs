using System;
using System.Collections.Generic;
using System.Linq;

namespace CSP
{
    class Activity : IEquatable<Activity>
    {
        public static float invalidCostFactor = 2f;
        public bool IsValid => RemainingLength >= 0;
        public bool IsEmpty => Orders.Count == 0;
        public float Cost => Stock.Cost + (Math.Max(0, -RemainingLength) * invalidCostFactor);

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

        public void Remove(float length)
        {
            Orders.Remove(length);
            InvalidateRemainingLength();
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

        public bool Equals(Activity other)
        {
            return this.Stock.Equals(other.Stock)
                && this.Orders.SequenceEqual(other.Orders)
                && this.Orders.Count == other.Orders.Count;
        }

        #endregion



        public Activity(Stock stock) : this(stock, new()) { }

        private Activity(Stock stock, List<float> orders)
        {
            Orders = orders;
            Stock = stock;
        }

        public override int GetHashCode()
        {
            int hash = Stock.GetHashCode();

            foreach(float order in Orders) hash ^= Orders.GetHashCode();

            return hash;
        }


    }
}