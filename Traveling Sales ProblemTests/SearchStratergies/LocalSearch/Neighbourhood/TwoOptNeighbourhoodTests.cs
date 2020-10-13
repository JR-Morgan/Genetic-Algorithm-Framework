using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Neighbourhood.Tests
{
    [TestClass()]
    public class TwoOptNeighbourhoodTests
    {
        [TestMethod()]
        public void GenerateNeighbourhoodTestNoDuplicates()
        {
            var startingRoute = new Route()
            {
                RouteNodes = new List<Node>() {
                    new Node(1),
                    new Node(2),
                    new Node(3),
                    new Node(4),
                },
                ExpectedFinalNodeCount = 4
            };

            var expectedResults = new List<Route>
            {
                new Route() {
                    RouteNodes = new List<Node>() {
                        new Node(1),
                        new Node(2),
                        new Node(3),
                        new Node(4),
                    },ExpectedFinalNodeCount = 4
                }, new Route() {
                    RouteNodes = new List<Node>() {
                        new Node(1),
                        new Node(3),
                        new Node(2),
                        new Node(4),
                    },ExpectedFinalNodeCount = 4
                }, new Route() {
                    RouteNodes = new List<Node>() {
                        new Node(1),
                        new Node(4),
                        new Node(3),
                        new Node(2),
                    },ExpectedFinalNodeCount = 4
                },
            };
            
            var actualResults = TwoOptNeighbourhood.GenerateNeighbourhood(startingRoute);

            Assert.AreEqual(expectedResults.Count, actualResults.Count);


            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.IsTrue(expectedResults[i].Equals(actualResults[i]));
            }
        }

    }
}