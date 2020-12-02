using System.Collections.Generic;
using System.Linq;

namespace CSP
{
    class Activity //TODO might have better performance being a struct
    {
        public bool IsValid => RemainingLength >= 0;
        public float Cost => Stock.Cost;

        public Stock Stock { get; }

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

        #region RemainingLength

        private float? _remainingLength = null;
        public float RemainingLength
        {
            get
            {
                float CalculateRemainingLength() => Orders.Sum() - Stock.Length;
                return _remainingLength ??= CalculateRemainingLength();
            }
        }
        private void InvalidateRemainingLength() => _remainingLength = null;
        #endregion


        public Activity(Stock stock)
        {
            Orders = new();
            Stock = stock;
        }

    }
}