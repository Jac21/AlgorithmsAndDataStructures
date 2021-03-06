function levenshtein(stringOne, stringTwo) {
	var t = [], u, i, j, stringOneLength = stringOne.length, stringTwoLength = stringTwo.length;

	if (!stringOneLength) {
		return stringTwoLength;
	}

	if (!stringTwoLength) {
		return stringOneLength;
	}

	for (j = 0; j <= stringTwoLength; j++) {
		t[j] = j;
	}

	for (i = 1; i <= stringOneLength; i++) {
		for (u = [i], j = 1; j <= stringTwoLength; j++) {
			u[j] = stringOne[i - 1] === stringTwo[j - 1] ? t[j - 1] : Math.min(t[j - 1], t[j], u[j - 1]) + 1;
		} 
		t = u;
	} 
	return u[stringTwoLength];
}

var output = document.getElementById('distance');
var inputA = "String", inputB = "Strung";
output.innerText = ("Levenshtein distance for " + inputA + " and " + inputB + ": " + levenshtein(inputA, inputB));

/**
* Finds edit distance between input and entries in an array, 
* returns entry with smallest distance
* @param {String} inputString
* @param {Array} stringArray
* @return {String} outputString
*/

// external memoization cache
var memCache = {};

function findLeastEditDistanceMemoized(inputString, stringArray) {
	// output string is set as the string with the smallest edit distance,
	// minimum number is used to check
	var outputString = "";
	var minimumNumber = Number.POSITIVE_INFINITY;

	// console.log(memCache);

	if (inputString in memCache) {
		// console.log('Cache hit for ', inputString);
		return memCache[inputString];
	} else {
		// console.log('Cache miss for ', inputString);
		// iterate through input array, find distance between inputString and string in array
		stringArray.forEach(function(arrayString) {
			var distance = levenshtein(inputString.toLowerCase(), arrayString.toLowerCase());

			// check if current distance is smaller than the minimum, set minimum and outputString 
			// if so
			if (distance < minimumNumber) {
				minimumNumber = distance
				outputString = arrayString
			}
		});

		return memCache[inputString] = outputString;
	}
}

var brandArray = ["Inspiron", "XPS", "AlienWare", "Latitude", "Precision", "ChromeBook"];
var incorrectBrandArray = ["Inspiron", "XPS", "AlienWare", "Latitude", "Precision", "ChromeBook",
														"Inspyron", "ExPS", "Anywhere", "Layditude", "Preecision", "CheromeBook",
														"Inspyron", "ExPS", "Anywhere", "Layditude", "Preecision", "CheromeBook"];

var brandOutput = document.getElementById('brand');

var t0 = performance.now();

incorrectBrandArray.forEach(function(string) {
	brandOutput.innerText += ("Brand returned for: " + string + " is " + (findLeastEditDistanceMemoized(string, brandArray)) + ", \n");
});

var t1 = performance.now();
console.log("Call to doSomething took " + (t1 - t0) + " milliseconds.")


/* tests
[ ['', '', 0],
  ['yo', '', 2],
  ['', 'yo', 2],
  ['yo', 'yo', 0],
  ['tier', 'tor', 2],
  ['saturday', 'sunday', 3],
  ['mist', 'dist', 1],
  ['tier', 'tor', 2],
  ['kitten', 'sitting', 3],
  ['stop', 'tops', 2],
  ['rosettacode', 'raisethysword', 8],
  ['mississippi', 'swiss miss', 8]
].forEach(function(v) {
  var a = v[0], b = v[1], t = v[2], d = levenshtein(a, b);
  if (d !== t) {
    console.log('levenstein("' + a + '","' + b + '") was ' + d + ' should be ' + t);
  }
});
*/