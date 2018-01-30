using System;

namespace RadixSort
{
    public class Program
    {
        /// <summary>
        /// https://en.wikibooks.org/wiki/Algorithm_Implementation/Sorting/Radix_sort
        /// </summary>
        /// <param name="a"></param>
        public static void RadixSort(int[] a)
        {
            // helper array
            int[] helperArray = new int[a.Length];

            // number of bits our group will be long
            int groupBits = 4;

            // number of bits in a C# int
            const int cSharpIntBits = 32;

            // counting and prefix arrays
            // (note: dimensions 2^r which is the number of all possible values of a r-bit number)
            int[] count = new int[1 << groupBits];
            int[] pref = new int[1 << groupBits];

            // number of groups
            int groups = (int) Math.Ceiling(cSharpIntBits / (double) groupBits);

            // the mask to identify groups
            int mask = (1 << groupBits) - 1;

            // the algorithm
            for (int c = 0, shift = 0; c < groups; c++, shift += groupBits)
            {
                // reset count array
                for (int j = 0; j < count.Length; j++)
                {
                    count[j] = 0;
                }

                // counting elements of the c-th group 
                for (int i = 0; i < a.Length; i++)
                {
                    count[(a[i] >> shift) & mask]++;
                }

                // calculating prefixes
                pref[0] = 0;
                for (int i = 1; i < count.Length; i++)
                {
                    pref[i] = pref[i - 1] + count[i - 1];
                }

                // from a[] to t[] elements ordered by the c-th group
                for (int i = 0; i < a.Length; i++)
                {
                    helperArray[pref[(a[i] >> shift) & mask]++] = a[i];
                }

                // a[] = t[] and start again until the last group
                helperArray.CopyTo(a, 0);
            }

            // a is sorted
        }

        static void Main()
        {
            // Testing
            int[] a = {1, 8, 4, 3, 8, 6, 2, 2, 7};

            RadixSort(a);

            foreach (var sorted in a)
            {
                Console.WriteLine(sorted);
            }

            Console.ReadLine();
        }
    }
}