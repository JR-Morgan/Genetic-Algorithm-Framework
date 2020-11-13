using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TSP
{
    /// <summary>
    /// Encaspuate a candidate route
    /// </summary>
    public class Route : IEquatable<Route>
    {

        public List<Node> RouteNodes { get; init; }
        public int ExpectedFinalNodeCount { private get; init; }

        /// <summary>
        /// Creates an empty <see cref="Route"/>
        /// </summary>
        /// <param name="expectedFinalNodeCount">The expected capacity of a valid route</param>
        public Route(int expectedFinalNodeCount)
            : this(new List<Node>(expectedFinalNodeCount), expectedFinalNodeCount)
        { }

        /// <summary>
        /// Creates an incomplete <see cref="Route"/> with the first <see cref="Node"/> set
        /// </summary>
        /// <param name="startNode">The first node in the <see cref="Route"/></param>
        /// <param name="expectedFinalNodeCount">The expected capacity of a valid route</param>
        public Route(Node startNode, int expectedFinalNodeCount)
            : this(new List<Node>(expectedFinalNodeCount){startNode}, expectedFinalNodeCount)
        { }

        /// <summary>
        /// Creates a partially completed <see cref="Route"/>
        /// </summary>
        /// <param name="routeNodes">A collection of <see cref="Node"/>s</param>
        /// <param name="expectedFinalNodeCount">The expected capacity of a valid route</param>
        public Route(List<Node> routeNodes, int expectedFinalNodeCount)
        {
            this.RouteNodes = routeNodes;
            this.ExpectedFinalNodeCount = expectedFinalNodeCount;
        }
        /// <summary>
        /// Creates a fully completed <see cref="Route"/>.<br/>
        /// Note: creating a route this way does not guarentee the route is valid.
        /// </summary>
        /// <param name="routeNodes">The complete collection of Nodes</param>
        internal Route(List<Node> routeNodes) : this(routeNodes, routeNodes.Count) { }

        /// <summary>
        /// Creates a deep clone of the <see cref="Route"/>
        /// </summary>
        /// <returns></returns>
        public Route Copy() => new Route(
                routeNodes: new List<Node>(this.RouteNodes),
                expectedFinalNodeCount: this.ExpectedFinalNodeCount
            );

        /// <summary>
        /// Adds the <paramref name="node"/> to the <see cref="Route"/> without checking its validity
        /// </summary>
        /// <remarks>
        /// Consider using <seealso cref="AddCheck(Node)"/> to first check or use <seealso cref="Add(Node)"/> instead
        /// </remarks>
        /// <param name="node"></param>
        public void AddUnchecked(Node node)
        {
            RouteNodes.Add(node);
            
        }

        /// <summary>
        /// Checks if the addition of the <paramref name="node"/> is valid.
        /// </summary>
        /// <param name="node">The node to be checked</param>
        /// <returns>True if the <paramref name="node"/> can be added, false otherwise</returns>
        public bool AddCheck(Node node)
        {
            return !IsCompleted && !RouteNodes.Contains(node);
        }

        /// <summary>
        /// Adds a node to the route
        /// </summary>
        /// <param name="node"></param>
        /// <returns>returns true if the node was a valid addition, false otherwise</returns>
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

        /// <summary>
        /// Returns true if the number of <see cref="RouteNodes"/> is equal to <see cref="ExpectedFinalNodeCount"/>
        /// </summary>
        public bool IsCompleted => RouteNodes.Count == ExpectedFinalNodeCount;

        private bool needsEvaluation = true;
        private float evaluation;

        /// <summary>
        /// Evaluates the total euclidean distance of the route
        /// </summary>
        /// <returns>The distance</returns>
        public float Distance()
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

        public override string ToString()
        {
            StringBuilder message = new StringBuilder($"{{{RouteNodes[0].id}");

            for (int i = 1; i < RouteNodes.Count; i++)
            {
                message.Append(", ");
                message.Append(RouteNodes[i].id);
            }
            message.Append('}');
            return message.ToString();
        }
    }
}
