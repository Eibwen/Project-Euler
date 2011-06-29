<Query Kind="Program" />

void Main()
{
	//TODO test List<int> PrimesLessThan(int maxValue) on laptop vs work, switch it to long?
	Question25().Dump("Result");
}

// Define other methods and classes here

/* Thoughts
#314: I'm thinking take 1 quadrant
	take from both outer corners a straight line
	recursive algorithm? to move the center of the line out for the best area... then do that for each sub lines

*/

//Fuck this bullshit statistics
#region Question298
//public static int Question298()
//{
//	int loop = 10000;
//	int sum = 0;
//	for (int i = 0; i < loop; ++i)
//	{
//		sum += Question298_Brute();
//	}
//	sum.Dump("Sum");
//	((Decimal)sum / loop).Dump("Average");
//	return 0;
//}
//public static int Question298_Brute()
//{
//	List<int> Larry = new List<int>(5);
//	Queue<int> Robin = new Queue<int>(5);
//	
//	int LarryScore = 0;
//	int RobinScore = 0;
//	
//	Random rand = new Random();
//	
//	for (int i = 0; i < 50; ++i)
//	{
//		int called = rand.Next(1, 10);
//		//called.Dump();
//		
//		if (Larry.Contains(called))
//		{
//			++LarryScore;
//			Larry.Remove(called);
//			Larry.Add(called);
//		}
//		else
//		{
//			Larry.Add(called);
//			if (Larry.Count > 5) Larry.RemoveAt(0);
//		}
//		
//		if (Robin.Contains(called))
//		{
//			++RobinScore;
//		}
//		else
//		{
//			Robin.Enqueue(called);
//			if (Robin.Count > 5) Robin.Dequeue();
//		}
//	}
//	
//	return LarryScore - RobinScore;
//}
#endregion Question298

#region Question37
public static int Question37()
{
//	int[] KnownPrimes = new int[] { 3797, 797, 97, 7, 379, 37, 3, 23 };
//	
//	int[] BasePrimes = new int[] { 2, 3, 5, 7 };
//	int[] EndPrimes = new int[] { 3, 7 };
	
	int[] Cheating = new int[] { /*2, 3, 5, 7,*/ 23, 37, 53, 73, 313, 317, 373, 797, 3137, 3797, 739397 };
	
	int MAX_CHECK = 9;
	
	//StartWith
	for (int num = 0; num <= MAX_CHECK; ++num)
	{
		Util.Progress = num * 100 / MAX_CHECK;
		
		////Recurse
		//Question37_Recurse(i);
		
		//TODO Future:
		// Maybe could try combining the ones i already found...  idk how that would work
		
		//Try adding to the right:
		Question37_Recurse(num * 10 + 3);
		Question37_Recurse(num * 10 + 7);
		
		//Try adding to the left
		Question37_Recurse(num + 3 * 10);
		Question37_Recurse(num + 7 * 10);
	}
	
	//List<int> primes = Helpers.FirstXPrimes(2000);
	
	//checking.Dump();
	foundNumbers.Count.Dump("Total Found");
	foundNumbers.Distinct().Where(f => f > 9).OrderBy(f => f).Dump().Sum().Dump("Result");
	//Cheating.Except(foundNumbers).Dump("NEED");
	return -1;
}
//static List<int> checking = new List<int>();
static List<int> foundNumbers = new List<int>();
public static void Question37_Recurse(int num)
{
//	//checking.Add(num);
//	if (!Question37_CheckTruncatable(num)) return;
//	
//	//Found one:
//	//num.Dump("Found");
//	foundNumbers.Add(num);
	
	Truncatable trunc = Question37_CheckTruncatable(num);
	if (trunc == Truncatable.None) return;
	if (trunc == Truncatable.Both) foundNumbers.Add(num);
	
	//Try adding to the right:
	Question37_Recurse(num * 10 + 3);
	Question37_Recurse(num * 10 + 7);
	Question37_Recurse(num * 10 + 9);
	
	//Try adding to the left
	int Modifier = 10;
	while (num > Modifier)
	{
		Modifier *= 10;
	}
	Question37_Recurse(num + 3 * Modifier);
	Question37_Recurse(num + 7 * Modifier);
}
[Flags]
public enum Truncatable 
{
	None = 0x0,
	Left = 0x1,
	Right = 0x2,
	Both = 0x3
}
public static Truncatable Question37_CheckTruncatable(int num)
{
	//If it overloads int, just ignore it
	if (num < 0) return Truncatable.None;
	
	if (!Helpers.IsPrime(num)) return Truncatable.None;
	
	//num.Dump();
	
	int length = 0;
	Truncatable trun = Truncatable.None;
	
	int numb = num;
	bool isRight = true;
	while (numb > 10)
	{
		++length;
		numb /= 10;
		
		if (!Helpers.IsPrime(numb))
		{
			isRight = false;
			break;
		}
	}
	if (isRight)
	{
		//numb.Dump("Right");
		trun = trun | Truncatable.Right;
	}
	
	int Modifier = 1;
	for (int i = 0; i < length; ++i)
	{
		Modifier *= 10;
	}
	
	numb = num;
	bool isLeft = true;
	for (int i = 0; i < length && Modifier > 1; ++i)
	{
		//(Modifier + " -- " + num + " -- " + (numb % Modifier)).Dump("fff");
		if (!Helpers.IsPrime(numb % Modifier))
		{
			isLeft = false;
			break;
		}
		
		Modifier /= 10;
	}
	if (isLeft)
	{
		//Modifier.Dump("Left");
		trun = trun | Truncatable.Left;
	}
	
	return trun;
}
#endregion Question37


public static long Question25()
{
	double phi = 1.61803398874989; //1.61803398874989484820458683436563811772030917980576286213544862270526046281890244970720720418939113748475;
	double sqrt5 = Math.Sqrt(5);
	
	//Get Fibonacci(n)
	int n = 5;
	int Fn = (int)(Math.Pow(phi, n) / sqrt5 + 0.5);
	Fn.Dump();
	
	int SEQ_COUNT = 12;
	
	//Fibonacci
	for (int i = 3; i < SEQ_COUNT; ++i)
	{
		
	}
	
	return -2;
}
public static long Question17()
{
//	Question17_GetWordLength(908).Dump("=17");
//	return 0;
	
	int[] tests = new int[]
	{
		342, 23,
		115, 20,
		892, 24,
		893, 26,
		894, 25,
		895, 25,
		896, 24,
		897, 26,
		898, 26,
		899, 25,
		900, 11,
		901, 17,
		
		908, 19,
		909, 18,
		910, 17,
		911, 20,
		912, 20
	};
	for (int k = 0; k < tests.Length; ++k)
	{
		if (Question17_GetWordLength(tests[k]) != tests[++k])
			tests[k-1].Dump("FAIL");
	}
	
	Question17_GetWordLength(900).Dump();
	Question17_GetWordLength(99).Dump();
	Question17_GetWordLength(999).Dump();
	
	int LOOP = 1000;
	
	long outputSum = 0;
	for (int i = 1; i <= LOOP; ++i)
	{
		outputSum += Question17_GetWordLength(i);//.Dump(i.ToString());
	}
	return outputSum;
}
public static long Question17_GetWordLength(int num)
{
	if (num > 1000)
	{
		throw new ApplicationException("Not coded to support over 1000");
	}
	
	
	int[] singles = new int[]
	{
		-99,
		"one".Length,
		"two".Length,
		"three".Length,
		"four".Length,
		"five".Length,
		"six".Length,
		"seven".Length,
		"eight".Length,
		"nine".Length,
		"ten".Length
	};
	int[] tens = new int[]
	{
		-99,
		"ten".Length,
		"twenty".Length,
		"thirty".Length,
		"forty".Length,
		"fifty".Length,
		"sixty".Length,
		"seventy".Length,
		"eighty".Length,
		"ninety".Length
	};
	int[] teens = new int[]
	{
		"ten".Length,
		"eleven".Length,
		"twelve".Length,
		"thirteen".Length,
		"fourteen".Length,
		"fifteen".Length,
		"sixteen".Length,
		"seventeen".Length,
		"eighteen".Length,
		"nineteen".Length,
		"twenty".Length
	};
	
	//Thousand... only need to handle 1000
	if (num == 1000) return "onethousand".Length;
	
	if (num <= 10) return singles[num];
	
	int length = 0;
	
	if (num >= 100)
	{
		length += singles[num / 100] + "hundred".Length;
	}
	int andTens = num % 100;
	if (andTens > 0)
	{
		if (num >= 100) length += "and".Length;
		
		int andOnes = andTens % 10;
		if (andOnes > 0)
		{
			if (andTens <= 10) length += singles[andTens];
			else if (andTens < 20)
				length += teens[andOnes];
			else
			{
				length += tens[andTens / 10];
				length += singles[andOnes];
			}
			//length.Dump();
		}
		else
		{
			length += tens[andTens / 10];
		}
	}
	
	return length;
}
public static long Question35()
{
//	int length = 3;
//	int lengthLimit = 100;
//	
//	int rot = 197;
//	for (int j = 1; j < length; ++j)
//	{
//		rot = (rot / 10) + (rot % 10) * lengthLimit;
//		rot.Dump();
//		if (!Helpers.isPrime_Q7PDF(rot)) "fail".Dump();
//	}
//	return -2;
	
	
	
	List<int> primes = Helpers.PrimesLessThan(1000000);
	primes.Count.Dump("Checking Primes");
	
	List<int> rotations = new List<int>();
	
	int length = 1;
	int lengthLimit = 10;
	foreach (int i in primes)
	{
		Util.Progress = i * 100 / primes.Count;
		
		if (i >= lengthLimit)
		{
			++length;
			lengthLimit *= 10;
		}
		
		if (length == 1)
		{
			//i.Dump("Added");
			rotations.Add(i);
			continue;
		}
		int rot = i;
		//rot.Dump("Starting Rot");
		bool shouldAdd = true;
		for (int j = 1; j < length; ++j)
		{
			rot = (rot / 10) + (rot % 10) * (lengthLimit/10);
			//rot.Dump();
			if (!Helpers.isPrime_Q7PDF(rot))
			{
				shouldAdd = false;
				break;
			}
		}
		if (shouldAdd)
		{
			//i.Dump("Added");
			rotations.Add(i);
		}
	}
	
	rotations.Dump();
	return rotations.Count;
}
public static long Question30()
{
	//Read forum, found how to get the max possible digits:
	long ninesPow = 9 * 9 * 9 * 9 * 9;
	int length = 1;
	long ninesDigits = 9;
	while (length * ninesPow > ninesDigits)
	{
		++length;
		ninesDigits = ninesDigits * 10 + 9;
	}
	length.Dump("Smaller than length");
	long LOOP_MAX = length * ninesPow;
	LOOP_MAX.Dump("All numbers must be smaller");
	
	//My original solution:
	int LOOP = 1000000;
	
	int outputSum = 0;
	for (int i = 2; i < LOOP; ++i)
	{
		if (i == Question30_DigitsFifthPowerSum(i))
		{
			outputSum += i.Dump();
		}
	}
	return outputSum;
}
public static long Question30_DigitsFifthPowerSum(long t)
{
	long sum = 0;
	while (t > 0)
	{
		long i = (t % 10);
		sum += i * i * i * i * i;
		t /= 10;
	}
	return sum;
}
public static long Question31()
{
	int[] answersaaa = new int[] { 1, 2, 4, 9 };
	
	int[] Currencies = new int[] { 1, 2, 5, 10, 20, 50, 100, 200 };
	
//	for (int i = 1; i < Currencies.Length; ++i)
//	{
//		int count = 1;
//		for (int j = i; j > 0; --j)
//		{
//			
//		}
//	}
	
	//LOL sort shocked this worked... wrote in like 2 minutes after trying a loop first
	Question31_recurse(ref Currencies, 7, 0, 0).Dump();
	
	return -1;
}
public static long Question31_recurse(ref int[] Currencies, int index, long count, int sum)
{
	if (sum > 200) return count;
	if (sum == 200) return ++count;
	
	count = Question31_recurse(ref Currencies, index, count, sum + Currencies[index]);
	if (index > 0)
	{
		count = Question31_recurse(ref Currencies, index-1, count, sum);
	}
	return count;
}
public static long Question12()
{
	//return Question12_CountFactors(24);
	
	int loop = 10000;
	int skip = 9900;
	
	int best = 0;
	
	long triangleNumber = 0;
	for (int i = 1; i < loop; ++i)
	{
		triangleNumber += i;
		
		if (i < skip) continue;
		
		int curFactors = Question12_CountFactors(triangleNumber);
		if (curFactors > best) best = curFactors;
		if (curFactors > 500)
		{
			return triangleNumber;
		}
	}
	
	best.Dump();
	return -1;
}
public static int Question12_CountFactors(long num)
{
	if (num == 1) return 1;
	
	//Itself and 1
	int count = 2;
	
	if (num % 2 == 0) ++count;
	if (num % 3 == 0) ++count;
	
	//long upperLimit = num / 2;
	for (long i = num / 2; i > 3; --i)
	{
		if (num % i == 0)
		{
			//i.Dump();
			++count;
//			upperLimit /= 2;
//			i = upperLimit;
			//i = (i+3)/2;
			//i.Dump();
			//("==" + upperLimit).Dump();
		}
	}
	return count;
	
//	int count = 0;
//	int t = 21;
//	int f = 2;
//	while (f > 1)
//	{
//		++count;
//		f = Helpers.GetAFactor_Basedon_Q7PDF(t).Dump();
//		t /= f;
//	}
//	count.Dump("c");
//	
//	Helpers.Factorial(count).Dump();
//	
//	return -1;
}

public static long Question67()
{
	string triInput = File.ReadAllText(@"C:\Users\gwalker\Documents\LINQPad Queries\ProjectEuler_Question67_triangle.txt");
	triInput = triInput.Trim();
	return Question18(triInput);
}
public static long Question49()
{
	List<int> primes = Helpers.PrimesLessThan(10000);
	
	while (primes[0] < 1000)
	{
		primes.RemoveAt(0);
	}
	
	var canidates = primes.GroupBy(p => new { A = Question49_SumSquares(p), B = Helpers.SumDigits(p) })
		.Where(g => g.Count() > 2);
	//canidates.Dump();
	
	foreach (var g in canidates)
	{
		var l = g.ToList();
//		if (l[2] - l[1] == l[1] - l[0])
//		{
//			l.Dump("Win");
//		}
		for (int i = 0; i < l.Count; ++i)
		{
			int basePrime = l[i];
			for (int j = i+1; j < l.Count; ++j)
			{
				int nextPrime = l[j] + l[j] - basePrime;
				if (l.Contains(nextPrime))
				{
					int diff = (l[j] - basePrime);
					("1: " + basePrime
					+ "   2: " + l[j]
					+ "   3: " + nextPrime
					+ "   Diff: " + diff).Dump("Win");
					
					long output = basePrime;
					output = output * 10000 + l[j];
					output = output * 10000 + nextPrime;
					
					if (diff == 3330 && output != 148748178147)
					{
						return output;
					}
				}
			}
		}
	}
	
	return 0;
}
public static long Question49_SumSquares(long t)
{
	long hash = 0;
	while (t > 0)
	{
		hash += (t % 10) * (t % 10);
		t /= 10;
	}
	return hash;
}
//public static long Question49_HashDigits(long t)
//{
//	long hash = 0;
//	while (t > 0)
//	{
//		hash ^= (t % 10);
//		t /= 10;
//	}
//	return hash;
//}
public static long Question20()
{
	List<int> array = new List<int>();
	array.Add(100);
	
	for (int i = 99; i > 0; --i)
	{
		Question16B_Multiply(ref array, i);
	}
	return Question16B_SumDigits(array);
}
public static long Question18()
{
	return Question18(null);
}
public static long Question18(string triInput)
{
	string triangle = @"75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";
	
	if (triInput != null)
	{
		triangle = triInput;
	}
	
	var rows = triangle.Split('\n');
	long[] prev = null;
	for (int i = rows.Length-1; i >= 0; --i)
	{
		long[] current = rows[i].Split(' ').Select(s => long.Parse(s)).ToArray();
		if (prev != null)
		{
			Question18_CombineRows(ref current, prev);
		}
		prev = current;
	}
	prev.Dump();
	return prev[0];
}
public static void Question18_CombineRows(ref long[] parents, long[] children)
{
	//parents will be the output
	
	for (int i = 0; i < parents.Length; ++i)
	{
		long l = children[i];
		if (children[i+1] > l) l = children[i+1];
		parents[i] += l;
	}
	
	//return parents;
}
public static long Question16()
{
	int POW = 1000;
	
//	long m = 1;
//	for (int i = 0; i < POW; ++i)
//	{
//		m *= 2;
//	}
//	m.Dump();
//	long sum = 0;
//	while (m > 1)
//	{
//		sum += m % 10;
//		m /= 10;
//	}
//	return sum;
	List<int> array = new List<int>();
	array.Add(2);
	for (int i = 1; i < POW; ++i)
	{
		int pos = array.Count-1;
		if (array[pos] * 2 >= 10000) array.Add(0);
		
		//array.Dump();
		
		for (int j = pos; j >= 0; --j)
		{
			array[j] *= 2;
			if (array[j] >= 10000)
			{
				array[j] %= 10000;
				array[j+1] += 1;
			}
		}
	}
	array.Dump();
	
	//Sum them up
	int sum = 0;
	for (int i = 0; i < array.Count; ++i)
	{
		int t = array[i];
		while (t > 0)
		{
			sum += t % 10;
			t /= 10;
		}
	}
	return sum;
}
public static long Question16B()
{
	List<int> array = new List<int>();
	array.Add(2);
	
	for (int i = 1; i < 1000; ++i)
	{
		Question16B_Multiply(ref array, 2);
	}
	return Question16B_SumDigits(array);
}
public static void Question16B_Multiply(ref List<int> array, int factor)
{
	int pos = array.Count-1;
	if (array[pos] * factor >= 10000) array.Add(0);
	
	for (int j = pos; j >= 0; --j)
	{
		array[j] *= factor;
		if (array[j] >= 10000)
		{
			array[j+1] += array[j] / 10000;
			array[j] %= 10000;
		}
	}
}
public static long Question16B_SumDigits(List<int> array)
{
	//Sum them up
	int sum = 0;
	for (int i = 0; i < array.Count; ++i)
	{
		int t = array[i];
		while (t > 0)
		{
			sum += t % 10;
			t /= 10;
		}
	}
	return sum;
}
public static long Question15()
{
//	int SIZE = 4;
//	
//	int count = 0;
//	for (int i = 0; i < SIZE; ++i)
//	{
//		for (int j = i; j < SIZE; ++j)
//		{
//			for (int k = j; k < SIZE; ++k)
//			{
//				for (int l = j; l < SIZE; ++l)
//				{
//					++count;
//				}
//			}
//		}
//	}
//	return count;

//	int count = 0;
//	for (int i = 0; i < SIZE - 1; ++i)
//	{
//		for (int j = i; j < SIZE - 1; ++j)
//		{
//			count += 6;
//		}
//	}
//	return count;
	
//	((double)6			/2/3).Dump("2x2");
//	((double)20			/2/2/5).Dump("3x3");
//	((double)70			/2/5/7).Dump("4x4");
//	((double)252		/2/2/3/3/7).Dump("5x5");
//	((double)924		/2/2/3/7/11).Dump("6x6");
//	((double)3432		/2/2/2/3/11/13).Dump("7x7");
//	((double)12870		/2/3/3/5/11/13).Dump("8x8");
//	((double)48620		/2/2/5/11/13/17).Dump("9x9");
//	((double)184756		/2/2/11/13/17/19).Dump("10x10"); // 2*2*11*13*17*19
	
//	184756.Dump();
//	(Helpers.Factorial(3)).Dump("f");
	
//	((double)155117520	/2/2/2/2/3/3/5/17/19/23/29).Dump("15x15");
//	((double)601080390	/2/3/3/5/17/19/23/29/31).Dump("16x16");
	//Helpers.GetAFactor_Basedon_Q7PDF(31).Dump();
	//Helpers.isPrime_Q7PDF(899).Dump();
	
	//Helpers.FirstXPrimes(20)[7].Dump();
	
	//((Question15_SIZE*Question15_SIZE)*Math.Pow(3, Question15_SIZE)).Dump();
	
	//2:6
	//3:20
	//4:70
	//10:184756
	//15:155117520 -- 4seconds
	//16:601080390 -- 17seconds
	//17:2333606220 -- 66 secs
	
	int SIZE = 20;
	long[,] matrix = new long[SIZE+1, SIZE+1];
	for (int i = 0; i <= SIZE; ++i)
	{
		matrix[i,0] = 1;
		matrix[0,i] = 1;
	}
	for (int i = 1; i <= SIZE; ++i)
	{
		for (int j = i; j <= SIZE; ++j)
		{
			matrix[i,j] = matrix[i-1,j] + matrix[i,j-1];
			matrix[j,i] = matrix[i-1,j] + matrix[i,j-1];
		}
	}
//	for (int i = 0; i < SIZE; ++i)
//	{
//		matrix[i,0] = i+2;
//		matrix[0,i] = i+2;
//	}
//	int m = matrix[0,0];
//	for (int i = 1; i < SIZE; ++i)
//	{
//		m *= matrix[0,i];
//		matrix[i,1] = m;
//		matrix[1,i] = m;
//	}
	matrix.Dump();
	return matrix[SIZE,SIZE];
	
	//return Question15_recurse(0, 0, 0);
}
//const int Question15_SIZE = 3;
//public static long Question15_recurse(long paths, int x, int y)
//{
//	if (x == Question15_SIZE && y == Question15_SIZE)
//	{
//		return ++paths;
//	}
//	
////	if (x < Question15_SIZE && y < Question15_SIZE)
////	{
////		long r = Question15_recurse(paths, x+1, y+1);
////		if (r > paths) paths = r;
////	}
//	
//	if (x < Question15_SIZE)
//	{
//		long a = Question15_recurse(paths, x+1, y);
//		if (a > paths) paths = a;
//	}
//	if (y < Question15_SIZE)
//	{
//		long a = Question15_recurse(paths, x, y+1);
//		if (a > paths) paths = a;
//	}
//	
//	return paths;
//}
public static int Question14()
{
	//return Question14_RunSeq(13);
	int longestRun = 0;
	int longestRunNum = -1;
	for (int i = 1000000; i > 800000; --i)
	{
		int cur = Question14_RunSeq(i);
		if (cur > longestRun)
		{
			longestRunNum = i;
			longestRun = cur;
		}
	}
	return longestRunNum;
}
public static int Question14_RunSeq(long num)
{
	int chain = 1;
	while (num != 1)
	{
		//num.Dump();
		if (num % 2 == 0)
		{
			num /= 2;
		}
		else
		{
			num = num * 3 + 1;
		}
		++chain;
	}
	return chain;
}
public static long Question13()
{
	string input = @"37107287533902102798797998220837590246510135740250
46376937677490009712648124896970078050417018260538
74324986199524741059474233309513058123726617309629
91942213363574161572522430563301811072406154908250
23067588207539346171171980310421047513778063246676
89261670696623633820136378418383684178734361726757
28112879812849979408065481931592621691275889832738
44274228917432520321923589422876796487670272189318
47451445736001306439091167216856844588711603153276
70386486105843025439939619828917593665686757934951
62176457141856560629502157223196586755079324193331
64906352462741904929101432445813822663347944758178
92575867718337217661963751590579239728245598838407
58203565325359399008402633568948830189458628227828
80181199384826282014278194139940567587151170094390
35398664372827112653829987240784473053190104293586
86515506006295864861532075273371959191420517255829
71693888707715466499115593487603532921714970056938
54370070576826684624621495650076471787294438377604
53282654108756828443191190634694037855217779295145
36123272525000296071075082563815656710885258350721
45876576172410976447339110607218265236877223636045
17423706905851860660448207621209813287860733969412
81142660418086830619328460811191061556940512689692
51934325451728388641918047049293215058642563049483
62467221648435076201727918039944693004732956340691
15732444386908125794514089057706229429197107928209
55037687525678773091862540744969844508330393682126
18336384825330154686196124348767681297534375946515
80386287592878490201521685554828717201219257766954
78182833757993103614740356856449095527097864797581
16726320100436897842553539920931837441497806860984
48403098129077791799088218795327364475675590848030
87086987551392711854517078544161852424320693150332
59959406895756536782107074926966537676326235447210
69793950679652694742597709739166693763042633987085
41052684708299085211399427365734116182760315001271
65378607361501080857009149939512557028198746004375
35829035317434717326932123578154982629742552737307
94953759765105305946966067683156574377167401875275
88902802571733229619176668713819931811048770190271
25267680276078003013678680992525463401061632866526
36270218540497705585629946580636237993140746255962
24074486908231174977792365466257246923322810917141
91430288197103288597806669760892938638285025333403
34413065578016127815921815005561868836468420090470
23053081172816430487623791969842487255036638784583
11487696932154902810424020138335124462181441773470
63783299490636259666498587618221225225512486764533
67720186971698544312419572409913959008952310058822
95548255300263520781532296796249481641953868218774
76085327132285723110424803456124867697064507995236
37774242535411291684276865538926205024910326572967
23701913275725675285653248258265463092207058596522
29798860272258331913126375147341994889534765745501
18495701454879288984856827726077713721403798879715
38298203783031473527721580348144513491373226651381
34829543829199918180278916522431027392251122869539
40957953066405232632538044100059654939159879593635
29746152185502371307642255121183693803580388584903
41698116222072977186158236678424689157993532961922
62467957194401269043877107275048102390895523597457
23189706772547915061505504953922979530901129967519
86188088225875314529584099251203829009407770775672
11306739708304724483816533873502340845647058077308
82959174767140363198008187129011875491310547126581
97623331044818386269515456334926366572897563400500
42846280183517070527831839425882145521227251250327
55121603546981200581762165212827652751691296897789
32238195734329339946437501907836945765883352399886
75506164965184775180738168837861091527357929701337
62177842752192623401942399639168044983993173312731
32924185707147349566916674687634660915035914677504
99518671430235219628894890102423325116913619626622
73267460800591547471830798392868535206946944540724
76841822524674417161514036427982273348055556214818
97142617910342598647204516893989422179826088076852
87783646182799346313767754307809363333018982642090
10848802521674670883215120185883543223812876952786
71329612474782464538636993009049310363619763878039
62184073572399794223406235393808339651327408011116
66627891981488087797941876876144230030984490851411
60661826293682836764744779239180335110989069790714
85786944089552990653640447425576083659976645795096
66024396409905389607120198219976047599490197230297
64913982680032973156037120041377903785566085089252
16730939319872750275468906903707539413042652315011
94809377245048795150954100921645863754710598436791
78639167021187492431995700641917969777599028300699
15368713711936614952811305876380278410754449733078
40789923115535562561142322423255033685442488917353
44889911501440648020369068063960672322193204149535
41503128880339536053299340368006977710650566631954
81234880673210146739058568557934581403627822703280
82616570773948327592232845941706525094512325230608
22918802058777319719839450180888072429661980811197
77158542502016545090413245809786882778948721859617
72107838435069186155435662884062257473692284509516
20849603980134001723930671666823555245252804609722
53503534226472524250874054075591789781264330331690";
	
	var trimmed = input.Split('\n').Select(s => long.Parse(s.Substring(0, 12)));
	return long.Parse(trimmed.Sum().ToString().Substring(0, 10));
}

public static long Question125()
{
	long MAX_NUMBER = 100000000;
	int MAX_LOOP = 10000;
//	long MAX_NUMBER = 10000;
//	int MAX_LOOP = 1000;
	
	long outputSum = 0;
	List<long> outputList = new List<long>();
	
	//To get 10^8, since its squared just need to go to 10000
	for (int i = 1; i < MAX_LOOP; ++i)
	{
		Util.Progress = i * 100 / 10000;
		
		long squaresSum = i * i;
		for (int j = i+1; j < MAX_LOOP; ++j)
		{
			//Square each digit between i and j
			squaresSum += j * j;
			
			if (squaresSum > MAX_NUMBER)
			{
				break;
			}
//			if (squaresSum < 100)
//			{
//				continue;
//			}
			
			if (Helpers.IsPalindrome(squaresSum))
			{
				//squaresSum.Dump("FOUND: " + i + "-" + j);
				outputList.Add(squaresSum);
				outputSum += squaresSum;
			}
		}
	}
	
	outputList.OrderBy(i => i).Dump();
	return outputList.Distinct().Sum().Dump("YAY");
	//return outputSum;
}
public static long Question11()
{
string input = @"08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";
	
	int[] array = input.Split(' ', '\n').Select(s => Int32.Parse(s)).ToArray();
	
//	for (int i = 0; i < 20; ++i)
//	{
//		for (int j = 0; j < 20; ++j)
//		{
//			int pos = 20*i+j;
//			
//		}
//	}
	
	long hMax = 0;
	long vMax = 0;
	
	for (int i = 0; i < 20; ++i)
	{
		for (int j = 0; j < 17; ++j)
		{
			try
			{
				//Horizontal
				int pos = 20*i+j;
				long hcur = array[pos] * array[pos+1] * array[pos+2] * array[pos+3];
				if (hcur > hMax)
					hMax = hcur;
			}
			catch (Exception)
			{
				(20*i+j).Dump("H");
				throw;
			}
			
			try
			{
				//Vertical
				int pos = 20*j+i;
				long vcur = array[pos] * array[pos+20] * array[pos+40] * array[pos+60];
				if (vcur > vMax)
					vMax = vcur;
			}
			catch (Exception)
			{
				(20*j+i).Dump("V");
				throw;
			}
		}
	}
	
	long drMax = 0;
	long dlMax = 0;
	for (int i = 0; i < 17; ++i)
	{
		for (int j = 0; j < 17; ++j)
		{
			try
			{
				// Right to left
				int pos = 20*i+j;
				//Util.HorizontalRun(true, array[pos], array[pos+21], array[pos+42], array[pos+63]).Dump();
				long drcur = array[pos] * array[pos+21] * array[pos+42] * array[pos+63];
				if (drcur > drMax)
					drMax = drcur;
			}
			catch (Exception)
			{
				(20*j+i).Dump("D Right");
				throw;
			}
			
			try
			{
				// Right to left
				int pos = 20*(i+3)+j;
				//Util.HorizontalRun(true, array[pos], array[pos-19], array[pos-38], array[pos-57]).Dump();
				long dlcur = array[pos] * array[pos-19] * array[pos-38] * array[pos-57];
				if (dlcur > dlMax)
					dlMax = dlcur;
			}
			catch (Exception)
			{
				(20*j+i).Dump("D Left");
				throw;
			}
		}
	}
	
	hMax.Dump("H Max");
	vMax.Dump("V Max");
	drMax.Dump("DR Max");
	dlMax.Dump("DL Max");
	
	return -1;
}
public static long Question10()
{
//	var primes = Helpers.PrimesLessThan(2000000);
//	primes.Count.Dump("Prime Count");
////	long multiplier = 1;
////	foreach (int i in primes)
////	{
////		multiplier *= i;
////	}
////	return multiplier;
//	return primes.Sum();
	
//	int maxValue = 2000000;
//	long sum = 0;
//	
//	
//	int number = 2000;
//	List<int> output = new List<int>(number);
//	
//	output.Add(2);
//	
//	for (int i = 3;
//		i < int.MaxValue
//		&& i <= maxValue
//		; ++i, ++i)
//	{
//		bool hasFactor = false;
//		foreach (int j in output)
//		{
//			if (i % j == 0)
//			{
//				hasFactor = true;
//			}
//		}
//		if (!hasFactor)
//		{
//			output.Add(i);
//			sum += i;
//		}
//	}
//	
//	return sum;
	int MAX = 2000000;
	long sum = 2;
	for (int i = 3; i < MAX; ++i, ++i)
	{
		if (Helpers.isPrime_Q7PDF(i))
			sum += i;
	}
	return sum;
}
public static int Question9()
{
	//A can't be more than 333, since a < b < c && a + b + c = 1000
	for (int A = 1; A < 400; ++A)
	{
		//B cannot be more than half
		for (int B = A; B < 500; ++B)
		{
			for (int C = B; C < 1000; ++C)
			{
				if (A + B + C == 1000
					&& (A*A + B*B == C*C))
				{
					(A + " < " + B + " < " + C).Dump();
					return A * B * C;
				}
			}
		}
	}
	return -1;
}
public static int Question8()
{
	string longDigit = @"73167176531330624919225119674426574742355349194934
96983520312774506326239578318016984801869478851843
85861560789112949495459501737958331952853208805511
12540698747158523863050715693290963295227443043557
66896648950445244523161731856403098711121722383113
62229893423380308135336276614282806444486645238749
30358907296290491560440772390713810515859307960866
70172427121883998797908792274921901699720888093776
65727333001053367881220235421809751254540594752243
52584907711670556013604839586446706324415722155397
53697817977846174064955149290862569321978468622482
83972241375657056057490261407972968652414535100474
82166370484403199890008895243450658541227588666881
16427171479924442928230863465674813919123162824586
17866458359124566529476545682848912883142607690042
24219022671055626321111109370544217506941658960408
07198403850962455444362981230987879927244284909188
84580156166097919133875499200524063689912560717606
05886116467109405077541002256983155200055935729725
71636269561882670428252483600823257530420752963450";
	
	var charString = (from char c in longDigit
					 where c != '\n'
					 select c - '0').ToList();
	
//	int window = 1;
//	for (int i = 0; i < 5; ++i)
//	{
//		window *= charString[i];
//	}
//	int highest = window;
//	for (int i = 0, j = 5; j < charString.Count; ++i, ++j)
//	{
//		if (window == 0) window = 1;
//		if (charString[i] == 0)
//		{
//			window = 1;
//			continue;
//		}
//		window = window / charString[i] * charString[j];
//		//window.Dump(i + "-" + j);
//		if (window > highest)
//			highest = window;
//	}
	int highest = 0;
	
	int[] window = new int[5];
	int windowPos = 0;
	int skip = 5;
	for (int i = 0; i < charString.Count; ++i)
	{
		window[windowPos] = charString[i];
		if (window[windowPos] == 0) skip = 5;
		
		if (skip > 0) --skip;
		if (skip == 0)
		{
			int windowProduct = window[0] * window[1] * window[2] * window[3] * window[4];
			if (windowProduct > highest) highest = windowProduct;
		}
		
		if (++windowPos >= window.Length) windowPos = 0;
	}
	return highest;
}
public static int Question7()
{
	//7 seconds... TODO better
	return Helpers.FirstXPrimes(10001).Last();
}
public static long Question6()
{
	//Fucking easy wtf
	int MAX = 100;
	long sumOfSquares = 0;
	long squareOfSums = 0;
	for (int i = 1; i <= MAX; ++i)
	{
		sumOfSquares += i * i;
		squareOfSums += i;
	}
	squareOfSums *= squareOfSums;
	
	return squareOfSums - sumOfSquares;
}
public static int Question5()
{
	int MAX = 20;
//	int output = 1;
//	for (int i = 1; i <= MAX; ++i)
//	{
//		int m = i;
//		for (int j = 2; j < i; ++j)
//		{
//			if (m % j == 0)
//			{
//				m = m / j;
//				//--j;
//			}
//		}
//		output = output * m;
//	}
	
	List<int> denominators = new List<int>();
	//List<int> temp = null;
	for (int i = MAX; i > 0; --i)
	{
		int temp = i;
		foreach (int d in denominators)
		{
			if (temp % d == 0)
			{
				temp /= d;
			}
		}
		for (int j = 2; j <= i; ++j)
		{
			if (temp % j == 0)
			{
				denominators.Add(j);
				temp = temp / j;
				--j;
			}
		}
	}
	int output = 1;
	denominators.Dump();
	foreach (int d in denominators)
	{
		output *= d;
	}
	
	//Check:
	for (int i = 1; i <= MAX; ++i)
	{
		if (output % i != 0)
		{
			i.Dump("Fail");
			//output *= 2;
			//--i;
		}
	}
	
	//Works, probably high: 1396755360
	//Might work:			 155195040
	//Good!:				 232792560
	
	return output;
}
public static int Question4()
{
	int LOW = 99;
	int biggestFound = 0;
	for (int i = 999; i > LOW; --i)
	{
		for (int j = i; j > LOW; --j)
		{
			//Added after reading pdf:
			if ((i * j) > biggestFound)
			{
			if (Question4_isPalendrome(i * j))
			{
				(i + " * " + j + " = " + i * j).Dump();
				//return i * j;
				if (biggestFound < (i * j))
				{
					biggestFound = i * j;
				}
				LOW = j;
			}
			}
		}
	}
	return biggestFound;
}
public static bool Question4_isPalendrome(int num)
{
	string str = num.ToString();
	for (int i = 0; i < (str.Length+1 / 2); ++i)
	{
		//(str + "--" + i.ToString()).Dump();
		if (str[i] != str[str.Length - 1 - i]) return false;
	}
	return true;
}
public static long Question3()
{
	//long Factor = 13195;
	long Factor = 600851475143;
	
	//if (topLimit % 2 != 0) //Skipping all Even Numbers already
	while (true)
	{
		//bool changed = false;
		long topLimit = Factor / 2;
		for (long f = 3; f < topLimit; ++f, ++f)
		{
			if (Factor % f == 0)
			{
				Factor = Factor / f;
				if (Factor == 1) return f;
				//changed = true;
			}
		}
		//if (!changed) return Factor;
	}
}
public static long Question3_fail()
{
	//long Factor = 13195;
	long Factor = 600851475143;
	
	//Can't be more than half:
	long topLimit = Factor / 16;

	long NUMBER = topLimit.Dump();
	
	for (; topLimit > 0; --topLimit, --topLimit)
	{
		if (Factor % topLimit != 0)
		{
			continue;
		}
		
		Util.Progress = (int)((NUMBER - topLimit) *100 / NUMBER);
		//topLimit.Dump();
		
		//Check if this has any factors
		bool hasFactor = false;
		//if (topLimit % 2 != 0) //Skipping all Even Numbers already
		for (long f = 3; f < topLimit; ++f, ++f)
		{
			if (topLimit % f == 0)
			{
				hasFactor = true;
				break;
			}
		}
		if (!hasFactor)
		{
			return topLimit;
		}
	}
	return -1;
}
public static int Question2()
{
//	int NumA = 1;
//	int NumB = 2;
//	bool EvenA = false;
//	bool EvenB = true;
	
	int[] Num = new int[2] { 1, 2 };
	bool[] Even = new bool[2] { false, true };
	int pointer = 1;
	
	int MAX_VALUE = 4000000;
	//int MAX_VALUE = 90;
	
	int sum = 2;
	
	while (true)
	{
		pointer = 1-pointer;
		
//		var display = new[] { new { a = Num[0], b = Even[0] }, new { a = Num[1], b = Even[1] } };
//		display.Dump();
		
		Num[pointer] = Num[0] + Num[1];
		Even[pointer] = !(Even[0] ^ Even[1]);
		
		
		if (Num[pointer] > MAX_VALUE)
		{
			return sum;
		}
		if (Even[pointer])
		{
			sum += Num[pointer].Dump();
		}
	}
}
public static int Question1()
{
	int sum = 0;
	for (int i = 0; i < 1000; i += 3)
	{
		sum += i;
	}
	for (int i = 0; i < 1000; i += 5)
	{
//		if (i % 3 != 0)
			sum += i;
	}
	for (int i = 0; i < 1000; i += 15)
	{
		sum -= i;
	}
	return sum;
}


public static class Helpers
{
	public static List<int> FirstXPrimes(int maxCount)
	{
//		int[] output = new int[number];
//		int cout = 0;
		List<int> output = new List<int>(maxCount);
		
		output.Add(2);
		
		for (int i = 3;
			i < int.MaxValue
			&& output.Count < maxCount
			; ++i, ++i)
		{
			bool hasFactor = false;
			foreach (int j in output)
			{
				if (i % j == 0)
				{
					hasFactor = true;
				}
			}
			if (!hasFactor) output.Add(i);
		}
		
		return output;
	}
	public static List<int> PrimesLessThan(int maxValue)
	{
		int number = 200;
		List<int> output = new List<int>(number);
		
		output.Add(2);
		
		for (int i = 3;
			i < int.MaxValue
			&& i <= maxValue
			; ++i, ++i)
		{
//			bool hasFactor = false;
//			foreach (int j in output)
//			{
//				if (i % j == 0)
//				{
//					hasFactor = true;
//				}
//			}
//			if (!hasFactor) output.Add(i);
			if (isPrime_Q7PDF(i)) output.Add(i);
		}
		
		return output;
	}
	
	public static bool IsPrime(int num)
	{
		//("Check: " + num).Dump();
		
		if (num == 1) return false;
		if (num == 2) return true;
		if (num % 2 == 0) return false;
		
		int topLimit = num / 2;
		for (long f = 3; f < topLimit; ++f, ++f)
		{
			if (num % f == 0)
			{
				//("Factor: " + f).Dump();
				return false;
			}
		}
		return true;
	}
	public static bool isPrime_Q7PDF(int n)
	{
		if (n == 1) return false;
		else if (n < 4) return true;	//2 and 3 are prime
		else if (n % 2 == 0) return false;
		else if (n < 9) return true;	//we have already excluded 4,6 and 8.
		else if (n % 3 == 0) return false;
		else
		{
			int r = (int)Math.Floor(Math.Sqrt(n));	// sqrt(n) rounded to the greatest integer r so that r*r<=n
			for (int f = 5; f <= r; f = f+6)
			{
				if (n % f == 0) return false;
				if (n % (f + 2) == 0) return false;
			}
		}
		return true;
	}
	public static int GetAFactor_Basedon_Q7PDF(int n)
	{
		if (n == 1) return 1;
		else if (n < 4) return 1;	//2 and 3 are prime
		else if (n % 2 == 0) return 2;
		else if (n < 9) return 1;	//we have already excluded 4,6 and 8.
		else if (n % 3 == 0) return 3;
		else
		{
			int r = (int)Math.Floor(Math.Sqrt(n));	// sqrt(n) rounded to the greatest integer r so that r*r<=n
			for (int f = 5; f <= r; f = f+6)
			{
				if (n % f == 0) return f;
				if (n % (f + 2) == 0) return f;
			}
		}
		return 1;
	}
	
	public static bool IsPalindrome(long num)
	{
		int length = 0;
		int tens = 1;
		long temp = num;
		while (temp > 0)
		{
			++length;
			tens *= 10;
			temp /= 10;
		}
		
		tens /= 10;
		temp = num;
		
		int z = (length+1)/2;
		for (int i = 0; i < z; ++i)
		{
			long highNum = num / tens % 10;
			long lowNum = temp % 10;
			//(highNum + " " + lowNum).Dump();
			if (highNum != lowNum) return false;
			
			tens /= 10;
			temp /= 10;
		}
		
		return true;
	}
	public static long GetDigit(long num, int digit)
	{
		long m = 1;
		for (int i = 1; i < digit; ++i)
			m *= 10;
		
		return num / m % 10;
	}
	public static long SumDigits(long t)
	{
		long sum = 0;
		while (t > 0)
		{
			sum += t % 10;
			t /= 10;
		}
		return sum;
	}
	
	public static long Factorial(int num)
	{
		long output = 1;
		for (int i = 1; i <= num; ++i)
		{
			output *= i;
		}
		return output;
	}
}
public class BigFib
{
	const int TRUNC_SIZE = 100000;
	const int TRUNC_LENGTH = 5;
	
//	List<int> arrayN1 = new List<int>();
//	List<int> arrayN2 = new List<int>();
	List<long> arrayN1 = new List<long>();
	List<long> arrayN2 = new List<long>();
	bool arrayN1IsCurrent = true;
	long sequenceNumber = 1;
	
	public BigFib()
	{
		arrayN1.Add(1);
		arrayN2.Add(1);
	}
	
	public long Next()
	{
		if (arrayN1IsCurrent)
		{
		}
		else
		{
		}
		arrayN1IsCurrent = 1 - arrayN1IsCurrent;
		
		return ++sequenceNumber;
	}
	public long ExtimateLength()
	{
	}
}
[Obsolete("Untested", true)]
public class InfiniteInt
{
	const int TRUNC_SIZE = 10;
	const int TRUNC_LENGTH = 1;
//	const int TRUNC_SIZE = 100000;
//	const int TRUNC_LENGTH = 5;
	List<int> array = new List<int>();
	
	public InfiniteInt(int start)
	{
		array.Add(start);
	}
	
	public void Add(InfiniteInt num)
	{
		
	}
	public void Add(int num)
	{
		for (int j = 0; j < array.Count; ++j)
		{
			array[j] += num;
			if (array[j] >= TRUNC_SIZE)
			{
				num = array[j] / TRUNC_SIZE;
				array[j] %= TRUNC_SIZE;
			}
			else
			{
				return;
			}
		}
//		int pos = array.Count-1;
//		if (array[pos] + num >= TRUNC_SIZE) array.Add(0);
//		
//		for (int j = pos; j >= 0; --j)
//		{
//			array[j] += num;
//			if (array[j] >= TRUNC_SIZE)
//			{
//				array[j+1] += array[j] / TRUNC_SIZE;
//				array[j] %= TRUNC_SIZE;
//			}
//		}
	}
	public void Multiply(int factor)
	{
		int pos = array.Count-1;
		if (array[pos] * factor >= TRUNC_SIZE) array.Add(0);
		
		for (int j = pos; j >= 0; --j)
		{
			array[j] *= factor;
			if (array[j] >= TRUNC_SIZE)
			{
				array[j+1] += array[j] / TRUNC_SIZE;
				array[j] %= TRUNC_SIZE;
			}
		}
	}
	public long Length()
	{
		long filled = (array.Count-1) * TRUNC_LENGTH;
		return filled + Helpers.SumDigits(array[array.Count-1]);
	}
	public long SumDigits()
	{
		//Sum them up
		int sum = 0;
		for (int i = 0; i < array.Count; ++i)
		{
			int t = array[i];
			while (t > 0)
			{
				sum += t % 10;
				t /= 10;
			}
		}
		return sum;
	}
}