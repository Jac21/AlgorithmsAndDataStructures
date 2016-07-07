"use strict";

// implementation
function Queue() {
	this._oldestIndex = 1;
	this._newestIndex = 1;
	this._storage = {};
}

// Structure Methods
Queue.prototype.size = function() {
	var size = this._newestIndex - this._oldestIndex;

	// logging
	console.log("Size of queue: ", size);

	return size;
};

Queue.prototype.enqueue = function(data) {
	// logging
	console.log("Enqueued " + data + " to queue");

	this._storage[this._newestIndex] = data;
	this._newestIndex++;

	// logging
	console.log("New index at: ", this._newestIndex);
};

Queue.prototype.dequeue = function() {
	var oldestIndex = this._oldestIndex,
			newestIndex = this._newestIndex,
			deletedData;

	if (oldestIndex !== newestIndex) {
		deletedData = this._storage[oldestIndex];

		// logging
		console.log("Deleting " + deletedData + " from queue");

		delete this._storage[oldestIndex];
		this._oldestIndex++;

		// logging
		console.log("Old index at: ", this._oldestIndex);

		return deletedData;
	}
};

// Usage
var queue = new Queue();

queue.enqueue(1);
queue.enqueue(2);
queue.dequeue();
queue.size();