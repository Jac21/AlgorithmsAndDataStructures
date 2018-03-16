/*
 * Skip List JS implementation
 */

/*
 * Nodes
 */

var Node = function(value) {
	this.value = value;
};

Node.prototype.insertAfter = function(node) {
	// inserts 'this' after the node argument
	this.right = node.right;
	node.right && (node.right.left = this); // only assigned if node.right exists
	node.right = this;
	this.left = node;
};

Node.prototype.stackOnTop = function(node) {
	// places 'this' on top of node argument
	this.up = node.up;
	node.up && (node.up.down = this); // only assigned if node.right exists
	node.up = this;
	this.down = node;
};

Node.prototype.remove = function() {
	this.down && (this.down.up = this.up);
	this.left && (this.left.right = this.right);
	this.right && (this.right.left = this.left);
	this.up && (this.up.down = this.up);
};

/*
 * Skip List
 */

var SkipList = function() {
	this.head = new Node(-Infinity);
};

SkipList.prototype.search = function(value) {
	return (function search(at, value) {
		// if value is found, return true
		if(at.value === value) return true;

		// if right is smaller, go right
		if(at.right && at.right.value <= value){
			return search(at.right, value);
		}

		// otherwise, go down if possible
		if(at.down) {
			return search(at.down, value);
		}

		// cannot go right or down, the value is not in the skip list
		return false;
	})(this.head, value);
};

SkipList.prototype.remove = function(value) {
	return function remove(at, value) {
		// value found -> perform delete
		if(at.value === value){
			// do delete
			while(at){
				at.remove();
				// if nothing else on this level and not on level 1
				if(at.left === this.head && !at.right && this.head.down){
					// set head to next level down
					this.head = this.head.down;
					this.head.up = null;
				}
				at = at.down;
			}
			// remove complete
			return true;
		}

		// if right is smaller, go right
		if(at.right && at.right.value <= value){
			return remove.call(this, at.right, value);
		}

		// otherwise, go down if possible
		if(at.down){
			return remove.call(this, at.down, value);
		}

		// value not found -> remove failed
		return false;
	}.call(this, this.head, value);
};

var coinFlip = function() {
  return Math.random() < 0.5;
};

SkipList.prototype.insert = function(value) {
	var drops = []; // *
	return function insert(at, value){
		// value already exists, insertion fail
		if(at.value === value) return false;

		// if right is smaller, go right
		if(at.right && at.right.value <= value){
			return insert.call(this, at.right, value);
		}

		// otherwise, go down if possible
		if(at.down){
			drops.push(at); // *
			return insert.call(this, at.down, value);
		}

		// do insert
		var base = new Node(value);
		base.insertAfter(at);

		while(coinFlip()){
			var promote = new Node(value);
			promote.stackOnTop(base);
			base = promote;

			var drop = drops.pop();
			if(drop){
				promote.insertAfter(drop);
			} else {
				var newHead = new Node(-Infinity);
				newHead.stackOnTop(this.head);
				this.head = newHead;

				promote.insertAfter(this.head);
			}
		}

		return true;

	}.call(this, this.head, value);
};

// testing
var mySkipList = new SkipList();
mySkipList.insert(1);
mySkipList.insert(3);
mySkipList.insert(4);
mySkipList.insert(7);
mySkipList.insert(8);
mySkipList.insert(9);
console.log(mySkipList);

console.log(mySkipList.search(4));
mySkipList.remove(4);
console.log(mySkipList.search(4));
