using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Trie
{
    public class Trie
    {
        public struct Letter
        {
            public const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            public static implicit operator Letter(char c)
            {
                return new Letter {Index = Chars.IndexOf(c)};
            }

            public int Index;

            public char ToChar()
            {
                return Chars[Index];
            }

            public override string ToString()
            {
                return Chars[Index].ToString();
            }
        }

        public class Node
        {
            public string Word;
            public bool IsTerminal => Word != null;
            public Dictionary<Letter, Node> Edges = new Dictionary<Letter, Node>();
        }

        public Node Root = new Node();

        public Trie(string[] words)
        {
            for (var w = 0; w < words.Length; w++)
            {
                var word = words[w];
                var node = Root;
                for (var len = 1; len <= word.Length; len++)
                {
                    var letter = word[len - 1];
                    Node next;
                    if (!node.Edges.TryGetValue(letter, out next))
                    {
                        next = new Node();
                        if (len == word.Length)
                            next.Word = word;
                        node.Edges.Add(letter, next);
                    }
                    node = next;
                }
            }
        }

        /// <summary>
        ///     Utility function
        /// </summary>
        /// <param name="n"></param>
        /// <param name="sets"></param>
        /// <param name="currentArrayIndex"></param>
        /// <param name="wordsFound"></param>
        private static void GenWords(Node n, HashSet<Letter>[] sets, int currentArrayIndex, List<string> wordsFound)
        {
            if (currentArrayIndex < sets.Length)
                foreach (var edge in n.Edges)
                    if (sets[currentArrayIndex].Contains(edge.Key))
                    {
                        if (edge.Value.IsTerminal)
                            wordsFound.Add(edge.Value.Word);
                        GenWords(edge.Value, sets, currentArrayIndex + 1, wordsFound);
                    }
        }

        private static void Main()
        {
            const int minArraySize = 3;
            const int maxArraySize = 4;
            const int setCount = 10;

            var trie = new Trie(File.ReadAllLines("sowpods.txt"));
            var watch = new Stopwatch();
            var trials = 10000;
            var wordCountSum = 0;
            var rand = new Random(37);

            for (var t = 0; t < trials; t++)
            {
                var sets = new HashSet<Letter>[setCount];
                for (var i = 0; i < setCount; i++)
                {
                    sets[i] = new HashSet<Letter>();
                    var size = minArraySize + rand.Next(maxArraySize - minArraySize + 1);
                    while (sets[i].Count < size)
                        sets[i].Add(Letter.Chars[rand.Next(Letter.Chars.Length)]);
                }

                watch.Start();
                var wordsFound = new List<string>();
                for (var i = 0; i < sets.Length - 1; i++)
                    GenWords(trie.Root, sets, i, wordsFound);
                foreach (var word in wordsFound)
                    Console.WriteLine(word);
                watch.Stop();
                wordCountSum += wordsFound.Count;
            }

            Console.WriteLine("Elapsed per trial = {0}", new TimeSpan(watch.Elapsed.Ticks / trials));
            Console.WriteLine("Average word count per trial = {0:0.0}", (float) wordCountSum / trials);
        }
    }
}
