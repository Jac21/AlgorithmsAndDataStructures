using System;

namespace Dijkstra
{
    public class Program
    {
        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            var min = int.MaxValue;
            var minIndex = 0;

            for (var v = 0; v < verticesCount; ++v)
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }

            return minIndex;
        }

        public static void DijkstraTraversal(int[,] graph, int source, int verticesCount)
        {
            // create vertex set
            var distance = new int[verticesCount];
            var shortestPathTreeSet = new bool[verticesCount];

            // initialization, for each vertex in Graph
            for (var i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue; // unknown distance from source to v
                shortestPathTreeSet[i] = false;
            }

            // distance from source to source
            distance[source] = 0;

            // while vertex set is not empty
            for (var count = 0; count < verticesCount - 1; ++count)
            {
                // Node with the least distance will be selected first
                var u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                // where vertex is still not in vertex set
                for (var v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue &&
                        distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                    }
            }

            Print(distance, verticesCount);
        }

        private static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Vertex    Distance from source");

            for (var i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }

        private static void Main()
        {
            int[,] graph =
            {
                {0, 6, 0, 0, 0, 0, 0, 9, 0},
                {6, 0, 9, 0, 0, 0, 0, 11, 0},
                {0, 9, 0, 5, 0, 6, 0, 0, 2},
                {0, 0, 5, 0, 9, 16, 0, 0, 0},
                {0, 0, 0, 9, 0, 10, 0, 0, 0},
                {0, 0, 6, 0, 10, 0, 2, 0, 0},
                {0, 0, 0, 16, 0, 2, 0, 1, 6},
                {9, 11, 0, 0, 0, 0, 1, 0, 5},
                {0, 0, 2, 0, 0, 0, 6, 5, 0}
            };

            DijkstraTraversal(graph, 0, 9);
        }
    }
}