"use strict";

// implementation
function Node(data) {
	this.data = data;
	this.parent = null;
	this.children = [];
}

function Tree(data) {
	var node = new Node(data);
	this._root = node;
}

// Structure Methods
Tree.prototype.traverseDF = function(callback) {
	// this is a recurse and immediately-invoking function
	(function recurse(currentNode) {
		// step 2
		for (var i = 0, length = currentNode.children.length; i < length; i++) {
			// step 3
			recurse(currentNode.children[i]);
		}

		// step 4
		callback(currentNode);

		// step 1
	})(this._root);
};

Tree.prototype.traverseBF = function(callback) {
	var queue = new Queue();

	queue.enqueue(this._root);

	var currentNode = queue.dequeue();

	while(currentNode) {
		for (var i = 0, length = currentNode.children.length; i < length; i++) {
			queue.enqueue(currentNode.children[i]);
		}

		callback(currentNode);
		currentNode = queue.dequeue();
	}
};

Tree.prototype.contains = function(callback, traversal) {
	traversal.call(this, callback);
};

Tree.prototype.add = function(data, toData /*used to compare against every node in a tree*/, traversal /*type of tree traversal*/) {
	var child = new Node(data),	// create new instance of Node
			parent = null,					// point to any node in a tree that matches the value of toData
			callback = function(node) {	// compares toData to every node's data property, if true
				if (node.data === toData) {	// then parent is assigned to the node that matches the comparison
					parent = node;
				}
			};

			this.contains(callback, traversal); // actual comparison

			if (parent) {
				parent.children.push(child);
				child.parent = parent;

				// logging
				console.log("Added \'" + child.data + "\'' to tree");
			} else {
				throw new Error('Cannot add to a non-existent parent...');
			}
};

Tree.prototype.remove = function(data, fromData, traversal) {
	var tree = this,
			parent = null,
			childToRemove = null,
			index;

	var callback = function(node) {
		if (node.data === fromData) {
			parent = node;
		}
	};

	this.contains(callback, traversal);

	if (parent) {
    index = findIndex(parent.children, data);

    if (index === undefined) {
        throw new Error('Node to remove does not exist.');
	  } else {
	      childToRemove = parent.children.splice(index, 1);
	  }
  } else {
      throw new Error('Parent does not exist.');
  }
 
 console.log("Removed: ", childToRemove);
 return childToRemove;
}

function findIndex(arr, data) {
  var index;

  for (var i = 0; i < arr.length; i++) {
      if (arr[i].data === data) {
          index = i;
      }
  }

  return index;
}

// usage

var tree = new Tree('one');
 
tree._root.children.push(new Node('two'));
tree._root.children[0].parent = tree;
 
tree._root.children.push(new Node('three'));
tree._root.children[1].parent = tree;
 
tree._root.children.push(new Node('four'));
tree._root.children[2].parent = tree;
 
tree._root.children[0].children.push(new Node('five'));
tree._root.children[0].children[0].parent = tree._root.children[0];
 
tree._root.children[0].children.push(new Node('six'));
tree._root.children[0].children[1].parent = tree._root.children[0];
 
tree._root.children[2].children.push(new Node('seven'));
tree._root.children[2].children[0].parent = tree._root.children[2];

/*
 
creates this tree
 
 one
 ├── two
 │   ├── five
 │   └── six
 ├── three
 └── four
     └── seven
 
*/

tree.traverseDF(function(node) {
	console.log(node.data);
});

/*
 
logs the following strings to the console
 
'five'
'six'
'two'
'three'
'seven'
'four'
'one'
 
*/

tree.traverseBF(function(node) {
	console.log(node.data);
});

/*
 
logs the following strings to the console
 
'one'
'two'
'three'
'four'
'five'
'six'
'seven'
 
*/

tree.contains(function(node) {
	if (node.data === "two") {
		console.log("Contains: ", node);
	}
}, tree.traverseBF);

var newTree = new Tree('CEO');

newTree.add('VP', 'CEO', tree.traverseBF);
newTree.remove('VP', 'CEO', tree.traverseBF);