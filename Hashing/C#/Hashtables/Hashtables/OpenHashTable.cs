using System;

namespace Hashtables
{
    public class OpenHashTable
    {
        class HashNode
        {
            private readonly int _key;
            private readonly string _data;
            private HashNode _next;

            public HashNode(int key, string data)
            {
                this._key = key;
                this._data = data;
                _next = null;
            }

            public int GetKey()
            {
                return _key;
            }

            public string GetData()
            {
                return _data;
            }

            public void SetNextNode(HashNode obj)
            {
                _next = obj;
            }

            public void DeleteNode()
            {
                this._next = null;
            }

            public HashNode GetNextNode()
            {
                return this._next;
            }
        }

        private readonly HashNode[] _table;
        private readonly int _size;

        public OpenHashTable(int value)
        {
            _size = value;

            _table = new HashNode[_size];
            for (int i = 0; i < _size; i++)
            {
                _table[i] = null;
            }

            Console.WriteLine("Constructed OpenHashTable!");
        }

        public void Insert(int key, string data)
        {
            HashNode nObject = new HashNode(key,data);

            int hash = key%_size;

            while (_table[hash] != null && _table[hash].GetKey() % 10 != key % 10)
            {
                hash = (hash + 1)%_size;
            }

            if (_table[hash] != null && hash == _table[hash].GetKey()%10)
            {
                nObject.SetNextNode(_table[hash].GetNextNode());
                _table[hash].SetNextNode(nObject);
                return;
            }
            _table[hash] = nObject;
        }

        public string Retrieve(int key)
        {
            int hash = key%_size;

            while (_table[hash] != null && _table[hash].GetKey() % 10 != key % 10)
            {
                hash = (hash + 1)%_size;
            }

            HashNode current = _table[hash];

            while (current!= null && current.GetKey() != key && current.GetNextNode() != null)
            {
                current = current.GetNextNode();
            }

            if (current != null && current.GetKey() == key)
            {
                return current.GetData();
            }
            return "Nothing found!";
        }

        public void Remove(int key)
        {
            int hash = key % _size;
            while (_table[hash] != null && _table[hash].GetKey() % 10 != key % 10)
            {
                hash = (hash + 1) % _size;
            }
            HashNode current = _table[hash];
            while (current != null && (current.GetNextNode().GetKey() != key && current.GetNextNode() != null))
            {
                current = current.GetNextNode();
            }
            if (current!= null && current.GetNextNode().GetKey() == key)
            {
                current.DeleteNode();
                Console.WriteLine("entry removed successfully!");
            }
            else
            {
                Console.WriteLine("nothing found to delete!");
            }
        }

        public void Print()
        {
            Console.WriteLine("------------------------------- Printing table -------------------------------");
            for (int i = 0; i < _size; i++)
            {
                var current = _table[i];
                while (current != null)
                {
                    Console.WriteLine("Key - {0}, Data - {1}",current.GetKey(), current.GetData());
                    current = current.GetNextNode();
                }
            }
        }
    }
}
