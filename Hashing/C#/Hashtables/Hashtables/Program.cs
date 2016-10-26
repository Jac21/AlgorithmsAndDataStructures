using System;

namespace Hashtables
{
    class Program
    {
        static void Main()
        {
            // init closed hashtable class
            ClosedHashTable closedHashTable = new ClosedHashTable(100);

            // insert using various methods
            closedHashTable.Insert(0, "One");
            closedHashTable.Insert(1, "Two");

            closedHashTable.QuadraticHashInsert(2, "Three");
            closedHashTable.QuadraticHashInsert(0, "Four");

            closedHashTable.DoubleHashInsert(4, "Five");
            closedHashTable.DoubleHashInsert(1, "Six");

            // print to console
            closedHashTable.Print();

            // remove from table, print
            closedHashTable.Remove(0);
            closedHashTable.Remove(1);

            closedHashTable.Print();

            // retrieve particular key value
            Console.WriteLine(closedHashTable.Retrieve(2));
        }
    }
}
