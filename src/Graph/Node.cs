using Graph.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Graph
{
    public class Node : INode
    {
        private readonly List<NodeConnection> connections;

        public Node()
        {
            connections = new List<NodeConnection>();
        }

        public string Name { get; set; }

        public int DistanceFromStart { get; set; }

        public IList<NodeConnection> Connections
        {
            get
            {
                return this.connections;
            }
        }

        public void AddConnection(INode targetNode, int distance)
        {
            if (targetNode == null) throw new ArgumentNullException("toNode");
            if (targetNode == this) throw new ArgumentException("Node may not connect to itself");
            if (distance <= 0) throw new ArgumentException("Distance must be greater than 0");

            var connection = new NodeConnection
            {
                Node = targetNode,
                Distance = distance
            };

            this.Connections.Add(connection);
        }

        public int ConnectionDistance (INode target)
        {
            var connection = this.connections.FirstOrDefault(c => c.Node.Name == target.Name);

            if (connection == null)
                throw new ConnectionNotFoundException("NO SUCH ROUTE");

            return connection.Distance;
        }
    }

    public class NodeConnection
    {
        public INode Node { get; set; }

        public int Distance { get; set; }
    }
}
