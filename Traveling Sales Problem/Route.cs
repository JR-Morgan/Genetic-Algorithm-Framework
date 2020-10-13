using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Travling_sales_problem
{
    public struct Route
    {

        public List<Node> RouteNodes { get; init; }
        public int ExpectedFinalNodeCount { private get; init; }

        public Route(Node startNode, int expectedFinalNodeCount)
        {
            RouteNodes = new List<Node>(expectedFinalNodeCount)
            {
                startNode
            };
            this.ExpectedFinalNodeCount = expectedFinalNodeCount;
        }

        public readonly Route Copy()
        {
            return new Route()
            {
                RouteNodes = new List<Node>(this.RouteNodes),
                ExpectedFinalNodeCount = this.ExpectedFinalNodeCount
            };
        }

        public void AddUnchecked(Node node)
        {
            RouteNodes.Add(node);
        }

        public bool AddCheck(Node node)
        {
            return !IsCompleted && !RouteNodes.Contains(node);
        }

        public bool Add(Node node)
        {
            if(AddCheck(node))
            {
                RouteNodes.Add(node);
                return true;
            }

            return false;
        }

        
        public readonly bool IsCompleted => RouteNodes.Count == ExpectedFinalNodeCount;

        public readonly float EvaluateDistance()
        {
            return EvaluateDistance(RouteNodes, ExpectedFinalNodeCount) + RouteNodes[^1].DistanceTo(RouteNodes[0]);
        }

        private static float EvaluateDistance(List<Node> routeNodes, int counter)
        {
            if (counter <= 1)
                return 0f;
            else
                return routeNodes[counter-- -1].DistanceTo(routeNodes[counter -1]) + EvaluateDistance(routeNodes, counter);
        }

        public bool Equals(Route? other) => other != null
                && ((Route)other).RouteNodes.SequenceEqual(RouteNodes)
                && ((Route)other).ExpectedFinalNodeCount.Equals(ExpectedFinalNodeCount);

    }
}
