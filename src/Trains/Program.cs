using Graph.Graph;

namespace Trains
{
    using Graph.Exceptions;
    using Graph.Extensions;
    using System;
    using System.IO;
    using Graph = Graph.Graph.Graph;

    public class Program
    {
        private static IGraph graph;

        public static void Main(string[] args)
        {
            graph = new Graph();

            if (args?.Length != 2)
            {
                Console.WriteLine("Missing arguements node file or edges file");
                Console.WriteLine("First argument required absolute path to the nodes file");
                Console.WriteLine("Second argument required absolute path to the edges file");
                Console.WriteLine("Example: \"D:\\GitHub\\Trains\\src\\Trains\\Data\\nodes.csv\" \"D:\\GitHub\\Trains\\src\\Trains\\Data\\edges.csv\"");
                Console.ReadKey();
            }
            else
            {
                GraphUtil.LoadNodes(graph, args[0]);
                GraphUtil.LoadEdges(graph, args[1]);
                Run();
            }
        }

        private static void Run()
        {
            var line = string.Empty;

            while (line != "q")
            {
                Console.WriteLine("Enter r to find all routes");
                Console.WriteLine("Enter t to find all trips");
                Console.WriteLine("Enter s to find shortest route");
                Console.WriteLine("Press d to find total distance");
                Console.WriteLine("Press q to quit");

                line = Console.ReadLine();

                switch (line)
                {
                    case "r":
                        FindAllRoutes();
                        break;
                    case "t":
                        FindAllTrips();
                        break;
                    case "s":
                        ShortestRoute();
                        break;
                    case "d":
                        TotalDistance();
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine($"{line} is an invalid command");
                        break;
                }
            }
        }

        private static void FindAllRoutes()
        {
            var line = string.Empty;

            Console.WriteLine("Find all routes from the start to the end where the maximum distance");
            Console.WriteLine("to travel is less than or equal to the maxDistance: ");
            Console.WriteLine("Input Format: startNodeName endNodeName maxDistance");
            Console.WriteLine("Enter x to go back.");

            while (line != "x")
            {
                line = Console.ReadLine();
                try
                {
                    var nodes = line.Split(' ');
                    var routes = graph.FindAllRoutes(nodes[0], nodes[1], int.Parse(nodes[2]));
                    Console.WriteLine($"Routes: {routes}");
                }
                catch (NodeNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong :(");
                }

            }
        }

        private static void FindAllTrips()
        {
            var line = string.Empty;

            Console.WriteLine("Find all trips from the start to the end where the number of stops");
            Console.WriteLine("are less than or equal to maxStops: ");
            Console.WriteLine("Input Format: startNodeName endNodeName maxStops exactStopMatch");
            Console.WriteLine("Enter x to go back.");

            while (line != "x")
            {
                line = Console.ReadLine();
                try
                {
                    var nodes = line.Split(' ');
                    var trips = graph.FindAllTrips(nodes[0], nodes[1], int.Parse(nodes[2]), bool.Parse(nodes[3]));
                    Console.WriteLine($"Trips: {trips}");
                }
                catch (NodeNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong :(");
                }

            }
        }

        private static void ShortestRoute()
        {
            var line = string.Empty;

            Console.WriteLine("Get the shortest distance between two stops: ");
            Console.WriteLine("Example: startNodeName endNodeName");
            Console.WriteLine("Enter x to go back.");

            while (line != "x")
            {
                line = Console.ReadLine();
                try
                {
                    var nodes = line.Split(' ');
                    var distance = graph.ShortestRoute(nodes[0], nodes[1]);
                    Console.WriteLine($"Distance: {distance}");
                }
                catch (NodeNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong :(");
                }

            }
        }

        private static void TotalDistance()
        {
            var line = string.Empty;

            Console.WriteLine("Enter node names seprated to calculate total distance: ");
            Console.WriteLine("Example: A B C");
            Console.WriteLine("Enter x to go back.");

            while (line != "x")
            {
                line = Console.ReadLine();
                try
                {
                    var distance = graph.TotalDistance(line.Split(' '));
                    Console.WriteLine($"Distance: {distance}");
                }
                catch(ConnectionNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong :(");
                }
                
            }
        }
    }
}
