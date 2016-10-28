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
            closedHashTable.Insert(0, "Zero");
            closedHashTable.Insert(1, "One");

            closedHashTable.QuadraticHashInsert(2, "Two");
            closedHashTable.QuadraticHashInsert(0, "Three");

            closedHashTable.DoubleHashInsert(4, "Four");
            closedHashTable.DoubleHashInsert(1, "Five");

            // print to console
            closedHashTable.Print();

            // remove from table, print
            closedHashTable.Remove(0);
            closedHashTable.Remove(1);

            closedHashTable.Print();

            // retrieve particular key value
            Console.WriteLine(closedHashTable.Retrieve(2));

            // init open hash table
            OpenHashTable openHashTable = new OpenHashTable(100);

            // insertion
            openHashTable.Insert(0, "Zero");
            openHashTable.Insert(1, "One");

            // print to console
            openHashTable.Print();

            // init 
            FixedSizeGenericHashTable<string, string> fixedSizeGenericHashTable = new FixedSizeGenericHashTable<string, string>(100);

            fixedSizeGenericHashTable.Add("1", "item 1");
            fixedSizeGenericHashTable.Add("2", "item 2");
            fixedSizeGenericHashTable.Add("dsfdsdsd", "sadsadsadsad");

            fixedSizeGenericHashTable.Find("1");
            fixedSizeGenericHashTable.Find("2");
            fixedSizeGenericHashTable.Find("dsfdsdsd");

            fixedSizeGenericHashTable.Remove("1");

            fixedSizeGenericHashTable.Find("1");
        }
    }
}
