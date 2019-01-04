const zip = (...arrays) => {
	const maxLength = Math.max(...arrays.map(x => x.length));

	return Array.from({
		length: maxLength
	}).map((_, i) => {
		return Array.from({
			length: arrays.length
		}, (_, k) => arrays[k][i]);
	});
};

// Return the Hamming distance between equal-length sequences
const hamming = function (s1, s2) {
	if (s1.length != s2.length)
		throw new Error("Undefined for sequences of unequal length");

	var hammingSum = 0;

	var sequenceZip = zip(s1, s2);

	sequenceZip.forEach(function (sequenceArray) {
		if (sequenceArray[0] != sequenceArray[1])
			hammingSum += 1;
	});

	return hammingSum;
}

// testing
let output = document.getElementById('distance');

let inputA = "String",
	inputB = "Strung";

output.innerText =
	("Hamming distance for " + inputA + " and " + inputB + ": " + hamming(inputA, inputB));

let outputTwo = document.getElementById('distanceTwo');

let inputTwoA = "1011101",
	inputTwoB = "1001001";

outputTwo.innerText =
	("Hamming distance for " + inputTwoA + " and " + inputTwoB + ": " + hamming(inputTwoA, inputTwoB));
