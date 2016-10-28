using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Hashtables
{
    public struct KeyValue<TK, TV>
    {
        public TK Key { get; set; }
        public TV Value { get; set; }
    }

    public class FixedSizeGenericHashTable<TK, TV>
    {
        private readonly int _size;
        private readonly LinkedList<KeyValue<TK, TV>>[] _items;

        public FixedSizeGenericHashTable(int size)
        {
            this._size = size;
            this._items = new LinkedList<KeyValue<TK, TV>>[size];

            Console.WriteLine("Constructed FixedSizeGenericHashTable!");
        }

        protected int GetArrayPosition(TK key)
        {
            int position = key.GetHashCode() % _size;
            return Math.Abs(position);
        }

        public TV Find(TK key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<TK, TV>> linkedList = GetLinkedList(position);

            foreach (KeyValue<TK, TV> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    var foundItem = item.Value;

                    Console.WriteLine("Found: {0}", foundItem);

                    return foundItem;
                }
            }

            Console.WriteLine("Unable to find value for key {0}!", key);
            return default(TV);
        }

        public void Add(TK key, TV value)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<TK, TV>> linkedList = GetLinkedList(position);
            KeyValue<TK, TV> item = new KeyValue<TK, TV>()
            {
                Key = key,
                Value = value
            };

            linkedList.AddLast(item);
        }

        public void Remove(TK key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<TK, TV>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            KeyValue<TK, TV> foundItem = default(KeyValue<TK, TV>);
            foreach (KeyValue<TK, TV> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            if (itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }

        private LinkedList<KeyValue<TK, TV>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<TK, TV>> linkedList = _items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<TK, TV>>();
                _items[position] = linkedList;
            }

            return linkedList;
        }
    }
}
