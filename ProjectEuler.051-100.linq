<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	Problem87().Dump("Result");
}

// Define other methods and classes here
public static long Problem87()
{
	int MAX = 50000000;
	
	BitArray MASK = new BitArray(MAX);
	
	int SquareLimit = (int)Math.Sqrt(MAX);
	int ForthLimit = (int)Math.Sqrt(Math.Sqrt(MAX));
	int CubeLimit = (SquareLimit + ForthLimit + 1) / 2;
	
	SquareLimit.Dump();
	int highest = 0;
	List<int> Primes = Helpers.PrimesLessThan(SquareLimit+500);  //TODO: WHAT THE SHIT, adding 500 gives a smaller number that is correct
	
	int count = 0;
	
	for (int i = 0; i < Primes.Count && Primes[i] <= SquareLimit; ++i)
	{
		int iN = Primes[i];
		int square = iN*iN;
		for (int j = 0; j < Primes.Count && Primes[j] <= CubeLimit; ++j)
		{
			int jN = Primes[j];
			int cube = jN*jN*jN;
			
			//Since cube limit is not accurate, this is required
			if (cube > MAX) break;
			
			for (int k = 0; k < Primes.Count && Primes[k] <= ForthLimit; ++k)
			{
				int kN = Primes[k];
				int forth = kN*kN*kN*kN;
				
				if (forth > MAX) break;
				
				int sum = square + cube + forth;
				if (sum < MAX)
				{
					if (iN > highest) highest = iN;
					//Util.HorizontalRun(true, sum, iN, jN, kN).Dump();
					if (!MASK[sum])
					{
						MASK[sum] = true;
						++count;
					}
				}
			}
		}
	}
	
	highest.Dump();
	return count;
}
public static long Problem91()
{
	int MAX = 50;
	
	int count = 0;
	
	for (int x1 = 0; x1 <= MAX; ++x1)
	{
		for (int y1 = 0; y1 <= MAX; ++y1)
		{
			//skip (0,0)
			if (x1 == 0 && y1 == 0) continue;
			
			int OP2 = x1*x1 + y1*y1;  //0,0 to each
			
			if (OP2 == 0) continue;
			
			for (int x2 = 0; x2 <= MAX; ++x2)
			{
				for (int y2 = 0; y2 <= MAX; ++y2)
				{
					if (x2 == 0 && y2 == 0) continue;
					
					int OQ2 = x2*x2 + y2*y2;
					if (OQ2 == 0) continue;
					int PQ2 = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1);
					if (PQ2 == 0) continue;
					
					bool taken = false;
					if (PQ2 == OP2 + OQ2
						|| OP2 == PQ2 + OQ2
						|| OQ2 == OP2 + PQ2)
					{
						taken = true;
						++count;
					}
//					if (taken)
//					{
//						Draw(MAX, x1, y1, x2, y2);
//					}
				}
			}
		}
	}
	
	count.Dump("Found");
	
	return count / 2;
}
public static void Draw(int MAX, int x1, int y1, int x2, int y2)
{
	int US = 20;
	using (Bitmap b = new Bitmap(MAX*US, MAX*US))
	{
		using (Graphics g = Graphics.FromImage(b))
		{
			Pen pen = new Pen(Color.DarkBlue);
			g.DrawLine(pen, x1*US, y1*US, x2*US, y2*US);
			g.DrawLine(pen, 0, 0, x2*US, y2*US);
			g.DrawLine(pen, 0, 0, x1*US, y1*US);
		}
		b.Dump();
	}
}
public static long Problem90()
{
	int[] requiredPairsInt = new int[] { 01, 04, 09, 16, 25, 36, 49, 64, 81 };
	//int[] requiredPairsInt = new int[] { 01, 04, 09, 19, 25, 39, 49, 94, 81 };
	//var requiredPairs = requiredPairsInt.Select(i => new int[] { i / 10, i % 10 });
	
	//requiredPairs.Dump();
	
	List<Helpers.tuple_double<List<int>, List<int>>> finishedList = 
		Problem90_Recurse(requiredPairsInt, 0, new List<int>(), new List<int>());
	
//	//finishedList.Dump();
//	var stringList = from f in finishedList
//					 select new { dieA = string.Join(" ", f.objA.OrderBy(d => d).Select(d => "a" + d.ToString()).ToArray()),
//					 			  dieB = string.Join(" ", f.objB.OrderBy(d => d).Select(d => "b" + d.ToString()).ToArray()) };
//	
//	//stringList.OrderBy(s => s.dieA).ThenBy(s => s.dieB).Dump();
//	stringList.GroupBy(s => s.dieA + s.dieB).Select(g => g.First()).Dump();
	
	finishedList = Problem90_PadDice(finishedList);
	
	List<string> outputList = new List<string>();
	int Count = 0;
	foreach (var f in finishedList)
	{
		//int RemoveInt = 9;
		List<int> objA = f.objA;
		//if (objA.Count > 6 && objA.Contains(6) && objA.Contains(9)) objA.Remove(RemoveInt);
		List<int> objB = f.objB;
		//if (objB.Count > 6 && objB.Contains(6) && objB.Contains(9)) objB.Remove(RemoveInt);
		
		string dieA = string.Join("", objA.OrderBy(d => d).Select(d => d.ToString()).ToArray());
		string dieB = string.Join("", objB.OrderBy(d => d).Select(d => d.ToString()).ToArray());
		
		string orderA = "A" + dieA + " B" + dieB;
		string orderB = "A" + dieB + " B" + dieA;
		
		if (!outputList.Contains(orderA) && !outputList.Contains(orderB))
		{
			outputList.Add(orderA);
		}
	}
	outputList.Dump();
	
	
	SuccessCount.Dump("Successful dice");
	TotalCount.Dump("Total dice");
	
	return -90;
}
public static List<Helpers.tuple_double<List<int>, List<int>>> Problem90_PadDice(List<Helpers.tuple_double<List<int>, List<int>>> dicePairs)
{
	//List<Helpers.tuple_double<List<int>, List<int>>> newOutputList = new List<Helpers.tuple_double<List<int>, List<int>>>(dicePairs.Count);
	
	//Fuck it all, ugly way going through eveyrthing twice
	for (int n = 0; n < dicePairs.Count; ++n)
	{
		List<int> die = dicePairs[n].objA;
		
		if (die.Count >= 6)
		{
			//newOutputList.Add(dicePairs[n]);
			continue;
		}
		
		if (die.Count == 5)
		{
			for (int i = 0; i <= 9; ++i)
			{
				if (!die.Contains(i))
				{
					List<int> newDie = die.ToList();
					newDie.Add(i);
					
					dicePairs.Add(new Helpers.tuple_double<List<int>, List<int>>(newDie, dicePairs[n].objB));
				}
			}
			//Remove this die
			dicePairs.Remove(dicePairs[n]);
			--n;
		}
		
		if (die.Count < 5) throw new NotImplementedException("Need to handle less than 5");
	}
	for (int n = 0; n < dicePairs.Count; ++n)
	{
		List<int> die = dicePairs[n].objB;
		
		if (die.Count >= 6)
		{
			//newOutputList.Add(dicePairs[n]);
			continue;
		}
		
		if (die.Count == 5)
		{
			for (int i = 0; i <= 9; ++i)
			{
				if (!die.Contains(i))
				{
					List<int> newDie = die.ToList();
					newDie.Add(i);
					
					dicePairs.Add(new Helpers.tuple_double<List<int>, List<int>>(dicePairs[n].objA, newDie));
				}
			}
			//Remove this die
			dicePairs.Remove(dicePairs[n]);
			--n;
		}
		
		if (die.Count < 5) throw new NotImplementedException("Need to handle less than 5");
	}
	
	return dicePairs;
}
public static int SuccessCount = 0;
public static int TotalCount = 0;
public static List<Helpers.tuple_double<List<int>, List<int>>> Problem90_Recurse(int[] requiredPairs, int index, List<int> die1, List<int> die2)
{
	List<Helpers.tuple_double<List<int>, List<int>>> output = new List<Helpers.tuple_double<List<int>, List<int>>>();
	
	if (index == requiredPairs.Length)
	{
		++TotalCount;
		//if (die1.Count <= 6 && die2.Count <= 6)
		//OR count 7 if contains both 6 and 9
		if ((die1.Count <= 6 || (die1.Count == 7 && die1.Contains(6) && die1.Contains(9)))
			&& (die2.Count <= 6 || (die2.Count == 7 && die2.Contains(6) && die2.Contains(9))))
		{
//			(string.Join(" ", die1.OrderBy(d => d).Select(d => d.ToString()).ToArray()) + " -- "
//				+ string.Join(" ", die2.OrderBy(d => d).Select(d => d.ToString()).ToArray())).Dump();
			++SuccessCount;
			
			//TODO if die.Count < 6, that is 4 possible matches
			
			output.Add(new Helpers.tuple_double<List<int>, List<int>>(die1, die2));
			return output;
		}
		return output;
	}
	
	int a = requiredPairs[index] / 10;
	int b = requiredPairs[index] % 10;
	
	
	List<int> recurse1 = die1.ToList();
	List<int> recurse2 = die2.ToList();
	
	if (!recurse1.Contains(a)) recurse1.Add(a);
	if (!recurse2.Contains(b)) recurse2.Add(b);
	output.AddRange(Problem90_Recurse(requiredPairs, index+1, recurse1, recurse2));
	
	if (!die1.Contains(b)) die1.Add(b);
	if (!die2.Contains(a)) die2.Add(a);
	output.AddRange(Problem90_Recurse(requiredPairs, index+1, die1, die2));
	
	return output;
}

public static class Helpers
{
	public static bool[] GetPrimeSieve(int limit)
	{
		int sieveBound = (limit-1) / 2; //last index of sieve
		bool[] sieve = new bool[sieveBound];
		int crosslimit = ((int)Math.Sqrt(limit)-1) / 2;
		for (int i = 1; i < crosslimit; ++i)
		{
			if (!sieve[i]) // 2*i+1 is prime, mark multiples
			{
				for (int j = 2*i*(i+1); j < sieveBound; j += 2*i+1)
				{
					sieve[j] = true;
				}
			}
		}
		return sieve;
	}
	public static List<int> PrimesLessThan(int limit)
	{
		int sieveBound = (limit-1) / 2; //last index of sieve
		bool[] sieve = GetPrimeSieve(limit);
		List<int> primes = new List<int>();
		primes.Add(2);
		for (int i = 1; i < sieveBound; ++i)
		{
			if (!sieve[i]) primes.Add(2*i+1);
		}
		return primes;
//		int sum = 2; //2 is prime
//		for (int i = 1; i < sieveBound; ++i)
//		{
//			if (!sieve[i]) sum += 2*i+1;
//		}
//		return sum;
	}
	
	public class tuple_double<A, B>
	{
		public tuple_double(A a, B b)
		{
			_a = a;
			_b = b;
		}
		A _a;
		B _b;
		
		public A objA
		{
			get { return _a; }
			set { _a = value; }
		}
		public B objB
		{
			get { return _b; }
			set { _b = value; }
		}
	}
}