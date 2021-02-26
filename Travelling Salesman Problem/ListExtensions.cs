using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    static class ListExtensions
    {
        /// <summary>
        /// Shuffles the contents of a list
        /// </summary>
        /// <remarks>
        /// This method was taken from https://stackoverflow.com/a/1262619/13874150 under the CC BY-SA 3.0 License and is unchanged
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">the <see cref="IList"/> to be shuffled</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
