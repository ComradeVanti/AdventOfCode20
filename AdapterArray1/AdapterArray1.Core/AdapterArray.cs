namespace AdapterArray1;

public static class AdapterArray {

	public static int CalcJoltageRating(int[] joltages) {

		/*
		 
		var diffs = joltages.Sort().Prepend(0).Diffs().Append(3).ToArray();

        return diffs.CountItem(1) * diffs.CountItem(3);
		 
		 */
		
		var sortedJoltages = joltages.OrderBy(it => it).ToArray();
		var oneCount = 0;
		var threeCount = 1;

		var firstDiff = sortedJoltages[0];

		if (firstDiff == 3)
			threeCount++;

		if (firstDiff == 1)
			oneCount++;

		for (var i = 0; i < sortedJoltages.Length-1; i++) {
			var diff = sortedJoltages[i + 1] - sortedJoltages[i];

			if (diff == 3)
				threeCount++;

			if (diff == 1)
				oneCount++;
		}
		
		return threeCount * oneCount;
	}

}