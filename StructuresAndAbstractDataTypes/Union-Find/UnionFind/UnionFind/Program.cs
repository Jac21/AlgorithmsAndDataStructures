using System;

/*
 * Works on disjoint-set data structures, which keeps track of a set of elements partioned
 * into a number of disjoint subsets. Union-find algorithm performs two ops:
 * 1. Find: Determine which subset element is in (which determines if they are in the same subset).
 * 2. Union: Join two subsets into a single subset.
 * Union-find determines if graph contains cycles */

namespace UnionFind
{
    class Program
    {
        static void Main()
        {
            /* Example graph
             * 0
             * |  \
             * |    \
             * 1 ---- 2 */

            int V = 3;
            int E = 3;
            Graph graph = new Graph(V, E);

            // add edge 0-1
            graph.Edges[0].Src = 0;
            graph.Edges[0].Dest = 1;

            // add edge 1-2
            graph.Edges[1].Src = 1;
            graph.Edges[1].Dest = 2;

            // add edge 0-2
            graph.Edges[2].Src = 0;
            graph.Edges[2].Dest = 2;

            // Output: Graph contains a cycle
            Console.WriteLine(graph.IsCycle(graph) == 1 ? "Graph contains a cycle" : "Graph doesn't contain a cycle");

            /* Example graph two
             * 0
             * |  \
             * |    \
             * 1     2 */

            int vTwo = 3;
            int eTwo = 2;
            Graph graphTwo = new Graph(vTwo, eTwo);

            // add edge 0-1
            graphTwo.Edges[0].Src = 0;
            graphTwo.Edges[0].Dest = 1;

            // add edge 0-2
            graphTwo.Edges[1].Src = 0;
            graphTwo.Edges[1].Dest = 2;

            // Output: Graph contains a cycle
            Console.WriteLine(graph.IsCycle(graphTwo) == 1 ? "Graph contains a cycle" : "Graph doesn't contain a cycle");
        }
    }

    public class Graph
    {
        public int V, E; // V = # of vertices & E = # of edges
        public Edge[] Edges; // collection of all edges

        public class Edge
        {
            public int Src, Dest;
        }

        public Graph(int v, int e)
        {
            V = v;
            E = e;
            Edges = new Edge[E];

            for (int i = 0; i < e; i++)
            {
                Edges[i] = new Edge();
            }
        }

        // a utility function to find the subset of an element i
        int find(int[] parent, int i)
        {
            if (parent[i] == -1)
            {
                return i;
            }

            return find(parent, parent[i]);
        }

        // a utility function to do union of two subsets
        void Union(int[] parent, int x, int y)
        {
            int xSet = find(parent, x);
            int ySet = find(parent, y);
            parent[xSet] = ySet;
        }

        // The main function to check whether a given graph
        // contains cycle or not
        public int IsCycle(Graph graph)
        {
            // Allocate memory for creating V subsets
            int[] parent = new int[graph.V];

            // initialize all subsets as single element sets
            for (int i = 0; i < graph.V; ++i)
                parent[i] = -1;

            // iterate through all edges of graph, find subset of both
            // vertices of every edge, if both subsets are same, then
            // there is a cycle in graph
            for (int i = 0; i < graph.E; ++i)
            {
                int x = graph.find(parent, graph.Edges[i].Src);
                int y = graph.find(parent, graph.Edges[i].Dest);

                if (x == y)
                    return 1;

                graph.Union(parent, x, y);
            }

            return 0;
        }
    }
}
