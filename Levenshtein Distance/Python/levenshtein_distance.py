"""
Iterative implementation of the Levenshtein distance algorithm

Metric for measuring the amount of difference between two sequences (edit distance).
Defined as the minimum number of edits needed to transform one string to another with
insertion, deletion, and substition being the "edits."
"""

def minimumEditDistance(stringOne, stringTwo):
	print("Starting string one: " + stringOne + " AND string two: " + stringTwo)
	if len(stringOne) > len(stringTwo):	# flip variables if string one is longer than the second
		stringOne, stringTwo = stringTwo, stringOne
	print("Resulting string one: " + stringOne + " AND string two: " + stringTwo)
	distances = range(len(stringOne) + 1)
	print(distances)
	for indexTwo, charTwo in enumerate(stringTwo):	#(0, char) (1, char) etc..
		newDistances = [indexTwo + 1]
		for indexOne, charOne in enumerate(stringOne):
			if charOne == charTwo:
				newDistances.append(distances[indexOne])
			else:
				newDistances.append(1 + min((distances[indexOne], distances[indexOne + 1], newDistances[-1])))
		distances = newDistances
	return distances[-1]

print(minimumEditDistance("kitten", "sitting")) # 3
print(minimumEditDistance("TestStringThatIsReallyLong", "Jeremy")) # 22