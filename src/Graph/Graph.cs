using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Exceptions;

namespace Graph.Graph
{
    public class Graph : IGraph
    {
        private readonly Dictionary<string, INode> nodes;

        public Graph()
        {
            nodes = new Dictionary<string, INode>();
        }

        public Dictionary<string, INode> Nodes
        {
            get
            {
                return this.nodes;
            }
        }    

        public void AddNode(INode node)
        {
            if (nodes.ContainsKey(node.Name) == true)
                return;

            this.nodes.Add(node.Name, node);
        }

        public void AddConnection(INode fromNode, INode toNode, int distance)
        {
            if (nodes.ContainsKey(fromNode.Name) == false)
                throw new NodeNotFoundException($"Could not find node {fromNode?.Name}");

            if (nodes.ContainsKey(toNode.Name) == false)
                throw new NodeNotFoundException($"Could not find node {toNode?.Name}");
        
            fromNode.AddConnection(nodes[toNode.Name], distance);
        }
    }
}
