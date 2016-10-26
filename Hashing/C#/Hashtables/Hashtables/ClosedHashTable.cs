using System;

namespace Hashtables
{
    public class ClosedHashTable
    {
        /// <summary>
        /// HashEntry class, used for storing key/data pair
        /// </summary>
        public class HashEntry
        {
            private readonly int _key;
            private readonly string _data;

            public HashEntry(int key, string data)
            {
                this._key = key;
                this._data = data;
            }

            public int GetKey()
            {
                return _key;
            }

            public string GetData()
            {
                return _data;
            }
        }

        private readonly int _maxSize;
        private readonly HashEntry[] _table;

        public ClosedHashTable(int value)
        {
            _maxSize = value;
            _table = new HashEntry[_maxSize];
            for (int i = 0; i < _maxSize; i++)
            {
                _table[i] = null;
            }
        }

        /// <summary>
        /// HashTable retrieval method
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Retrieve(int key)
        {
            int hash = key%_maxSize;

            while (_table[hash] != null && _table[hash].GetKey() != key)
            {
                hash = (hash + 1)%_maxSize;
            }

            if (_table[hash] == null)
            {
                return "Nothing found!";
            }

            return _table[hash].GetData();
        }

        /// <summary>
        /// HashTable insertion method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Insert(int key, string data)
        {
            if (!CheckOpenSpace()) // if no open spaces available
            {
                Console.WriteLine("Table is at full capacity!");
            }

            int hash = (key%_maxSize);

            while (_table[hash] != null && _table[hash].GetKey() != key)
            {
                hash = (hash + 1)%_maxSize;
            }

            _table[hash] = new HashEntry(key, data);
        }

        /// <summary>
        /// Checks for open spaces in table
        /// </summary>
        /// <returns></returns>
        private bool CheckOpenSpace()
        {
            bool isOpen = false;

            for (int i = 0; i < _maxSize; i++)
            {
                if (_table[i] == null)
                {
                    isOpen = true;
                }
            }

            return isOpen;
        }

        /// <summary>
        /// HashTable removal method
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(int key)
        {
            int hash = key%_maxSize;

            while (_table[hash] != null && _table[hash].GetKey() != key)
            {
                hash = (hash + 1)%_maxSize;
            }

            if (_table[hash] == null)
            {
                return false;
            }

            Console.WriteLine("Removing key {0} from table", key);
            _table[hash] = null;
            return true;
        }

        public void Print()
        {
            Console.WriteLine("------------------------------- Printing table -------------------------------");
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i] == null && i <= _maxSize)
                {
                    continue;
                }

                var hashEntry = _table[i];
                if (hashEntry != null) Console.WriteLine("Key - {0}, Data - {1}", hashEntry.GetKey(), hashEntry.GetData());
            }
        }

        /// <summary>
        /// Quadratic probing method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void QuadraticHashInsert(int key, string data)
        {
            if (!CheckOpenSpace())
            {
                Console.WriteLine("Table is at full capactiy");
            }

            int j = 0;
            int hash = key%_maxSize;
            while (_table[hash] != null && _table[hash].GetKey() != key)
            {
                j++;
                hash = (hash + j*j)%_maxSize;
            }

            if (_table[hash] == null)
            {
                Console.WriteLine("Inserting using quadratic probing");
                _table[hash] = new HashEntry(key,data);
            }
        }

        /// <summary>
        /// Double probing method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void DoubleHashInsert(int key, string data)
        {
            if (!CheckOpenSpace())
            {
                Console.WriteLine("Table is at full capacity");
            }

            // double probing method
            int hashValue = HashOne(key);
            int stepSize = HashTwo(key);

            while (_table[hashValue] != null && _table[hashValue].GetKey() != key)
            {
                hashValue = (hashValue + stepSize*HashTwo(key))%_maxSize;
            }

            Console.WriteLine("Inserting using doubling probing");
            _table[hashValue] = new HashEntry(key, data);
        }

        private int HashOne(int key)
        {
            return key%_maxSize;
        }

        private int HashTwo(int key)
        {
            return 5 - key%5;
        }
    }
}