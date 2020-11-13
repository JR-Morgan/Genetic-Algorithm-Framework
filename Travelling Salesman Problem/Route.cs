using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TSP
{
    public class Route : IEquatable<Route>
    {

        public List<Node> RouteNodes { get; init; }
        public int ExpectedFinalNodeCount { private get; init; }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder($"{{{RouteNodes[0].id}");

            for(int i = 1; i < RouteNodes.Count; i++)
            {
                message.Append(", ");
                message.Append(RouteNodes[i].id);
            }
            message.Append('}');
            return message.ToString();
        }

        public Route(int expectedFinalNodeCount)
            : this(new List<Node>(expectedFinalNodeCount), expectedFinalNodeCount)
        { }

        public Route(Node startNode, int expectedFinalNodeCount)
            : this(new List<Node>(expectedFinalNodeCount){startNode}, expectedFinalNodeCount)
        { }

        public  Route(List<Node> routeNodes, int expectedFinalNodeCount)
        {
            this.RouteNodes = routeNodes;
            this.ExpectedFinalNodeCount = expectedFinalNodeCount;
        }

        public Route(List<Node> routeNodes) : this(routeNodes, routeNodes.Count) { }

        public Route Copy() => new Route(
                routeNodes: new List<Node>(this.RouteNodes),
                expectedFinalNodeCount: this.ExpectedFinalNodeCount
            );

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
                needsEvaluation = true;
                return true;
            }

            return false;
        }

        
        public bool IsCompleted => RouteNodes.Count == ExpectedFinalNodeCount;

        private bool needsEvaluation = true;
        private float evaluation;
        public float Cost()
        {
            if(needsEvaluation)
            {
                evaluation = EvaluateDistance(RouteNodes, ExpectedFinalNodeCount) + RouteNodes[^1].DistanceTo(RouteNodes[0]);
                needsEvaluation = false;
            }
            return evaluation;
        }

        private static float EvaluateDistance(List<Node> routeNodes, int counter)
        {
            if (counter <= 1)
                return 0f;
            else
                return routeNodes[counter-- -1].DistanceTo(routeNodes[counter -1]) + EvaluateDistance(routeNodes, counter);
        }

        public bool Equals([MaybeNullWhen(false)] Route? other) => other != null
                && other.RouteNodes.SequenceEqual(RouteNodes)
                && other.ExpectedFinalNodeCount.Equals(ExpectedFinalNodeCount);


    }
}
