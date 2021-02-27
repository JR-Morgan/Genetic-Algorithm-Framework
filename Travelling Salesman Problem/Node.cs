using System;
using System.Numerics;

namespace TSP
{
    /// <summary>
    /// Encapsulates a <see cref="Graph"/> node.<br/>
    /// I.E. a city.
    /// </summary>
    public class Node : IEquatable<Node>
    {
        private readonly Vector2 position;

        public readonly int id;

        /// <summary>
        /// Creates a new Node
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        public Node(int id, Vector2 position = default)
        {
            this.id = id;
            this.position = position;
        }

        /// <summary>
        /// Returns the Euclidean distance between this <see cref="Node"/> and a given <paramref name="position"/>
        /// </summary>
        /// <param name="position">The position to measure distance from</param>
        /// <returns>The distance</returns>
        public float DistanceTo(Vector2 position) => Vector2.Distance(this.position, position);

        /// <summary>
        /// Returns the Euclidean distance between this <see cref="Node"/> and a given <paramref name="node"/>
        /// </summary>
        /// <param name="position">The <see cref="Node"/> to measure distance from</param>
        /// <returns>The distance</returns>
        public float DistanceTo(Node node) => this.DistanceTo(node.position);

        public bool Equals(Node? other) => other != null
            && other.position.Equals(position)
            && other.id.Equals(id);
    }
}
