using System;

namespace LevenshteinDistance
{
    class Program
    {
        static int LevenshteinDistance(string s, string t)
        {
            // set argument string length variables, init 2D string array
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n+1,m+1];

            // base cases
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }

            // set string array size
            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }
            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            // test if string characters match, return overall distance
            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        d[i, j] = d[i - 1, j - 1]; // noop
                    }
                    else
                    {
                        d[i, j] = Math.Min(Math.Min(
                                d[i - 1, j] + 1,    // a deletion
                                d[i, j - 1] + 1),   // an insertion
                            d[i - 1, j - 1] + 1);   // a substitution
                    }
                }
            }

            return d[n, m];
        }

        static void Main()
        {
            string[] argumentStrings = new string[2];

            Console.Write("Enter first string to compare: ");
            argumentStrings[0] = Console.ReadLine();

            Console.Write("Enter second string to compare: ");
            argumentStrings[1] = Console.ReadLine();

            if (argumentStrings.Length == 2)
            {
                Console.WriteLine("{0} -> {1} = {2}",
                    argumentStrings[0], argumentStrings[1], LevenshteinDistance(argumentStrings[0], argumentStrings[1]));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Usage:-\n\nLevenshteinDistance <string1> <string2>");
                Console.ReadLine();
            }
        }
    }
}
