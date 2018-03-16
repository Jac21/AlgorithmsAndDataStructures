using System;
using System.Collections.Generic;

namespace SkipList
{
    public class SkipList
    {
        private class Node
        {
            public Node[] Next { get; }
            public int Value { get; }

            public Node(int value, int level)
            {
                Value = value;
                Next = new Node[level];
            }
        }

        private readonly Node head = new Node(0, 33);
        private readonly Random rand = new Random();
        private int levels = 1;

        /// <summary>
        /// Insert a value into the skip list
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            // Determine level of new node, # of 1-bits before we encounter
            // the first 0-bit is the level, since R is 32-bit, 32 is max level
            int level = 0;
            for (int r = rand.Next(); (r & 1) == 1; r >>= 1)
            {
                level++;
                if (level == levels)
                {
                    levels++;
                    break;
                }
            }

            // Insert this node into the skip list
            Node newNode = new Node(value, level + 1);
            Node cur = head;
            for (int i = levels - 1; i >= 0; i--)
            {
                for(; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if(cur.Next[i].Value > value) break;
                }

                if (i <= level)
                {
                    newNode.Next[i] = cur.Next[i];
                    cur.Next[i] = newNode;
                }
            }
        }

        /// <summary>
        /// Returns whether a particular value exists in the skip list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(int value)
        {
            Node cur = head;
            for (int i = levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value > value) break;
                    if (cur.Next[i].Value == value) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempts to remove particular value from skip list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(int value)
        {
            Node cur = head;

            bool found = false;
            for (int i = levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value == value)
                    {
                        found = true;
                        cur.Next[i] = cur.Next[i].Next[i];
                        break;
                    }

                    if(cur.Next[i].Value > value) break;
                }
            }

            return found;
        }
        
        /// <summary>
        /// Enumerator that iterates over elements in
        /// list in order
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> Enumerate()
        {
            Node cur = head.Next[0];
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }
    }

    class Program
    {
        static void Main()
        {
            SkipList skipList = new SkipList();

            int[] fibs = {1, 1, 2, 3, 5, 8, 13, 21};

            foreach (var fib in fibs)
            {
                skipList.Insert(fib);
            }

            Console.WriteLine(skipList.Contains(8));

            if (skipList.Remove(8))
            {
                Console.WriteLine(skipList.Contains(8));
            }
            else
            {
                Console.WriteLine("Remove failed!");
            }

            foreach (var skipValue in skipList.Enumerate())
            {
                Console.WriteLine("SkipList enumeration: {0}", skipValue);
            }
        }
    }
}
