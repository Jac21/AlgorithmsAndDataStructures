"use strict";

// implementation
function Stack() {
	this._size = 0;
	this._storage = {};
}

// Structure Methods

// every time we add data, increment size of stack,
// as well as retain the order in which it was added
Stack.prototype.push = function(data) {
	// increases the size of our storage
	var size = ++this._size;

	// assign size as a key os storage
	// assign data as the value of this key
	this._storage[size] = data;

	// logging
	console.log(data + " pushed to stack at point " + size);
};

// use a stack's current size to get most recently added data,
/// delete it, decrement, return that deleted data
Stack.prototype.pop = function() {
	// set size and deletedData variables
	var size = this._size,
			deletedData;

	// handle case where increment is 0
	if (size) {

		deletedData = this._storage[size];

		// logging
		console.log(deletedData + " deleted from stack");

		// delete most recent data, decrement
		delete this._storage[size];
		this._size--;

		return deletedData;
	}
};

// Usage

var stack = new Stack();

stack.push("World!");
stack.push("Hello,");

stack.pop();
