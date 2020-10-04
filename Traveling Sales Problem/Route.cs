using System.Collections.Generic;

namespace Travling_sales_problem
{
    struct Route
    {

        private List<Node> routeNodes;
        private int expectedFinalNodeCount;

        public Route(Node startNode, int expectedFinalNodeCount)
        {
            routeNodes = new List<Node>(expectedFinalNodeCount)
            {
                startNode
            };
            this.expectedFinalNodeCount = expectedFinalNodeCount;
        }

        public readonly Route Copy()
        {
            return new Route()
            {
                routeNodes = new List<Node>(this.routeNodes),
                expectedFinalNodeCount = this.expectedFinalNodeCount
            };
        }

        public void AddUnchecked(Node node)
        {
            routeNodes.Add(node);
        }

        public bool AddCheck(Node node)
        {
            return !IsCompleted && !routeNodes.Contains(node);
        }

        public bool Add(Node node)
        {
            if(AddCheck(node))
            {
                routeNodes.Add(node);
                return true;
            }

            return false;
        }

        
        public readonly bool IsCompleted => routeNodes.Count == expectedFinalNodeCount;

        public readonly float EvaluateDistance()
        {
            return EvaluateDistance(routeNodes, expectedFinalNodeCount) + routeNodes[^1].DistanceTo(routeNodes[0]);
        }

        private static float EvaluateDistance(List<Node> routeNodes, int counter)
        {
            if (counter <= 1)
                return 0f;
            else
                return routeNodes[counter-- -1].DistanceTo(routeNodes[counter -1]) + EvaluateDistance(routeNodes, counter);
        }


    }
}
