using Graph.Exceptions;
using Graph.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Extensions
{
    /// <summary>
    /// Find all routes from the start to the end where the maximum distance
    /// to travel is less than or equal to the maxDistance
    /// </summary>
    /// <param name="startNodeName"></param>
    /// <param name="endNodeName"></param>
    /// <param name="maxDistance">The maximum distance allowed to travel from start to end</param>
    /// <returns></returns>
    public static class GraphFindAllRoutesExtension
    {
        public static int FindAllRoutes(this IGraph graph, string startNodeName, string endNodeName, int maxDistance)
        {
            if (graph.Nodes.ContainsKey(startNodeName) == false)
                throw new NodeNotFoundException($"Could not find node {startNodeName}");

            var startNode = graph.Nodes.FirstOrDefault(n => n.Key == startNodeName).Value;

            var routeCount = 0;
            var currentDistance = 0;

            return CountRoutes(startNode, endNodeName, maxDistance, ref routeCount, ref currentDistance);
        }

        private static int CountRoutes(INode startNode, string endNodeName, int maxDistance, ref int count, ref int currentDistance)
        {
            foreach (var connection in startNode.Connections)
            {
                currentDistance += connection.Distance;
                if (currentDistance <= maxDistance)
                {
                    if (connection.Node.Name == endNodeName) count++;

                    CountRoutes(connection.Node, endNodeName, maxDistance, ref count, ref currentDistance);
                }
                currentDistance -= connection.Distance;
            }

            return count;
        }


    }
}
