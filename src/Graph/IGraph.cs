using System.Collections.Generic;

namespace Graph.Graph
{
    public interface IGraph
    {
        /// <summary>
        /// The list of nodes in the graph
        /// </summary>
        Dictionary<string, INode> Nodes { get; }

        /// <summary>
        /// Add a new stop/node to the graph
        /// </summary>
        /// <param name="node"></param>
        void AddNode(INode node);

        /// <summary>
        /// Add a connection between two nodes
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="distance"></param>
        void AddConnection(INode fromNode, INode toNode, int distance);
    }
}
