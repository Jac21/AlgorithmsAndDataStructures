using System;
using System.Collections.Generic;
using System.Linq;

// https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
// https://msdn.microsoft.com/en-us/library/018hxwa8(v=vs.110).aspx
namespace Tree
{
    public class TreeNode<T>
    {
        // class fields
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        // set tree init value
        public TreeNode(T value)
        {
            this._value = value;
        }

        // set tree getter
        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        // tree parent value
        public TreeNode<T> Parent { get; private set; }

        // tree Value object, derived from init value
        public T Value { get { return _value; } }

        // set tree children structure
        public IReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        /// <summary>
        /// Intakes tree node value, creates new TreeNode with parent value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value)
            {
                Parent = this
            };

            _children.Add(node);

            return node;
        }

        /// <summary>
        /// Array based use of Add
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        /// <summary>
        /// Insert child node value below specified parent value
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeNode<T> InsertChild(TreeNode<T> parent, T value)
        {
            var node = new TreeNode<T>(value)
            {
                Parent = parent
            };

            parent._children.Add(node);

            return node;
        }  

        /// <summary>
        /// Removes specified node from tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        /// <summary>
        /// Traverse through tree, intakes action delegate to
        /// perform on each node
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
            {
                child.Traverse(action);
            }
        }

        /// <summary>
        /// Flatten tree values, removing duplicate values from structure
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Flatten()
        {
            return new[] {Value}.Union(_children.SelectMany(x => x.Flatten()));
        } 
    }
}
