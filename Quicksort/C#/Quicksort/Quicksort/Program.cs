using System;
using System.Collections.Generic;

namespace Quicksort
{
    class Program
    {
        static void Main()
        {
            // class declaration and initialization
            QSorter sorter = new QSorter();

            // some small objects to test on
            IEnumerable<IComparable> integerList = new List<IComparable>()
            {
                1,
                5,
                6,
                8,
                2
            };

            IEnumerable<IComparable> charList = new List<IComparable>()
            {
                "A",
                "B",
                "Z",
                "X",
                "H",
                "G",
                "C"
            };

            // go go go 
            var newIntegerList = sorter.QSort(integerList);

            foreach (var integer in newIntegerList)
            {
                Console.WriteLine(integer);
            }

            var newCharList = sorter.QSort(charList);

            foreach (var charater in newCharList)
            {
                Console.WriteLine(charater);
            }
        }
    }
}
