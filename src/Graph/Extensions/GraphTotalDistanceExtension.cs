using Graph.Graph;
using System;
using System.Collections.Generic;

namespace Graph.Extensions
{
    public static class GraphTotalDistanceExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="nodeNames">Ordered list or stops to travel to</param>
        /// <returns>Total distance</returns>
        /// <exception cref="ConnectionNotFoundException"></exception>
        public static int TotalDistance(this IGraph graph, IEnumerable<string> nodeNames)
        {
            if (nodeNames == null)
                throw new ArgumentNullException("nodeNames");

            var distance = 0;
            INode currentNode = null;
            INode prevNode = null;

            foreach (var name in nodeNames)
            {
                currentNode = graph.Nodes[name];

                if (prevNode != null)
                {
                    distance += prevNode.ConnectionDistance(currentNode);
                }
                prevNode = currentNode;
            }

            return distance;
        }
    }
}
