using System.Numerics;

namespace Travling_sales_problem
{
    class Node
    {
        private readonly Vector2 position;

        public readonly int id;

        public Node(int id, Vector2 position)
        {
            this.id = id;
            this.position = position;
        }

        //public float DistanceTo(Vector2 position) => (this.position - position).Length();
        public float DistanceTo(Vector2 position) => Vector2.Distance(this.position, position);
        public float DistanceTo(Node node) => this.DistanceTo(node.position);
    }
}
