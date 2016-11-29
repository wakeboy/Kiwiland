using FluentAssertions;
using Graph.Exceptions;
using Graph.Extensions;
using Graph.Graph;
using NUnit.Framework;
using System;
using System.Linq;

namespace Graph.Tests
{
    [TestFixture]
    public class GraphTests
    {
        private IGraph graph;

        [SetUp]
        public void Setup()
        {
            this.graph = new Graph.Graph();
        }

        [TearDown]
        public void TareDown()
        {
            this.graph = null;
        }

        [Test]
        public void should_add_node_to_graph()
        {
            INode node = new Node
            {
                Name = "Node"
            };
            
            this.graph.AddNode(node);

            var actual = graph.Nodes;

            actual.Count().Should().Be(1);
            actual.First().Key.Should().Be("Node");
            actual.First().Value.Should().Be(node);
        }

        [Test]
        public void should_add_connection_between_nodes()
        {
            INode node1 = new Node
            {
                Name = "Node1"
            };

            INode node2 = new Node
            {
                Name = "Node2"
            };

            this.graph.AddNode(node1);
            this.graph.AddNode(node2);
            this.graph.AddConnection(node1, node2, 5);

            var nodes = this.graph.Nodes;
            var connectedNode = nodes[node1.Name];
            var nonConnectedNode = nodes[node2.Name];

            connectedNode.Connections.Count().Should().Be(1);
            connectedNode.Connections[0].Node.Should().Be(node2);
            nonConnectedNode.Connections.Should().BeNullOrEmpty();
        }
        
        [TestCase("A,B,C", 9)]
        [TestCase("A,D", 5)]
        [TestCase("A,D,C", 13)]
        [TestCase("A,E,B,C,D", 22)]
        public void should_return_total_distance_between_nodes(string nodeNameList, int expectedDistance)
        {
            PopulateTestGraph();
            var nodeNames = nodeNameList.Split(',');

            var distance = this.graph.TotalDistance(nodeNames);

            distance.Should().Be(expectedDistance);
        }

        [Test]
        public void should_throw_route_not_found_exception()
        {
            PopulateTestGraph();

            Action distance = () => this.graph.TotalDistance(new[] { "A", "E", "D" } );

            distance.ShouldThrow<ConnectionNotFoundException>()
                    .WithMessage("NO SUCH ROUTE");
        }

        [Test]
        [Description("Calculate the number of tirps which can be made upto and including the maxStops")]
        public void should_calculate_number_of_trips()
        {
            PopulateTestGraph();

            var numberOfTrips = this.graph.FindAllTrips("C", "C", 3);

            numberOfTrips.Should().Be(2);
        }

        [Test]
        [Description("Calculate the number of tirps which can be made with exact maxStops")]
        public void should_calculate_number_of_trips_with_exact_stops()
        {
            PopulateTestGraph();

            var numberOfTrips = this.graph.FindAllTrips("A", "C", 4, true);
        
            numberOfTrips.Should().Be(3);
        }
        
        [TestCase("A", "C", 9)]
        [TestCase("A", "E", 7)]
        [TestCase("B", "B", 9)]
        public void should_calculate_shortest_distance(string startNode, string endNode, int expectedDistance)
        {
            PopulateTestGraph();

            var distance = this.graph.ShortestRoute(startNode, endNode);

            distance.Should().Be(expectedDistance);
        }

        [Test]
        public void should_find_all_routes ()
        {
            PopulateTestGraph();

            var routeCount = this.graph.FindAllRoutes("C", "C", 29);

            routeCount.Should().Be(7);
        }

        private void PopulateTestGraph()
        {
            var a = new Node { Name = "A" };
            var b = new Node { Name = "B" };
            var c = new Node { Name = "C" };
            var d = new Node { Name = "D" };
            var e = new Node { Name = "E" };

            this.graph.AddNode(a);
            this.graph.AddNode(b);
            this.graph.AddNode(c);
            this.graph.AddNode(d);
            this.graph.AddNode(e);

            this.graph.AddConnection(a, b, 5);
            this.graph.AddConnection(b, c, 4);
            this.graph.AddConnection(c, d, 8);
            this.graph.AddConnection(d, c, 8);
            this.graph.AddConnection(d, e, 6);
            this.graph.AddConnection(a, d, 5);
            this.graph.AddConnection(c, e, 2);
            this.graph.AddConnection(e, b, 3);
            this.graph.AddConnection(a, e, 7);
        }
    }
}
