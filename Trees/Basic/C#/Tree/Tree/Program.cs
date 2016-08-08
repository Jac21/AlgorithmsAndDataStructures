using System;

namespace Tree
{
    class Program
    {
        static void Main()
        {
            // declara and initialize tree class with starting character
            char treeInitializerCharacter = 'A';
            TreeNode<char> tree = new TreeNode<char>(treeInitializerCharacter);

            // add character children nodes
            tree.AddChild('B');
            var cNode = tree.AddChild('C');
            var dNode = tree.AddChild('D');
            tree.AddChild('A');

            char[] charArray = {
                'F',
                'G'
            };

            tree.AddChildren(charArray);

            tree.InsertChild(dNode, 'E');

            // removal
            tree.RemoveChild(cNode);

            // creation action method group to print nodes to console on traversal
            Action<char> printAction = Console.WriteLine;
            tree.Traverse(printAction);

            Console.WriteLine("---------------------------------------------------");

            // flatten
            var flatTree = tree.Flatten();
            foreach (var node in flatTree)
            {
                Console.WriteLine(node);
            }
        }
    }
}
