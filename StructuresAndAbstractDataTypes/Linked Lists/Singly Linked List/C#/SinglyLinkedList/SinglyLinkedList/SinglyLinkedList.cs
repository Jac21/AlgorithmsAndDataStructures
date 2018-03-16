using System;

namespace SinglyLinkedList
{
    /// <summary>
    /// Node class, to be used in Linked List class
    /// </summary>
    public class Node
    {
        // class fields
        public Node Next;
        public Object Data;
    }

    /// <summary>
    /// Singly Linked List class, with append, pre-pend, 
    /// and print methods
    /// </summary>
    public class SinglyLinkedList
    {
        // class fields
        private Node _headNode;

        /// <summary>
        /// Add to the front of the linked list structure
        /// </summary>
        /// <param name="data"></param>
        public void AddToFront(Object data)
        {
            // create new node that is added,
            // set data and Next node as headNode
            Node toAdd = new Node
            {
                Data = data,
                Next = this._headNode
            };

            // Set added node as head
            this._headNode = toAdd;
        }

        /// <summary>
        /// Append node to the end of the linked list
        /// </summary>
        /// <param name="data"></param>
        public void AddToEnd(Object data)
        {
            // Null head node case
            if (this._headNode == null)
            {
                // initialize head node, set data and next node
                _headNode = new Node();

                this._headNode.Data = data;
                this._headNode.Next = null;
            }
            else    // head node is not null
            {
                // initialize new node, set data
                Node toAdd = new Node()
                {
                    Data = data
                };

                // set new head node, iterate
                Node current = this._headNode;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toAdd;
            }
        }

        /// <summary>
        /// Iterate through all the nodes in the list
        /// and print each one's data to the console
        /// </summary>
        public void PrintAllNodes()
        {
            // set new node to head node
            Node current = this._headNode;

            // as long as current node is not null,
            while (current != null)
            {
                // write current node's data to console
                Console.WriteLine(current.Data);

                // set current node to next node
                current = current.Next;
            }
        }
    }
}
