using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
    public class Program
    {
        private static void Main()
        {
            char[][] matrix =
            {
                new[] {'-', 'S', '-', '-', 'X'},
                new[] {'-', 'X', 'X', '-', '-'},
                new[] {'-', '-', '-', 'X', '-'},
                new[] {'X', '-', 'X', 'E', '-'},
                new[] {'-', '-', '-', '-', 'X'}
            };


            // looking for shortest path from 'S' at (0,1) to 'E' at (3,3)
            // obstacles marked by 'X'
            const int fromX = 0;
            const int fromY = 1;
            const int toX = 3;
            const int toY = 3;

            MatrixNode endNode = AStarSearch(matrix, fromX, fromY, toX, toY);

            // looping through the Parent nodes until we get to the start node
            Stack<MatrixNode> path = new Stack<MatrixNode>();

            while (endNode.X != fromX || endNode.Y != fromY)
            {
                path.Push(endNode);
                endNode = endNode.Parent;
            }

            path.Push(endNode);

            Console.WriteLine("The shortest path from  " +
                              "(" + fromX + "," + fromY + ")  to " +
                              "(" + toX + "," + toY + ")  is:  \n");

            while (path.Count > 0)
            {
                MatrixNode node = path.Pop();
                Console.WriteLine("(" + node.X + "," + node.Y + ")");
            }
        }

        public static MatrixNode AStarSearch(char[][] matrix, int fromX, int fromY, int toX, int toY)
        {
            // The set of nodes already evaluated
            Dictionary<string, MatrixNode> closedSet = new Dictionary<string, MatrixNode>();

            // The set of currently discovered nodes that are not evaluated yet.
            // Initially, only the start node is known.
            Dictionary<string, MatrixNode> openSet = new Dictionary<string, MatrixNode>();

            MatrixNode startNode = new MatrixNode
            {
                X = fromX,
                Y = fromY
            };

            string key = startNode.X + startNode.X.ToString();
            openSet.Add(key, startNode);

            // ReSharper disable once ConvertToLocalFunction
            Func<KeyValuePair<string, MatrixNode>> smallestInOpenSet = () =>
            {
                KeyValuePair<string, MatrixNode> smallest = openSet.ElementAt(0);

                foreach (var item in openSet)
                {
                    if (item.Value.Sum < smallest.Value.Sum)
                    {
                        smallest = item;
                    }
                    else if (item.Value.Sum == smallest.Value.Sum && item.Value.To < smallest.Value.To)
                    {
                        smallest = item;
                    }
                }

                return smallest;
            };

            // add these values to current node's x and y values to get the left/up/right/bottom neighbors
            List<KeyValuePair<int, int>> fourNeighbors = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(-1, 0),
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 0),
                new KeyValuePair<int, int>(0, -1)
            };

            int maxX = matrix.GetLength(0);

            if (maxX == 0)
            {
                return null;
            }

            int maxY = matrix[0].Length;

            // while openSet is not empty
            while (true)
            {
                if (openSet.Count == 0)
                {
                    return null;
                }

                // current := the node in openSet having the lowest fScore[] value
                // if current = goal
                //  return reconstruct_path(cameFrom, current)
                KeyValuePair<string, MatrixNode> current = smallestInOpenSet();
                if (current.Value.X == toX && current.Value.Y == toY)
                {
                    return current.Value;
                }

                openSet.Remove(current.Key);
                closedSet.Add(current.Key, current.Value);

                foreach (var plusXy in fourNeighbors)
                {
                    int nbrX = current.Value.X + plusXy.Key;
                    int nbrY = current.Value.Y + plusXy.Value;
                    string nbrKey = nbrX + nbrY.ToString();
                    if (nbrX < 0 || nbrY < 0 || nbrX >= maxX || nbrY >= maxY
                        || matrix[nbrX][nbrY] == 'X' //obstacles marked by 'X'
                        || closedSet.ContainsKey(nbrKey))
                        continue;

                    if (openSet.ContainsKey(nbrKey))
                    {
                        MatrixNode curNbr = openSet[nbrKey];
                        int from = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
                        if (from < curNbr.Fr)
                        {
                            curNbr.Fr = from;
                            curNbr.Sum = curNbr.Fr + curNbr.To;
                            curNbr.Parent = current.Value;
                        }
                    }
                    else
                    {
                        MatrixNode curNbr = new MatrixNode
                        {
                            X = nbrX,
                            Y = nbrY,
                            Fr = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY),
                            To = Math.Abs(nbrX - toX) + Math.Abs(nbrY - toY)
                        };

                        curNbr.Sum = curNbr.Fr + curNbr.To;
                        curNbr.Parent = current.Value;

                        openSet.Add(nbrKey, curNbr);
                    }
                }
            }
        }
    }

    public class MatrixNode
    {
        public int Fr, To, Sum;
        public int X, Y;
        public MatrixNode Parent;
    }
}