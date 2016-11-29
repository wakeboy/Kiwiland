using Graph.Exceptions;
using Graph.Graph;
using System.Linq;

namespace Graph.Extensions
{
    public static class GraphFindAllTripsExtension
    {
        /// <summary>
        /// Find all trips from the start to the end where the number of stops
        /// are less than or equal to maxStops
        /// </summary>
        /// <param name="startNodeName"></param>
        /// <param name="endNodeName"></param>
        /// <param name="maxStops">The maximum number of stops allowed while travelling from start to end</param>
        /// <param name="exactStopMatch">If the number of stops should be an exact match</param>
        /// <returns></returns>
        public static int FindAllTrips(this IGraph graph, string startNodeName, string endNodeName, int maxStops, bool exactStopMatch = false)
        {
            if (graph.Nodes.ContainsKey(startNodeName) == false)
                throw new NodeNotFoundException($"Could not find node {startNodeName}");

            var startNode = graph.Nodes.FirstOrDefault(n => n.Key == startNodeName).Value;

            var tripCount = 0;
            return CountTrips(startNode, endNodeName, --maxStops, exactStopMatch, ref tripCount);
        }

        private static int CountTrips(INode startNode, string endNodeName, int maxDepth, bool exactStopMatch, ref int count)
        {
            foreach (var connection in startNode.Connections)
            {
                if (connection.Node.Name == endNodeName)
                {
                    if (!exactStopMatch || exactStopMatch && maxDepth == 0)
                    {
                        count++;
                    }
                }
                if (maxDepth > 0)
                {
                    maxDepth--;
                    CountTrips(connection.Node, endNodeName, maxDepth, exactStopMatch, ref count);
                    maxDepth++;
                }
            }

            return count;
        }

    }
}
