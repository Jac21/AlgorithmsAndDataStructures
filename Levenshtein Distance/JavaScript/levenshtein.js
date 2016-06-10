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
var inputA = "Jeremy", inputB = "Germany";
output.innerText = ("Levenshtein distance for " + inputA + " and " + inputB + ": " + levenshtein("Jeremy", "Germany"));