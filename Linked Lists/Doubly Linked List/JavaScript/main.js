"use strict";

// bi-directional movement
function Node(data) {
	this.data = data;
	this.previous = null;
	this.next = null;
}

// has a reference to both beginning and end of list
function DoublyList() {
	this._length = 0;
	this.head = null;
	this.tail = null;
}

// Structure Methods

DoublyList.prototype.add = function(value) {
	var node = new Node(value);

	// if empty list, assign to its head and tail the
	// node being added, if it contains nodes, find the tail
	// and assign it to tail.next
	if (this._length) {
		this.tail.next = node;
		node.previous = this.tail;
		this.tail = node;
	} else {	
		this.head = node;
		this.tail = node;
	}

	this._length++;

	// logging
	console.log("New node added: ", node);
	console.log("Length: ", this._length);

	return node;
};

DoublyList.prototype.searchNodeAt = function(position) {
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

DoublyList.prototype.remove = function(position) {
	var currentNode = this.head,
			length = this._length,
			count = 1,
			message = {failure: 'Failure: non-existent node in this list...',
								 success: 'Node removed!'},
			beforeNodeToDelete = null,
			afterNodeToDelete = null,
			nodeToDelete = null,
			deletedNode = null;

	// logging
	console.log("Removing Node at position ", position);

	// invalid position
	if (length === 0 || position < 1 || position > length) {
		throw new Error(message.failure);
	}

	// first node is removed
	if (position === 1) {
		this.head = currentNode.next;

		// if there is a second node
		if (!this.head) {
			this.head.previous = null;
		} else {
			this.tail = null;
		}
	} else if (position == this._length) {	// last node is removed
		this.tail = this.tail.previous;
		this.tail.next = null;
	} else {	//a middle node is removed
		while (count < position) {
			currentNode = currentNode.next;
			count++;
		}

		beforeNodeToDelete = currentNode.previous;
		nodeToDelete = currentNode;
		afterNodeToDelete = currentNode.next;

		beforeNodeToDelete.next = afterNodeToDelete;
		afterNodeToDelete.previous = beforeNodeToDelete;
		deletedNode = nodeToDelete;
		nodeToDelete = null;
	}

	this._length--;

	// logging
	console.log("Removed Node ", deletedNode);

	return message.success;
};

// Usage

var newList = new DoublyList();
newList.add(1);
newList.add(2);
newList.add(3);

newList.searchNodeAt(3);

newList.remove(2);