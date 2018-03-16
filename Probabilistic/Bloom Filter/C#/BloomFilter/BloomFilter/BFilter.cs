using System;
using System.Collections;
using System.Linq;

namespace BloomFilter
{
    /// <summary>
    /// Main Bloom Filter Class, with generic item T type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BFilter<T>
    {
        // class fields
        private readonly int _hashFunctionCount;
        private readonly BitArray _hashBits;
        private readonly HashFunction _getHashSecondary;

        /// <summary>
        /// Instantiates a new Bloom Filter with specific error rate using the optimal size for the structure,
        /// intakes number of items to be added to the filter
        /// </summary>
        /// <param name="capacity"></param>
        public BFilter(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Intakes number of items to add to the Bloom Filter, as well as acceptable rate of false positives
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="errorRate"></param>
        public BFilter(int capacity, float errorRate) : this(capacity, errorRate, null) { }

        /// <summary>
        /// Intakes same as above, as well as the function used to hash those values
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="hashFunction"></param>
        public BFilter(int capacity, HashFunction hashFunction)
            : this(capacity, BestErrorRate(capacity), hashFunction) { }

        /// <summary>
        /// Bloom Filter Constructor
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="errorRate"></param>
        /// <param name="hashFunction"></param>
        public BFilter(int capacity, float errorRate, HashFunction hashFunction)
			: this(capacity, errorRate, hashFunction, BestM(capacity, errorRate), BestK(capacity, errorRate)) { }

        /// <summary>
        /// Creates a new Bloom Filter, m being the number of elements in the array and k being the 
        /// number of hash functions to utilize
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="errorRate"></param>
        /// <param name="hashFunction"></param>
        /// <param name="m"></param>
        /// <param name="k"></param>
        public BFilter(int capacity, float errorRate, HashFunction hashFunction, int m, int k)
        {
            // params range validation
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(String.Format("Capacity provided: {0} must be greater than 0", capacity));
            } 
            else if (errorRate >= 1 || errorRate <= 0)
            {
                throw new ArgumentOutOfRangeException(String.Format("Error rate provided: {0} must be between 0 and 1", errorRate));
            } 
            else if (m < 1)
            {
                throw new ArgumentOutOfRangeException(String.Format("The provided capacity and errorRate values would result in " +
                                                                    "an array of length > int.MaxValue. Please reduce either of " +
                                                                    "these values. Capacity: {0}, Error rate: {1}", capacity, errorRate));
            }

            // Set secondary hash function based on type T
            if (hashFunction == null)
            {
                if (typeof (T) == typeof (string))
                {
                    this._getHashSecondary = HashString;
                }
                else if (typeof (T) == typeof (int))
                {
                    this._getHashSecondary = HashInt32;
                }
                else
                {
                    throw new ArgumentNullException("Please provide a hash function for type T");
                }
            }
            else
            {
                this._getHashSecondary = hashFunction;
            }

            this._hashFunctionCount = k;
            this._hashBits = new BitArray(m);
        }

        public delegate int HashFunction(T input);

        /// <summary>
        /// The ratio of false to true bits in the filter. E.g., 1 true bit in a 10 bit filter means a truthiness of 0.1.
        /// </summary>
        public double Truthiness
        {
            get { return (double)this.TrueBits() / this._hashBits.Count; }
        }

        /// <summary>
        /// Iterate through hash bits, add to output based on each true bit in total
        /// </summary>
        /// <returns></returns>
        private int TrueBits()
        {
            return this._hashBits.Cast<bool>().Count(bit => bit);
        }

        /// <summary>
        /// Add a new T item to the instantiated filter
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            // flip bits for each hash of the item
            int primaryHash = item.GetHashCode();
            int secondaryHash = this._getHashSecondary(item);
            for (int i = 0; i < this._hashFunctionCount; i++)
            {
                int hash = this.ComputeHash(primaryHash, secondaryHash, i);
                this._hashBits[hash] = true;
            }
            Console.WriteLine("Item \"{0}\" added to filter", item);
        }

        /// <summary>
        /// Check for existence of given T item in the Bloom Filter
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            int primaryHash = item.GetHashCode();
            int secondaryHash = this._getHashSecondary(item);
            for (int i = 0; i < this._hashFunctionCount; i++)
            {
                int hash = this.ComputeHash(primaryHash, secondaryHash, i);
                if (this._hashBits[hash] == false)
                {
                    Console.WriteLine("Item \"{0}\" is not contained within the filter, false", item);
                    return false;
                }
            }
            Console.WriteLine("Item \"{0}\" is contained within the filter, true", item);
            return true;
        }

        /// <summary>
        /// The best k value
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="errorRate"></param>
        /// <returns></returns>
        private static int BestK(int capacity, float errorRate)
        {
            return (int) Math.Round(Math.Log(2.0)*BestM(capacity, errorRate)/capacity);
        }

        /// <summary>
        /// The best m value
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="errorRate"></param>
        /// <returns></returns>
        private static int BestM(int capacity, float errorRate)
        {
            return (int) Math.Ceiling(capacity*Math.Log(errorRate, (1.0/Math.Pow(2, Math.Log(2.0)))));
        }

        /// <summary>
        /// The best error rate value
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        private static float BestErrorRate(int capacity)
        {
            float c = (float) (1.0/capacity);
            if (c != 0)
            {
                return c;
            }

            return (float) Math.Pow(0.6185, int.MaxValue/capacity);
        }

        /// <summary>
        /// Hashes a 32-bit signed int using Thomas Wang's method v3.1 (http://www.concentric.net/~Ttwang/tech/inthash.htm).
        /// Runtime is suggested to be 11 cycles. 
        /// </summary>
        /// <param name="input">The integer to hash.</param>
        /// <returns>The hashed result.</returns>
        private static int HashInt32(T input)
        {
            uint? x = input as uint?;
            unchecked
            {
                x = ~x + (x << 15); // x = (x << 15) - x- 1, as (~x) + y is equivalent to y - x - 1 in two's complement representation
                x = x ^ (x >> 12);
                x = x + (x << 2);
                x = x ^ (x >> 4);
                x = x * 2057; // x = (x + (x << 3)) + (x<< 11);
                x = x ^ (x >> 16);
                return (int)x;
            }
        }

        /// <summary>
        /// Hashes a string using Bob Jenkin's "One At A Time" method from Dr. Dobbs (http://burtleburtle.net/bob/hash/doobs.html).
        /// Runtime is suggested to be 9x+9, where x = input.Length. 
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <returns>The hashed result.</returns>
        private static int HashString(T input)
        {
            string s = input as string;
            int hash = 0;

            for (int i = 0; i < s.Length; i++)
            {
                hash += s[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        /// <summary>
        /// Performs Dillinger and Manolios double hashing. 
        /// </summary>
        /// <param name="primaryHash"></param>
        /// <param name="secondaryHash"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int ComputeHash(int primaryHash, int secondaryHash, int i)
        {
            int resultingHash = (primaryHash + (i*secondaryHash))%this._hashBits.Count;
            return Math.Abs(resultingHash);
        }
    }
}
