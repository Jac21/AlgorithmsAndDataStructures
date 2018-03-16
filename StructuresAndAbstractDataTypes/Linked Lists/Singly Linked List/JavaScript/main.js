"use strict";

function Node(data) {
	this.data = data;
	this.next = null;
}

function SinglyList() {
	this._length = 0;
	this.head = null;
}

// Structure Methods

SinglyList.prototype.add = function(value) {
	// create new instance of a Node, initialize currentNode as head of the list
	var node = new Node(value),
		currentNode = this.head;

	// empty list case
	if (!currentNode) {
		this.head = node;
		this._length++;

		console.log("New list created with: ", node);
		return node;
	}

	// non-empty list
	while(currentNode.next) {
		currentNode = currentNode.next;
	}

	currentNode.next = node;

	this._length++;

	// logging
	console.log("New node added: ", node);
	console.log("Length: ", this._length);

	return node;
};

SinglyList.prototype.searchNodeAt = function(position) {
	var currentNode = this.head,
			length = this._length,
			count = 1,
			message = {failure: 'Failure: non-existent node in this list...'};

	// logging
	console.log("Searching for Node at position ", position);

	// first use-case: an invalid position
	if (length === 0 || position < 1 || position > length) {
		throw new Error(message.failure);
	}

	// valid position
	while(count < position) {
		currentNode = currentNode.next;
		count++;
	}

	// logging
	console.log("Node found: ", currentNode);

	return currentNode;
};

SinglyList.prototype.remove = function(position) {
	var currentNode = this.head,
			length = this._length,
			count = 0,
			message = {failure: 'Failure: non-existent node in this list...'},
			beforeNodeToDelete = null,
			nodeToDelete = null,
			deletedNode = null;

	// logging
	console.log("Removing Node at position ", position);

	// invalid position
	if (position < 0 || position > length) {
		throw new Error(message.failure);
	}

	// first node is removed
	if (position === 1) {
		this.head = currentNode.next;
		deletedNode = currentNode;
		currentNode = null;
		this._length--;

		return deletedNode;
	}

	// any other node is removed
	while (count < position) {
		beforeNodeToDelete = currentNode;
		nodeToDelete = currentNode.next;
		count++;
	}

	beforeNodeToDelete.next = nodeToDelete.next;
	deletedNode = nodeToDelete;
	nodeToDelete = null;
	this._length--;

	// logging
	console.log("Removed Node ", deletedNode);

	return deletedNode;
};

// Usage

var newList = new SinglyList();
newList.add(1);
newList.add(2);
newList.add(3);

newList.searchNodeAt(3);

newList.remove(2);