<Query Kind="Program" />

void Main()
{
	Problem90().Dump("Result");
}

// Define other methods and classes here

public static long Problem90()
{
	int[] requiredPairsInt = new int[] { 01, 04, 09, 16, 25, 36, 49, 64, 81 };
	//var requiredPairs = requiredPairsInt.Select(i => new int[] { i / 10, i % 10 });
	
	//requiredPairs.Dump();
	
	Problem90_Recurse(requiredPairsInt, 0, new List<int>(), new List<int>());
	
	return -90;
}
public static void Problem90_Recurse(int[] requiredPairs, int index, List<int> die1, List<int> die2)
{
	if (index == requiredPairsInt.Length)
	{
		(die1 + " -- " + die2).Dump();
		return;
	}
	
	requiredPairs[index] / 10;
	requiredPairs[index] % 10;
}
