using System;

namespace SinglyLinkedList
{
    class Program
    {
        static void Main()
        {
            // declare and initialize linked list classes
            SinglyLinkedList singlyLinkedList = new SinglyLinkedList();
            SinglyLinkedList singlyLinkedListTwo = new SinglyLinkedList();

            // test, append to front of the first list and print
            Console.WriteLine("Adding to the front of the list:");
            singlyLinkedList.AddToFront("World!");
            singlyLinkedList.AddToFront("Hello");

            singlyLinkedList.PrintAllNodes();

            // test, append to end of the second list and print
            Console.WriteLine("Adding to the end of the list:");
            singlyLinkedListTwo.AddToEnd("Hello");
            singlyLinkedListTwo.AddToEnd("World!");

            singlyLinkedListTwo.PrintAllNodes();
        }
    }
}
