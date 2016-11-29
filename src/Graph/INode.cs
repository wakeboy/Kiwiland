using System.Collections.Generic;

namespace Graph.Graph
{
    public interface INode
    {
        /// <summary>
        /// The name of the node
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// List of nodes the node is connected to
        /// </summary>
        IList<NodeConnection> Connections { get; }

        /// <summary>
        /// The distance this node is from the starting node
        /// </summary>
        int DistanceFromStart { get; set; }

        /// <summary>
        /// Add a connection to this node
        /// </summary>
        /// <param name="targetNode">Node to connect to</param>
        /// <param name="distance">Distance from this to toNode</param>
        void AddConnection(INode targetNode, int distance);

        /// <summary>
        /// Find the distance to a connection if it exists
        /// </summary>
        /// <param name="target">The node to find the distance for</param>
        /// <returns>The distance</returns>
        /// <exception cref="ConnectionNotFoundException"></exception>
        int ConnectionDistance(INode target);
    }
}
