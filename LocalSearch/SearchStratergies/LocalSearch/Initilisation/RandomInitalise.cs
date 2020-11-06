using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Initilisation
{
    class RandomInitalise : IInitalise
    {

        public Route Initalise(List<Node> nodes)
        {
            List<Node> routes = new List<Node>(nodes);
            routes.RemoveAt(0);
            Shuffle(routes);
            routes.Add(nodes[0]);
            routes[^1] = routes[0];
            routes[0] = nodes[0];


            return new Route()
            {
                RouteNodes = routes,
                ExpectedFinalNodeCount = nodes.Count
            };
        }

        private static void Shuffle<T>(IList<T> list)
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
