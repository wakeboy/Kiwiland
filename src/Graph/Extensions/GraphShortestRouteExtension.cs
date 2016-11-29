using Graph.Exceptions;
using Graph.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Extensions
{
    public static class GraphShortestRouteExtension
    {
        /// <summary>
        /// Get the shortest distance between two stops
        /// </summary>
        /// <param name="startNodeName">The starting point</param>
        /// <param name="endNodeName">The end point</param>
        /// <returns></returns>
        public static int ShortestRoute(this IGraph graph, string startNodeName, string endNodeName)
        {
            if (graph.Nodes.ContainsKey(startNodeName) == false)
                throw new NodeNotFoundException($"Could not find node {startNodeName}");

            // Clone the nodes to make calls thread save
            var workingNodes = graph.Nodes.Values.ToList();

            // Rest node values distance froms start
            RestNodesDistanceFromStart(workingNodes, startNodeName);

            // Order nodes so starting node is first in the list
            workingNodes = workingNodes.OrderBy(n => n.DistanceFromStart).ToList();

            foreach (var n in workingNodes)
            {
                var connections = n.Connections.Where(c => workingNodes.Contains(n));

                foreach (var connection in connections)
                {
                    int distance = n.DistanceFromStart == int.MaxValue ? connection.Distance : n.DistanceFromStart + connection.Distance;

                    if (distance < connection.Node.DistanceFromStart || (startNodeName == endNodeName && connection.Node.Name == endNodeName))
                        connection.Node.DistanceFromStart = distance;
                }
            }

            return workingNodes.SingleOrDefault(n => n.Name == endNodeName).DistanceFromStart;
        }

        private static void RestNodesDistanceFromStart(IEnumerable<INode> workingNodes, string startNodeName)
        {
            foreach (var node in workingNodes)
            {
                if (node.Name == startNodeName)
                {
                    node.DistanceFromStart = 0;
                }
                else
                {
                    node.DistanceFromStart = int.MaxValue;
                }
            }
        }
    }
}
