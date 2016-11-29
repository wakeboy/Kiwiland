using Graph.Graph;
using System.IO;

namespace Trains
{
    public class GraphUtil
    {
        /// <summary>
        /// Load nodes from file
        /// </summary>
        /// <param name="filePath"></param>
        public static void LoadNodes (IGraph graph, string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));

            while(!reader.EndOfStream)
            {
                var nodeName = reader.ReadLine();
                graph.AddNode(new Node { Name = nodeName });
            }
        }

        /// <summary>
        /// Load edges from file
        /// </summary>
        /// <param name="filePath"></param>
        public static void LoadEdges(IGraph graph, string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));

            while (!reader.EndOfStream)
            {
                var connection = reader.ReadLine().Split(',');

                var fromNode = graph.Nodes[connection[0]];
                var toNode = graph.Nodes[connection[1]];

                var distance = 0;
                int.TryParse(connection[2], out distance);

                graph.AddConnection(fromNode, toNode, distance);
            }
        }
    }
}
