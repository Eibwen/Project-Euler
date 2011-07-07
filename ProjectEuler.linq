<Query Kind="Program" />

void Main()
{
	//TODO test List<int> PrimesLessThan(int maxValue) on laptop vs work, switch it to long?
	Problem34().Dump("Result");
	//InfiniteIntTest();
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
	
	if (!Helpers.isPrime_Q7PDF(num)) return Truncatable.None;
	
	//num.Dump();
	
	int length = 0;
	Truncatable trun = Truncatable.None;
	
	int numb = num;
	bool isRight = true;
	while (numb > 10)
	{
		++length;
		numb /= 10;
		
		if (!Helpers.isPrime_Q7PDF(numb))
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
		if (!Helpers.isPrime_Q7PDF(numb % Modifier))
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


public static long Problem36()
{
	long outputSum = 0;
	char[] base2 = new char[] { '0', '1' };
	//Can skip even numbers since it cannot include leading zeros (so base2 has to begin/end with 1 == odd)
	for (int i = 1; i < 1000000; ++i, ++i)
	{
		if (Helpers.IsPalindrome(i) && Helpers.IsPalindrome(Problem36_IntToStringFast(i, base2)))
		{
			Util.HorizontalRun(true, i, Problem36_IntToStringFast(i, base2)).Dump();
			outputSum += i;
		}
	}
	return outputSum;
}
//GOT THIS FROM: http://stackoverflow.com/questions/923771/quickest-way-to-convert-a-base-10-number-to-any-base-in-net/923814#923814
/// <summary>
/// An optimized method using an array as buffer instead of 
/// string concatenation. This is faster for return values having 
/// a length > 1.
/// </summary>
public static string Problem36_IntToStringFast(int value, char[] baseChars)
{
	// 32 is the worst cast buffer size for base 2 and int.MaxValue
	int i = 32;
	char[] buffer = new char[i];
	int targetBase = baseChars.Length;

	do
	{
		buffer[--i] = baseChars[value % targetBase];
		value = value / targetBase;
	}
	while (value > 0);

	char[] result = new char[32 - i];
	Array.Copy(buffer, i, result, 0, 32 - i);

	return new string(result);
}
public static long Problem56()
{
	long maxSum = 0;
	
	for (int a = 1; a < 100; ++a)
	{
		for (int b = 1; b < 100; ++b)
		{
			InfiniteInt ii = new InfiniteInt(a);
			for (int i = 1; i < b; ++i)
			{
				ii.Multiply(a);
			}
			long sum = ii.SumDigits();
			if (sum > maxSum)
			{
				maxSum = sum;
			}
		}
	}
	
	return maxSum;
}

#region Problem59
public static int Problem59() //After reading forum, used common stragiety but all coded by me, correct solution
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem59_cipher1.txt");
	var chars = from c in File.ReadAllText(PATH).Split(',')
				select Int32.Parse(c);
	
	List<int> encrypted = chars.ToList();
	
	int MAX = 95;
	
	int[] set1 = new int[MAX];
	int[] set2 = new int[MAX];
	int[] set3 = new int[MAX];
	
//	int highest = 0;
	
	for (int i = 0; i < encrypted.Count; ++i)
	{
		switch (i % 3)
		{
			case 0:
				set1[encrypted[i]]++;
				break;
			case 1:
				set2[encrypted[i]]++;
				break;
			case 2:
				set3[encrypted[i]]++;
				break;
		}
//		if (highest < encrypted[i])
//		{
//			highest = encrypted[i];
//		}
	}
	
//	highest.Dump();
//	set1.Dump();
//	set2.Dump();
//	set3.Dump();
	
	int BestChar = ' ';
	
	int k1 = Problem59_MostCommonIndex(set1) ^ BestChar;
	int k2 = Problem59_MostCommonIndex(set2) ^ BestChar;
	int k3 = Problem59_MostCommonIndex(set3) ^ BestChar;
	
	int decodedSum = Problem59_Test(k1, k2, k3).Select(s => (int)s).Sum();
	
	Util.HorizontalRun(false, (char)k1, (char)k2, (char)k3, " -- ",
								decodedSum).Dump();
	
	return decodedSum;
}
public static int Problem59_MostCommonIndex(int[] intArray)
{
	int maxVal = intArray[0];
	int maxIndex = 0;
	for (int i = 1; i < intArray.Length; ++i)
	{
		if (intArray[i] > maxVal)
		{
			maxVal = intArray[i];
			maxIndex = i;
		}
	}
	return maxIndex;
}
public static long Problem59_MySolution()
{
	//Sorta cheated, in that i used my human brain to select possible matches
	// Output each decoded character, selected what characters might possibly start a text, did this with the first 3 chars (thats what testChar#.Dump() are for)
	// Output decoded first 12 bytes for every possible group
	// Glanced through those, found 2 decent matches
	//  Filtered out any with odd characters anywhere within those 12, then increased it to 15, first characters
	//  Finally now it is outputting 16 possible keys, with only one being actual text, but code does not determine that -- sorta cheating
	
	
	//Forum Strageties
	// 20040723 rayfil:  I must admit that I "cheated" a bit on this one. The first part of my algo was to separate the text into three arrays where the frequency of each character was accumulated. Using my debugger showed me those frequencies and XORing those most frequent characters with 20h (space character) was a giveaway to the key. I then completed the program without writing any code to find the key. 
	// 20040723 bishwa:  My approach was to genearte frequencies of repeating 3 letter patterns. Then code word "the" with a candidate key (including cycles of the key) an compare to 10 most frequent patterns and see if it matched. If it did then decoded first 20 characters of supplied text to see if any made sense. 
	// 20040726 bitRAKE:  I wrote the three character decode algorithm and an ASCII text filter to weed out uncommon bytes in x86. I let the key increment until the decoded text passed the filter. There were only a couple of false decodes - which could plainly be seen with the naked eye, and the key was incremented further. 
	// 20040726 md2perpe:  I looped through all possible keys, summing the number of E:s, A:s, N:s and T:s (the four most common letters in English, I think) to a score. The key giving the highest score was the used encryption key.
	// 20041024 Xaphiosis [has code]:  More hacking ... I looked through all lowercase password possibilities for the three password letters: a,b,c
	//								   Where an application of XOR generated an unwanted character, I removed it from the possibilities. I was expecting to have a few possible passwords left from this, and imagine my surprise when this script printed: 
	// 20050314 bartmeijer [has code]: 
	
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem59_cipher1.txt");
	var chars = from c in File.ReadAllText(PATH).Split(',')
				select Int32.Parse(c);
	
	int a = 'a';
	int z = 'z';
	
////	//Trying to limit the char sets needed to be checked
////	string validChars = "abcdefghijklmnopqrstuvxyz.!? -()";
////	int CHARLEN = 26;
////	bool[] key1Mask = new bool[CHARLEN];
////	bool[] key2Mask = new bool[CHARLEN];
////	bool[] key3Mask = new bool[CHARLEN];
////	foreach (int letter in chars)
////	{
////		for (int k1 = a; k1 <= z; ++k1)
////		{
////			char c = (char)(letter ^ k1);
////			if (!validChars.Contains(c))
////			{
////				switch (k1 % 3)
////				{
////					case 0:
////						key1Mask[k1 - a] = true;
////						break;
////					case 1:
////						key2Mask[k1 - a] = true;
////						break;
////					case 2:
////						key3Mask[k1 - a] = true;
////						break;
////				}
////			}
////		}
////	}
////	
////	string k1charset = "";
////	for (int i = 0; i < CHARLEN; ++i)
////		if (!key1Mask[i]) k1charset += (char)(i+a);
////	string k2charset = "";
////	for (int i = 0; i < CHARLEN; ++i)
////		if (!key2Mask[i]) k2charset += (char)(i+a);
////	string k3charset = "";
////	for (int i = 0; i < CHARLEN; ++i)
////		if (!key3Mask[i]) k3charset += (char)(i+a);
////	k1charset.Dump();
////	k2charset.Dump();
////	k3charset.Dump();
////	string.Format("From {0} down to {1}", CHARLEN*CHARLEN*CHARLEN, k1charset.Length * k2charset.Length * k3charset.Length).Dump();
////	//== From 17576 down to 5202
	
	
	
//	char testChar = '.';
//	if (!(('A' <= testChar && 'Z' >= testChar)
//		|| ('a' <= testChar && 'z' >= testChar)))
//	{
//		"SKIP".Dump();
//	}
	
	//string validK1 = "-('\"! ?=:98765";
	string validK1 = "(\"";
	
	int dumpCount = 0;
	
	List<int> checkChars = chars.Take(15).ToList();
	for (int k1 = a; k1 <= z; ++k1)
	{
		char testChar1 = (char)(k1 ^ checkChars[0]);
//		testChar1.Dump();
//		continue;
		if (!validK1.Contains(testChar1)) continue;
		
		for (int k2 = a; k2 <= z; ++k2)
		{
			char testChar2 = (char)(k2 ^ checkChars[1]);
//			testChar2.Dump();
//			continue;
			if (!('A' <= testChar2 && 'Z' >= testChar2)) continue;
			
			for (int k3 = a; k3 <= z; ++k3)
			{
				char testChar3 = (char)(k3 ^ checkChars[2]);
//				testChar3.Dump();
//				continue;
				if (!(('a' <= testChar3 && 'z' >= testChar3) || testChar3 == ' ')) continue;
				
				string testStr = "";
				for (int i = 0; i < checkChars.Count; ++i)
				{
					switch (i % 3)
					{
						case 0:
							testStr += (char)(k1 ^ checkChars[i]);
							break;
						case 1:
							testStr += (char)(k2 ^ checkChars[i]);
							break;
						case 2:
							testStr += (char)(k3 ^ checkChars[i]);
							break;
					}
				}
				if (!testStr.Contains("&")
					&& !testStr.Contains("#")
					&& !testStr.Contains("~")
					&& !testStr.Contains("@")
					&& !testStr.Contains("}")
					&& !testStr.Contains("{")
					&& !testStr.Contains("%")
					&& !testStr.Contains("`")
					&& !testStr.Contains("$")
					&& !testStr.Contains("*")
					&& !testStr.Contains("+")
					&& !testStr.Contains("=")
					&& !testStr.Contains("|")
					&& !testStr.Contains("<")
					&& !testStr.Contains(">")
					&& !testStr.Contains(":")
					&& !testStr.Contains(";")
					&& !testStr.Contains("/")
					
					&& !testStr.Contains("'")
					&& !testStr.Contains("\"")
					&& !testStr.Contains("!")
					&& !testStr.Contains("?"))
//				if (testStr.ToLower().Contains("the")
//					|| testStr.ToLower().Contains("of")
//					|| testStr.ToLower().Contains("it"))
				{
					//testStr.Dump();
					Util.HorizontalRun(false, (char)k1, (char)k2, (char)k3, " : '", testStr, "'", " -- ",
												Problem59_Test(k1, k2, k3).Select(s => (int)s).Sum()).Dump();
					++dumpCount;
				}
//				//if (testStr == "\"Tho Gespol ")
//				if (testStr == " (The Gospel  ")
//				{
//					Util.HorizontalRun(false, (char)k1, (char)k2, (char)k3).Dump("This maybe");
//					Problem59_Test(k1, k2, k3).Dump();
//				}
			}
		}
	}
	
	dumpCount.Dump("DUMPED");
	
	string solved = Problem59_Test('g', 'o', 'd').Dump();
	
	return solved.Select(s => (int)s).Sum();
}
public static string Problem59_Test(int k1, int k2, int k3)
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem59_cipher1.txt");
	var chars = from c in File.ReadAllText(PATH).Split(',')
				select Int32.Parse(c);
	
	List<int> encrypted = chars.ToList();
	
	StringBuilder sb = new StringBuilder();
	for (int i = 0; i < encrypted.Count; ++i)
	{
		switch (i % 3)
		{
			case 0:
				sb.Append((char)(encrypted[i] ^ k1));
				break;
			case 1:
				sb.Append((char)(encrypted[i] ^ k2));
				break;
			case 2:
				sb.Append((char)(encrypted[i] ^ k3));
				break;
		}
	}
	return sb.ToString();
}
#endregion Problem59

public static long Problem52()
{
	int MAX = 1000000;
	
	for (int i = 10; i <= MAX; ++i)
	{
		int goodCheckSum = Problem52_CheckSumDigits(i);
		if (goodCheckSum == Problem52_CheckSumDigits(2*i)
			&& goodCheckSum == Problem52_CheckSumDigits(3*i)
			&& goodCheckSum == Problem52_CheckSumDigits(4*i)
			&& goodCheckSum == Problem52_CheckSumDigits(5*i)
			&& goodCheckSum == Problem52_CheckSumDigits(6*i))
		{
			return i;
		}
	}
	
	return -52;
}
public static int Problem52_CheckSumDigits(int a) //Based on Question32_CheckSumDigits(int a, int b, int c)
{
	int chkSum = 0;
	int chkSum2 = 1;
	while (a > 0)
	{
		//chkSum += (a % 10) ^ 23;	// Had 1138 matches
		chkSum += (a % 10);
		chkSum2 *= (a % 10) ^ 23;		// + Had  966 matches, * had 55, with ^23 had 9
		a /= 10;
	}
//	while (b > 0)
//	{
//		//chkSum += (b % 10) ^ 23;
//		chkSum += (b % 10);
//		chkSum2 *= (b % 10) ^ 23;
//		b /= 10;
//	}
//	while (c > 0)
//	{
//		//chkSum += (c % 10) ^ 23;
//		chkSum += (c % 10);
//		chkSum2 *= (c % 10) ^ 23;
//		c /= 10;
//	}
	return chkSum + chkSum2;
}
public static long Problem23()
{
	//Runtime: 10.5 seconds on laptop
	// Now using Helpers.sum_of_divisors(i) it runs in 0.15 seconds
	
	int MAX = 28123;
	
	//"Build Number List".Dump();
	//Build Abundant number list
	List<int> abundantNums = new List<int>();
	for (int i = 1; i <= MAX; ++i)
	{
//		if (i < Helpers.GetProperDivisorSum(i))
//			abundantNums.Add(i);
		if (2*i < Helpers.sum_of_divisors(i))
			abundantNums.Add(i);
	}
	
	//"Build Sieve".Dump();
	//Setup Sieve
	bool[] sieve = new bool[MAX];
	for (int i = 0; i < abundantNums.Count; ++i)
	{
		int iNum = abundantNums[i];
		for (int j = i; j < abundantNums.Count; ++j)
		{
			int index = iNum + abundantNums[j];
			if (index >= sieve.Length) break;
			
			sieve[index] = true;
		}
	}
	
	//"Sum Numbers".Dump();
	//Sum numbers
	long outputSum = 0;
	for (int i = 0; i < sieve.Length; ++i)
	{
		if (!sieve[i]) outputSum += i;
	}
	
	return outputSum;
}
public static long Problem38()
{
	bool zeroOffset = false;
	int offset = zeroOffset ? 0 : 1;
	
	int length = 9;
	
	int correct = 0;
	for (int i = 0; i < length; ++i)
	{
		correct |= 1 << (i+offset);
	}
	
	int bestN = 0;
	int bestToM = 0;
	long bestConcat = 0;
	
	for (int n = 1; n < 100000; ++n)
	{
		int checkMask = 0;
		
		long concatNum = 0;
		
		bool breakN = false;
		for (int m = 1; m <= 9; ++m)
		{
			int num = n * m;
			int numOffset = 1;
			while (num > 0)
			{
				int digit = num % 10;
				if ((checkMask & 1 << digit) > 0 || digit == 0) breakN = true;
				checkMask |= 1 << digit;
				num /= 10;
				numOffset *= 10;
			}
			if (breakN) break;
			concatNum = concatNum * numOffset + n * m;
			//checkMask.Dump();
			
			if (correct == checkMask)
			{
				if (concatNum > bestConcat)
				{
					bestConcat = concatNum;
					bestN = n;
					bestToM = m;
				}
			}
		}
		if (breakN) continue;
	}
	
	Util.HorizontalRun(true, bestN, bestToM, bestConcat).Dump();
	
	return bestConcat;
}
public static bool Problem38_IsPandigital(int num, int length, bool zeroOffset)
{
	int offset = zeroOffset ? 0 : 1;
	
	//bool[] checkMask = new bool[length+offset];
	int correct = 0;
	for (int i = 0; i < length; ++i)
	{
		correct |= 1 << (i+offset);
	}
	//correct.Dump();
	int checkMask = 0;
	
	for (int i = 0; i < length; ++i)
	{
		if ((checkMask & 1 << (num % 10)) > 0) return false;
		checkMask |= 1 << (num % 10);
		num /= 10;
	}
	//checkMask.Dump();
	
	return correct == checkMask;
}
public static int Problem39()
{
	// a < b < c
	// x - a + b = c
	
	int bestI = 0;
	int bestCount = 0;
	
	for (int i = 1; i <= 1000; ++i)
	{
		int curCount = 0;
		for (int a = 1; a < (i/3); ++a)
		{
			for (int b = a+1; b < (i/2); ++b)
			{
				int c = i - (a + b);
				if (c < 0)
				{
					//Testing my limits
					Util.HorizontalRun(true, "i:", i, "a:", a, "b:", b, "c:", c).Dump();
					break;
				}
				if (a*a + b*b == c*c)
				{
					++curCount;
				}
			}
		}
		
		if (curCount > bestCount)
		{
			bestCount = curCount;
			bestI = i;
		}
	}
	
	return bestI;
}
public static long Problem40()
{
	int outputProduct = 1;
	
	int length = 0;
	int lenMask = 1;
	int getMask = 1;
	int pos = 0;
	for (int i = 1; i < 1000000; ++i)
	{
		if (i == lenMask)
		{
			lenMask *= 10;
			length += 1;
		}
		pos += length;
		//pos.Dump();
		if (pos >= getMask)
		{
			int p = pos;
			int ii = i;
			while (p > getMask)
			{
				--p;
				ii.Dump();
				ii /= 10;
			}
			outputProduct *= ii % 10;
			(ii % 10).Dump(p.ToString());
			
			getMask *= 10;
		}
	}
	
	return outputProduct;
}
public static long Problem41()
{
	long outputPrime = 0;
	int LENGTH = 9;
	
	for (int i = LENGTH; i > 0; --i)
	{
		HelperEnumerators.LexicographicPermutations per = new HelperEnumerators.LexicographicPermutations(i, false);
		
		do
		{
			long d = per.CurrentLong;
			
			if (Helpers.isPrime_Q7PDF(d))
			{
				//d.Dump();
				outputPrime = d;
				//Don't break, want the largest one
			}
		}
		while (per.MoveNext());
		
		if (outputPrime > 0) return outputPrime;
	}
	
	return -41;
}
public static long Problem43()
{
	HelperEnumerators.LexicographicPermutations per = new HelperEnumerators.LexicographicPermutations(10);
	
	long outputSum = 0;
	do
	{
		long d = per.CurrentLong;
		
		if (d % 1000 % 17 == 0
			&& d / 10 % 1000 % 13 == 0
			&& d / 100 % 1000 % 11 == 0
			&& d / 1000 % 1000 % 7 == 0
			&& d / 10000 % 1000 % 5 == 0
			&& d / 100000 % 1000 % 3 == 0
			&& d / 1000000 % 1000 % 2 == 0)
		{
			outputSum += d;
		}
	}
	while (per.MoveNext());
	
	return outputSum;
}
public static long Problem44()
{
//	int n = 117;
//	double pent = n*(3*n-1)/2;
//	pent.Dump();
	
//	int n = 26756;
//	double pent = (double)n*(3*n-1)/2;
//	double sn = Math.Sqrt((2*pent+1)/3) + 0.5;
//	Util.HorizontalRun(true, n, ":", pent, ":", sn).Dump();
//	return -3;
	
//	for (int n = 1; n < 1000000; ++n)
//	{
//		long pent = (long)n*(3*n-1)/2;
////		//if (double.IsNaN(pent)) break;
////		double sn = Math.Sqrt((2*pent+1)/3) + 0.5;
////		//if (double.IsNaN(sn)) break;
////		if (n != (int)sn) Util.HorizontalRun(true, n, ":", sn).Dump();
//		if (!Problem44_IsPentagonal(pent))
//		{
//			Util.HorizontalRun(true, n, ":", pent).Dump("FAIL");
//		}
//	}
	int MAX = 10000;
	
	long minD = long.MaxValue;
	int highN = 0;
	for (int n = 1; n < MAX; ++n)
	{
		long pentN = (long)n*(3*n-1)/2;
		for (int i = n+1; i < MAX; ++i)
		{
			long pentI = (long)i*(3*i-1)/2;
			long D = pentI - pentN;
			
			//This check cut it from 12.6 seconds to 3.6 on laptop for MAX = 10000
			if (D > minD) break;
			
			if (Problem44_IsPentagonal(D)
				&& Problem44_IsPentagonal(pentI + pentN))
			{
				if (D < minD)
				{
					minD = D;
					highN = n;
				}
			}
		}
	}
	//highN.Dump();
	return minD;
}
public static bool Problem44_IsPentagonal(long pent)
{
	double sn = Math.Sqrt((2d*pent+1)/3) + 0.5;
	long n = (long)sn;
	if (pent == (n*(3*n-1)/2))
	{
		return true;
	}
	return false;
}
public static long Problem45()
{
	//The problem tells us that T285 = P165 = H143 = 40755.  So start at H144
	// Hex-to-Pent == 15/13 for given example
	//		fail: (x*(2*x-1))*6/8 == x*(3*x-1)/2
	//		((x*((2*x)-1))*3+x)/4 == x*(3*x-1)/2
	// Hex-to-Tri == 2h-1 == THIS IS PERFECT (FOR INTS ANYWAY)
	int MAX = 100000;
	for (int h = 143; h < MAX; ++h)
	{
		Util.Progress = h * 100 / MAX;
		long hexNum = h*(2*h-1);
		
		long estP = h*15/13;
		//double estP = (3*h-1)/2-((h+4)/3);
		long pentNum = estP*(3*estP-1)/2;
		//if (pentNum >= hexNum) "FAIL".Dump();
		for (; pentNum < hexNum; ++estP)
		{
			pentNum = estP*(3*estP-1)/2;
		}
		
		long estT = h*2-1;
		long triNum = estT*(estT+1)/2;
		//Util.HorizontalRun(true, hexNum, pentNum, triNum).Dump();
		if (hexNum != triNum) "TRI FAIL".Dump();
		if (pentNum == hexNum)
		{
			Util.HorizontalRun(true, hexNum, pentNum, triNum).Dump("SUCCESS");
			if (hexNum != 40755) return hexNum;
		}
		
//		int x = h;
//		Util.HorizontalRun(true, (x*((2*x)-1))*(double)3/4+((double)x/4),
//								((x*((2*x)-1))*3+x)/4,
//								x*(3*x-1)/2).Dump();
	}
	return -45;
}
public static long Problem46()
{
	//CHEATED due to two bugs that caused false results for runs upto 1,000,000,000 (that ran in like 4 minutes if i remember right)
	//			(2,000,000,000 caused computer to run out of memory and fail)
	//  Tho i did find that i*i+p is true at least upto 1 trillion i believe
	
	//56927025
	//int.max = 2147483647
	int MAX =   10000000;
	int MAX_SQUARE = 400;
	
	//Get cached lists
	List<int> Primes = Helpers.PrimesLessThan(MAX);
	bool[] MarkedSieve = Helpers.GetPrimeSieve(MAX); //NOTE: This calls GetPrimeSieve... so we are building that twice
	"Built Sieves".Dump();
	//bool[] MarkedSieve = new bool[MAX];
	List<int> Squares = new List<int>(MAX_SQUARE);
	for (int i = 1; i <= MAX_SQUARE; ++i)
	{
		//BUG2: after cheated, the question is two times the square
		Squares.Add(i*i*2);
	}
	//Squares.Dump();
	
	"marking".Dump();
	//Start marking off primes... brute force
	for (int i = 0; i < Primes.Count; ++i)
	{
		Util.Progress = i * 100 / Primes.Count;
		foreach (int square in Squares)
		{
			//Primes are marked as false, so mark any odd composites back to false
			//Primes[i].Dump();
			//BUG1: cheated to find, only process evens here
			if ((square + Primes[i]) % 2 == 0) continue;
			int index = (square + Primes[i]-1)/2;
			if (index >= MarkedSieve.Length) break;
			//if (MarkedSieve[index]) Util.HorizontalRun(true, (2*index+1), "=", square, "+", Primes[i]).Dump();
//			if ((2*index+1) == 5777) Util.HorizontalRun(true, (2*index+1), "=", square, "+", Primes[i]).Dump();
//			if ((2*index+1) == 5993) Util.HorizontalRun(true, (2*index+1), "=", square, "+", Primes[i]).Dump();
			MarkedSieve[index] = false;
		}
	}
	Util.Progress = null;
	
	"Finding unmarked".Dump();
	//Find first not marked
	int maxDump = 50;
	int dumpCount = 0;
	for (int i = 1; i < MarkedSieve.Length; ++i)
	{
		if (MarkedSieve[i])
		{
			//return 2*i+1;
			(2*i+1).Dump();
			if (++dumpCount > maxDump) return -43;
		}
	}
	
	return -46;
}
public static long Problem47()
{
	//ALTERNATIVE WAYS TO DO THIS:
	// Sieve, have code for that in ProjectEulier.OthersSolutions.linq
	// use list of primes divide by each upto x, count each time a unique one divides cleanly -- slightly optimized version of the GetPrimeFactors i used... but not needed in C# at least
	
	//int lowestPossible = 2*3*5*7;
	
//	//Fail match
//	Helpers.GetPrimeFactors(50138).Dump();
//	Helpers.GetPrimeFactors(50139).Dump();
//	Helpers.GetPrimeFactors(50121).Dump();
//	Helpers.GetPrimeFactors(50122).Dump();
//	//Success
//	long test = 134043;
//	for (int i = 0; i < 4; ++i)
//	{
//		Helpers.GetPrimeFactors(test+i).Dump();
//	}
	
	int baseNumber = 0;
	int consecutiveCount = 0;
	for (int i = 600; i < 400000; ++i)
	{
		List<long> curFactors = Helpers.GetPrimeFactors(i);
		if (curFactors.Count >= 4 && curFactors.Distinct().Count() >= 4)
		{
			if (baseNumber == 0) baseNumber = i;
			++consecutiveCount;
			if (consecutiveCount >= 4)
			{
				return baseNumber;
			}
		}
		else if (baseNumber > 0) //If next number does not match, throw away this
		{
			baseNumber = 0;
			consecutiveCount = 0;
		}
	}
	
	return -9999;
}
public static long Question42()
{
//	int[] TriAlpha = new int[26];
//	int sum = 0;
//	for (int i = 0; i < 26; ++i)
//	{
//		sum += i + 1;
//		TriAlpha[i] = sum;
//	}
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem42_words.txt");
	
	string wordsFile = File.ReadAllText(PATH);
	var lines = wordsFile.Trim('"').Split(new string[] { "\",\"" }, StringSplitOptions.None).ToList();
	long count = 0;
	for (int i = 0; i < lines.Count; ++i)
	{
		long worth = 0;
		foreach (char c in lines[i])
		{
			worth += (c - 'A' + 1);
		}
		//if (lines[i] == "SKY") worth.Dump("SKY");
		if (Question12_IsTriangleNumber(worth)) ++count;
	}
	
	return count;
}
public static bool Question12_IsTriangleNumber(long num)
{
	long i = (long)Math.Sqrt(num*2);
	long triNum = (i*i+i)/2;
	if (triNum == num) return true;
	return false;
}
public static long Question33()
{
	int MAX = 100;
	
	int numerator = 1;
	int denominator = 1;
	
	for (int i = 10; i < MAX; ++i)
	{
		for (int j = i+1; j < MAX; ++j)
		{
			if (i / 10 == j % 10)
			{
				if ((double)(i % 10) / (j / 10) == ((double)i/j))
				{
					numerator *= i % 10;
					denominator *= j / 10;
					Util.HorizontalRun(false, i % 10, "/", j / 10).Dump();
				}
			}
			if (i % 10 == j / 10)
			{
				if ((double)(i / 10) / (j % 10) == ((double)i/j))
				{
					numerator *= i / 10;
					denominator *= j % 10;
					Util.HorizontalRun(false, i / 10, "/", j % 10).Dump();
				}
			}
		}
	}
	
	Util.HorizontalRun(false, numerator, "/", denominator).Dump("Product");
	
	return denominator / numerator;
}
public static long Question50()
{
	//TODO Cheating... longer than 1 minute to finish, had it output best matches as it went and took one that didn't change for a while
	return -23;
	
	//Could go backwards, recursive back from MAX, check if that number is prime, then remove primes from x/2 and see what length we get
	//Or go backwards, recursive fom MAX/2, add primes and see if its still prime, can it be prime -- i think this fails due to having to run down to 2 every time
	
	int MAX = 1000000;
	
	//TODO should just cache all the primes under MAX
	//Without that was 2.8 for 10000
	List<int> Primes = Helpers.PrimesLessThan(MAX);
	
	int bestMatch = 0;
	int bestLength = 0;
	
	--MAX; //Make MAX an odd number
	for (int i = Primes.Count-1; i >= 0; --i)
	{
		for (int j = i; j >= 0; --j)  //could reduce i by a factor proportial to... something
		{
			int testingPrime = Primes[i];
			int consecutiveLength = 0;
			
			for (int k = j/2; k >= 0; --k)
			{
				++consecutiveLength;
				testingPrime -= Primes[k];
				//if (Primes[i] == 41) testingPrime.Dump();
				//k.Dump("Prime:" + testingPrime.ToString());
				if (testingPrime == 0)
				{
					//Hey worked out to 0
					//Primes[i].Dump("Okay at " + consecutiveLength);
					if (consecutiveLength > bestLength)
					{
						bestMatch = Primes[i];
						bestLength = consecutiveLength;
						
						bestLength.Dump("Length");
						bestMatch.Dump("Match");
					}
					//Move onto the next one
					break;
				}
				if (testingPrime < 0)
				{
					//this fails
					break;
				}
			}
		}
	}
	
	bestLength.Dump("Length");
	bestMatch.Dump("Match");
	
	return -23;
}
public static long Question48B()
{
	//After reading forum and seeing that you just need to keep track of the lowest 10 digits... which should always be within long
	int MAX = 1000;
	long KEEP_DIGITS_UNDER = 10000000000;
	
	long outputSum = 1; //Start with 1^1
	
	for (int i = 2; i <= MAX; ++i)
	{
		long thisResult = 1;
		for (int j = 0; j < i; ++j)
		{
			thisResult *= i;
			thisResult %= KEEP_DIGITS_UNDER;
		}
		outputSum += thisResult;
		outputSum %= KEEP_DIGITS_UNDER;
	}
	//outputSum.ToString().Dump();
	
	//"6797271283465789498383642350667978127819110846700".Dump();
	//"9110846700".Dump();
	return outputSum;
}
public static string Question48()
{
	//DONE 7 seconds or so on laptop, could just keep track of the lowest 10 digits... which should always be within long
	int MAX = 1000;
	
	InfiniteInt outputSum = new InfiniteInt(1);
	
	for (int i = 2; i <= MAX; ++i)
	{
		//long thisResult = 1;
		InfiniteInt thisResult = new InfiniteInt(1);
		for (int j = 0; j < i; ++j)
		{
			thisResult.Multiply(i);
		}
		outputSum.Add(thisResult);
	}
	//outputSum.ToString().Dump();
	
	return outputSum.ToString(0, 10);
}
public static long Question29()
{
	//Duplicates would happen when i*i = ib, or similar
	//  So i wrote Question29_IsPowerOfLower()
	
	int MAX = 100;
	
//	int outputCount = 0;
	List<string> products = new List<string>();
	
	for (int i = 2; i <= MAX; ++i)
	{
		InfiniteInt prod = new InfiniteInt(i);
//		int skipPower = Question29_IsPowerOfLower(i);
		for (int j = 2; j <= MAX; ++j)
		{
			prod.Multiply(i);
//			if (skipPower > 0 && skipPower == j)
//			{
//				("skip: " + skipPower + " == " + j).Dump();
//			}
			products.Add(prod.ToString());
			//prod.Dump();
//			++outputCount;
		}
	}
	
	//products.Skip(9000).Dump();
	
	return products.Distinct().LongCount();
}
public static int Question29_IsPowerOfLower(int num)
{
	//Determine if this number can be represented by a power of a lower number
//	if (num > 2 && num % 2 == 0)
//	{
//		int twos = num;
//		while (twos > 2)
//		{
//			if (twos % 2 != 0) break;
//			twos /= 2;
//		}
//	}
	
	for (int i = 2; (i * i) <= num; ++i)
	{
		//Since we check it in the loop anyway...
		if (i * i == num) return i;
		if (Question29_IsPowerOf(num, i))
		{
			("Yes that is a square of " + i).Dump();
			return i;
		}
	}
	return 0;
}
public static bool Question29_IsPowerOf(int num, int intBase)
{
	//Determine if this number can be represented by a power of a lower number
	if (num > intBase)
	{
		//if it can't be evenly divided by it in the first place, it never will become so
		if (num % intBase == 0)
		{
			int twos = num;
			while (twos > intBase)
			{
				twos /= intBase;
				//If it can't be evenly divided, it won't become so
				if (twos % intBase != 0) return false;
			}
			if (twos == intBase)
			{
				return true;
			}
		}
		else
		{
			return false;
		}
	}
	else
	{
		throw new ArgumentException("num must be greater than intBase");
	}
	throw new ArgumentException("f");
}
public static long Question27()
{
	//b has to be prime, since starting at 0
	//  Can negative numbers be prime??... of not b has to be positive too by same logic lol
	
//	Helpers.isPrime_Q7PDF(0).Dump();
//	Helpers.isPrime_Q7PDF(1).Dump();
//	Helpers.isPrime_Q7PDF(2).Dump();
//	Helpers.isPrime_Q7PDF(3).Dump();
//	Helpers.isPrime_Q7PDF(-1).Dump(); //Bug in this function, all negatives are true
//	Helpers.isPrime_Q7PDF(-4).Dump();
//	return -2;
	
	int bestN = 0;
	long bestOutput = -1;
	
	for (int b = 2; b < 1000; ++b)
	{
		if (!Helpers.isPrime_Q7PDF(b)) continue;
//	for (int bInc = 0; bInc < 2000; ++bInc)
//	{
//		int b = bInc / 2 * (1 - 2*(bInc % 2));
		
		for (int aInc = 0; aInc < 2000; ++aInc)
		{
			//This switches between positive and negative
			//There probably is a simpler way to write this.. but this works fine
			int a = aInc / 2 * (1 - 2*(aInc % 2));
			
			int n = 0;
			for (; Helpers.isPrime_Q7PDF(n*n + a*n + b); ++n)
			{ }
			if (n > bestN)
			{
				bestN = n;
				bestOutput = a*b;
				Util.HorizontalRun(true, bestN, bestOutput, a, b).Dump();
			}
		}
	}
	bestN.Dump("Best N");
	return bestOutput;
}
public static long Question28()
{
//	//Not what i want at all...
//	int width = 5;
//	for (; width < 20; ++width)
//	{
//		Question28_DiagnalSum(width).Dump();
//	}
//	return width;
	int width = 1001;
	if (width % 2 == 0) throw new ApplicationException("Width must be odd");
	//Upper Right:
	long urSum = 0;
	long ulSum = 0;
	long llSum = 0;
	long lrSum = 0;
	for (int i = width; i > 1; --i, --i)
	{
		long sqr = i * i;
		urSum += sqr;
		ulSum += sqr - i + 1;
		llSum += sqr - 2*(i - 1);
		lrSum += sqr - 3*(i - 1);
	}
	
	//Add one the center //-being counted twice
	long outputSum = 1 + urSum + ulSum + llSum + lrSum;
	return outputSum;
}
public static long Question22()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem22_names.txt");
	
	string namesFile = File.ReadAllText(PATH);
	var lines = namesFile.Trim('"').Split(new string[] { "\",\"" }, StringSplitOptions.None).OrderBy(n => n).ToList();
	long totalScore = 0;
	for (int i = 0; i < lines.Count; ++i)
	{
		long worth = 0;
		foreach (char c in lines[i])
		{
			worth += (c - 'A' + 1);
		}
		long rank = (i+1) * worth;
		totalScore += rank;
	}
	
	return totalScore;
}
public static long Question21()
{
	//Original version on laptop: 0.923s == 31626
	//The (sum > i) check here has very minor improvement from my original (at most 0.2s)
	//Using Question21_SumOfProperDivisorsPDF instead of Question21_DivisorsSum takes the time down to 0.010s
	int UNDER = 10000;
	long outputSum = 0;
	for (int i = 2; i < UNDER; ++i)
	{
		//if (Helpers.isPrime_Q7PDF(i)) continue;
		int sum = Question21_DivisorsSum(i);
		if (sum > UNDER || sum == 1
			|| sum == i) continue;
		if (sum > i) continue; //THIS WAS IN THE PDF
		if (Question21_DivisorsSum(sum) == i) outputSum += i + sum;  //ADD THE SUM HERE TOO, PDF
	}
	
	return outputSum;
}
public static int Question21_DivisorsSum(int num)
{
	int sum = 1;
	for (int i = num / 2; i > 1; --i)
	{
		if (num % i == 0) sum += i;
	}
	return sum;
}
public static int Question21_SumOfProperDivisorsPDF(int n)
{
	return Question21_SumOfDivisorsPDF(n) - n;
}
public static int Question21_SumOfDivisorsPDF(int n)
{
	//This is using prime factorization
	int sum = 1;
	int p = 2;
	while (p*p <= n && n > 1) //Prevents us from checking prime factors greater than sprt(n)
	{
		if (n % p == 0)
		{
			int j = p*p;
			n /= p;
			while (n % p == 0)
			{
				j *= p;
				n /= p;
			}
			sum *= j-1;
			sum /= p-1;
		}
		if (p == 2)
		{
			p = 3;
		}
		else
		{
			p +=2;
		}
	}
	if (n > 1) //covers the case that one prime factor greater than sqrt(n) remains
	{
		sum *= n+1;
	}
	return sum;
}
public static long Question26()
{
//	Question26_LongDivisionFindRecurring(1, 902, 10).Dump();
//	Question26_LongDivisionFindRecurring(1, 902, 15).Dump();
//	Question26_LongDivisionFindRecurring(1, 902, 16).Dump();
//	Question26_LongDivisionFindRecurring(1, 902, 17).Dump();
//	Question26_LongDivisionFindRecurring(1, 902, 18).Dump();
//	Question26_LongDivisionFindRecurring(1, 902, 19).Dump();
//	Question26_LongDivisionFindRecurring(1, 953, 30).Dump();
//	Question26_LongDivision(1, 953, 0, 2000).Dump("Def recurring length is 952");
//	return 90;
//	Question26_LongDivisionFindRecurring(1, 712, 10).Dump();
//	Question26_LongDivision(1, 712, 10, 30).Dump();
//	((decimal)1/712).Dump();
	
	
	Decimal dec = 1;
//	List<int> Canidates = new List<int>();
	List<int> NoRepeats = new List<int>();
//	int CanidatesSize = 0;
	
	int count = 0;
	for (int i = 1; i < 1000; ++i)
	{
		////STRING PROCESSING
		//Decimal div = dec / i;
		string div = (dec / i).ToString();
		
		//if (div.Length != 30) continue;
		div = div.TrimStart('0', '.', '1');
		if (div.Length > 1) div = div.TrimStart(div[0], div[1])
									.TrimEnd(div[div.Length-1]); //Remove any rounded digits
		
		if (div.Length == 0) continue;
		
		int recurring = Question26_FindRecurring(div);
		if (recurring == -3)
		{
			NoRepeats.Add(i);
		}
//		else if (recurring > CanidatesSize)
//		{
//			CanidatesSize = recurring;
//			Canidates = new List<int>();
//			Canidates.Add(i);
//		}
//		else if (recurring == CanidatesSize)
//		{
//			Canidates.Add(i);
//		}
		//(i.ToString() + ": " + div + " == " + recurring).Dump();
		
		++count;
	}
	count.Dump("Number Processed");
	
	int max = 0;
	int num = -1;
	int failCount = 0;
	foreach (int i in NoRepeats)
	//for (int i = 1; i < 1000; ++i)
	{
		try
		{
			int tmp = Question26_LongDivisionFindRecurring(1, i, 20);
			if (tmp > max)
			{
				num = i;
				max = tmp;
			}
			else if (tmp == max)
			{
				i.Dump("Equals max: " + max);
			}
		}
		catch (Exception)
		{
			i.Dump();
			++failCount;
		}
	}
	failCount.Dump("Fail count");
	
	//FUCK not working...
	// TOTRY: 191, 953
	// tried: 931, 447, 848, 894, 699, 149, 514, 599, 828, 831, 526, 755, 277, 683
	// think tried: 867
	// not tried: 
	((Decimal)1/983).Dump();
	
	num.Dump("Okay: " + max);
	
//	Canidates.Dump("Size: " + CanidatesSize);
//	NoRepeats.Dump("No Repeating Found In This Range");
	
//	ulong ulo = 10000000000000000000;
//	for (ulong i = 1; i < 1000; ++i)
//	{
//		(ulo / i).Dump();
//	}
	
	return num;
}
public static int Question26_LongDivisionFindRecurring(long X, long D, int skip)
{
	long iX = X;
	long iD = D;
	
	int MAX_LENGTH = 1000;
	
	int FIND_LEN = 4;
	string find = null;
	
	StringBuilder sb = new StringBuilder(MAX_LENGTH);
	for (int i = 0; i < MAX_LENGTH; ++i)
	{
		bool shifted = false;
		while (X < D)
		{
			if (X == 0) break;
			//Insert zeros, only after X has been normally shifted once
			if (shifted && i >= skip) sb.Append(0);
			X *= 10;
			shifted = true;
		}
		
		//Check if we should start outputting digits
		if (i >= skip)
		{
			sb.Append(X/D);
			if (find == null && sb.Length >= FIND_LEN)
			{
				//(sb.Length + " >= " + FIND_LEN).Dump();
				find = sb.ToString();
				//If its all zeros, then repeat is zero
				if (find == new string('0', FIND_LEN)) return 0;
			}
			else if (find != null
				&& sb[sb.Length-1] == find[find.Length-1])
			{
				bool failed = false;
				//find.Length.Dump();
				for (int k = 2; k < find.Length; ++k)
				{
					//(sb[sb.Length-k] + " != " + find[FIND_LEN-k]).Dump();
					if (sb[sb.Length-k] != find[find.Length-k]) failed = true;
				}
				if (!failed)
				{
//					Util.HorizontalRun(true, iX, iD, skip,
//										"Find:", find, "StrLen:", sb.Length).Dump("CAUSED RETURN");
//					sb.ToString().Dump();
					return sb.Length - find.Length;
				}
			}
		}
		//((X/D) + " R" + (X % D)).Dump();
		//Move on to the remainder
		X %= D;
	}
	Util.HorizontalRun(true, iX, iD, skip,
							"Find:", find, "StrLen:", sb.Length).Dump("CAUSED FAIL");
	sb.ToString().Dump();
	throw new NotImplementedException("idk failure");
}
public static string Question26_LongDivision(long X, long D, int skip, int length)
{
	StringBuilder sb = new StringBuilder(length);
	for (int i = 0; i < (skip+length); ++i)
	{
		bool shifted = false;
		while (X < D)
		{
			if (X == 0) break;
			if (shifted && i >= skip) sb.Append(0);
			X *= 10;
			shifted = true;
		}
		if (i >= skip)
		{
			sb.Append(X/D);
		}
		//((X/D) + " R" + (X % D)).Dump();
		X %= D;
	}
	return sb.ToString();
}
public static int Question26_FindRecurring(string tirmmedNumber)
{
	try
	{
		if (tirmmedNumber.Length <= 1) return 0;
		
		const int NOT_FOUND_RETURN = -3;
		
		int[] finds = new int[30];
		int findsIndex = 0;
		
		
//		int lastPos = 0;
//		for (int pos = tirmmedNumber.IndexOf(tirmmedNumber[0]);
//			pos >= 0;
//			pos = tirmmedNumber.IndexOf(tirmmedNumber[0], pos+1))
//		{
//			finds[findsIndex++] = pos - lastPos;
//			lastPos = pos;
//		}
		char find = tirmmedNumber[tirmmedNumber.Length - 1];
		int lastPos = tirmmedNumber.Length - 1;
		for (int pos = tirmmedNumber.LastIndexOf(find, lastPos-1);
			pos > 0;
			pos = tirmmedNumber.LastIndexOf(find, pos-1))
		{
			finds[findsIndex++] = lastPos - pos;
			lastPos = pos;
		}
		
		//Didn't find anything i assume...
		if (findsIndex == 1) return NOT_FOUND_RETURN;
		
		int last = finds[1];
		int sum = finds[1];
		for (int i = 1; i < findsIndex; ++i)
		{
			if (finds[i] == last) return sum;
			sum += finds[i];
		}
		return sum;
	}
	catch (Exception ex)
	{
		ex.ToString().Dump("Exception");
		return -1;
	}
}
public static long Question32()
{
	//THIS ALSO WOULD BE RADIX TYPE SHIT SORTA, but easier to brute force probably than others
	
	//1-9 pandigital
	// a * b = c
	// a < b < c
	// a MUST be under 100: 99 * 100 = 9900 == 9 total digits, anything more than that will have 10 digits
	// b depents on a... must be larger than a, and the count of the digits should be less than 9, if not break the b loop
	//   upper limit would be 9 digits
	
	int GoodChecksum = Question32_CheckSumDigits(123, 456, 789).Dump();
	if (GoodChecksum != Question32_CheckSumDigits(39, 186, 7254).Dump())
	{
		throw new ArithmeticException("test checksum should match");
	}
	if (GoodChecksum == Question32_CheckSumDigits(27, 186, 7254).Dump())
	{
		throw new ArithmeticException("test checksum should NOT match");
	}
	if (GoodChecksum == Question32_CheckSumDigits(21, 474, 9954).Dump())
	{
		throw new ArithmeticException("test checksum should NOT match");
	}

	
	//int loopCount = 0;
	int countCount = 0;
	int checksumCount = 0;
	List<int> cValues = new List<int>();
	
	for (int a = 1; a < 100; ++a)
	{
		for (int b = a; b <= 987654321; ++b)
		{
			//++loopCount;
			
			int c = a * b;
			int count = Question32_countDigits(a, b, c);
			if (count > 9) break;
			if (count < 9) continue;
			
			++countCount;
			
			if (GoodChecksum == Question32_CheckSumDigits(a, b, c))
			{
				++checksumCount;
				Util.HorizontalRun(true, a, b, c).Dump();
				cValues.Add(c);
			}
		}
	}
	
	//loopCount.Dump("Looped");
	countCount.Dump("Had 9");
	checksumCount.Dump("Matched Checksum");
	
	//Could do it with a checksum of all the digits together...
	return cValues.Distinct().Dump().Sum();
}
public static int Question32_countDigits(int a, int b, int c)
{
	int sum = 0;
	while (a > 0)
	{
		a /= 10;
		++sum;
	}
	while (b > 0)
	{
		b /= 10;
		++sum;
	}
	while (c > 0)
	{
		c /= 10;
		++sum;
	}
	return sum;
}
public static int Question32_CheckSumDigits(int a, int b, int c)
{
	int chkSum = 0;
	int chkSum2 = 1;
	while (a > 0)
	{
		//chkSum += (a % 10) ^ 23;	// Had 1138 matches
		chkSum += (a % 10);
		chkSum2 *= (a % 10) ^ 23;		// + Had  966 matches, * had 55, with ^23 had 9
		a /= 10;
	}
	while (b > 0)
	{
		//chkSum += (b % 10) ^ 23;
		chkSum += (b % 10);
		chkSum2 *= (b % 10) ^ 23;
		b /= 10;
	}
	while (c > 0)
	{
		//chkSum += (c % 10) ^ 23;
		chkSum += (c % 10);
		chkSum2 *= (c % 10) ^ 23;
		c /= 10;
	}
	return chkSum + chkSum2;
}
//NOTE: wrote this AFTER all the other Problem 34 stuff, checking if brute forcing it would be eaiser... turns out if fails, only finding 145
public static long Problem34_BruteForce_Fail() //NOTE: non-brute force solution started at: Question34_Elegant_NotDone()
{
	int[] fc = new int[] { 0, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
	
	long outputSUm = 0;
	
	//BRUTE FORCE
	for (long i = 3; i < 100000000; ++i)
	{
		long ti = i;
		int iSum = 0;
		while (ti > 0)
		{
			iSum += fc[ti % 10];
			ti /= 10;
		}
		if (i == iSum)
		{
			i.Dump();
			outputSUm += i;
		}
	}
	
	return -34;
}
public static long Problem34()
{
	int[] idealList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 
								  11, 12, 22, 13, 23, 33, 14, 24, 34, 44, 15, 25, 35, 45, 55, 16, 26, 36, 46, 56, 66, 17, 27, 37, 47, 57, 67, 77, 18, 28, 38, 48, 58, 68, 78, 88, 19, 29, 39, 49, 59, 69, 79, 89, 99,
								  111, 112, 122, 222, 113, 123, 223, 133, 233, 333, 114, 124, 224, 134, 234, 334, 144, 244, 344, 444, 115, 125, 225, 135, 235, 335, 145, 245, 345, 445, 155, 255, 355, 455, 555, 116, 126, 226, 136, 236, 336, 146, 246, 346, 446, 156, 256, 356, 456, 556, 166, 266, 366, 466, 566, 666, 117, 127, 227, 137, 237, 337, 147, 247, 347, 447, 157, 257, 357, 457, 557, 167, 267, 367, 467, 567, 667, 177, 277, 377, 477, 577, 677, 777, 118, 128, 228, 138, 238, 338, 148, 248, 348, 448, 158, 258, 358, 458, 558, 168, 268, 368, 468, 568, 668, 178, 278, 378, 478, 578, 678, 778, 188, 288, 388, 488, 588, 688, 788, 888, 119, 129, 229, 139, 239, 339, 149, 249, 349, 449, 159, 259, 359, 459, 559, 169, 269, 369, 469, 569, 669, 179, 279, 379, 479, 579, 679, 779, 189, 289, 389, 489, 589, 689, 789, 889, 199, 299, 399, 499, 599, 699, 799, 899, 999 };
	
	
	
	
	int[] array = new int[9];
	
	int index = 0;
	int topIndex = 0;
	for (int i = 0; i < 1000; ++i)
	{
		if (array[index] == 9)
		{
			//Reached limit, need to add another place
			array[index + 1] = 1;
			++topIndex;
			for (; index >= 0; --index){
				array[index] = 1;
			}
			++index;
			++index;
		}
		else if (index > 0 && array[index] == array[index - 1])
		{
			if (index == topIndex)
			{
				//Go to the furthest one back that is not equal to previous
				//  setting each step over back to 1
				int j = index;
				for (; j > 0 && array[j] == array[j - 1]; --j)
				{
					array[j] = 1;
				}
				array[j]++;
			}
			else
			{
				//Start incrementing the next position over
				array[index - 1]++;
				++index;
			}
		}
		else
		{
			//Normal increment
			array[index]++;
		}
		//string.Join("", array.Select(x => x.ToString()).Reverse().ToArray()).Dump();
		
		if (i < idealList.Length)
		{
			Util.HorizontalRun(true, Problem34(array), idealList[i], Problem34(array) == idealList[i]).Dump();
//			string fail = Problem34(array) == idealList[i] ? "" : " -- FAIL";
//			(Problem34(array) + " " + idealList[i] + fail).Dump();
		}
		else
		{
			Problem34(array).Dump();
		}
	}
	return -34;
}
public static long Problem34(int[] array)
{
	long output = 0;
	bool skip = true;
	for (int i = array.Length-1; i >= 0; --i)
	{
		if (skip && array[i] == 0)
			continue;
		else
		{
			skip = false;
			output = output * 10 + array[i];
		}
	}
	return output;
}
public static long Problem34_Elegant_InProgress()
{
	//FactorialCache fc = new FactorialCache(9);
	int[] fc = new int[] { 0, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
	
	//Question34_FactorialDigitSum(fc, 145).Dump();
	
	long outputSum = 0;
	
//	for (int i = 1; i < 100; ++i)
//	{
//		if (Question34_FactorialDigitSum(factorials, i).Dump() == i)
//		{
//			outputSum += i;//.Dump();
//		}
//	}
	
//	int startSum = 0;
//	for (int i = 1; i <= 9; ++i)
//	{
//		startSum += factorials[i];
//		startSum.Dump();
//		for (int j = i; j <= 9; ++j)
//		{
//			int curSum = startSum + factorials[j];
//			if (curSum == Question34_FactorialDigitSum(factorials, curSum))
//			{
//				outputSum += curSum.Dump();
//			}
//		}
//	}
	
//	outputSum = Question34_recurse(factorials, 0, 0, 0);
	
	
	////This is very close... but has some bug that i'm not able to debug right now...
	////Switching to IEnumerator
//	int MAX_LENGTH = 5;
//	long baseCurrent = 0;
//	
//	for (int m = 0; m < MAX_LENGTH; ++m)
//	{
//	
//	baseCurrent = baseCurrent + 1;
//	if (m > 0) baseCurrent *= 10;
//	
//	
//	//Each Digit
//	for (int i = 1; i <= 9; ++i)
//	{
//		long CurrentString = baseCurrent + i;
//		long place = 1;
//		for (int j = 1; j < m; ++j)
//		{
//			place *= 10;
//			//place.Dump();
//			for (int k = 0; k < i; ++k)
//			{
//				if (k > 0) CurrentString += place * 1;
//				
//				CurrentString.Dump();
//				
//				long curStrSum = Question34_FactorialDigitSum(fc, CurrentString);
//				if (curStrSum == Question34_FactorialDigitSum(fc, curStrSum))
//				{
//					curStrSum.Dump("w");
//				}
//			}
//		}
//	}
//	}
	
//////	//BRUTE FORCE MAKING IDEAL LIST
//////	Dictionary<long, long> nextIdealList = new Dictionary<long, long>();
//////	for (int i = 111; i < 1000; ++i)
//////	{
//////		long f = Question34_FactorialDigitSum(fc, i);
//////		if (!nextIdealList.ContainsKey(f))
//////			nextIdealList.Add(f, i);
//////	}
//////	var ideal = nextIdealList.Where(f => (f.Value % 10 != 0) && (f.Value / 10 % 10 != 0)).OrderBy(f => f.Key).Select(f => f.Value).Dump();
//////	("int[] idealList = new int[] { " + string.Join(", ", ideal.Select(i => i.ToString()).ToArray()) + " };").Dump();
//////	return -5;
	
	int[] idealList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 
								  11, 12, 22, 13, 23, 33, 14, 24, 34, 44, 15, 25, 35, 45, 55, 16, 26, 36, 46, 56, 66, 17, 27, 37, 47, 57, 67, 77, 18, 28, 38, 48, 58, 68, 78, 88, 19, 29, 39, 49, 59, 69, 79, 89, 99,
								  111, 112, 122, 222, 113, 123, 223, 133, 233, 333, 114, 124, 224, 134, 234, 334, 144, 244, 344, 444, 115, 125, 225, 135, 235, 335, 145, 245, 345, 445, 155, 255, 355, 455, 555, 116, 126, 226, 136, 236, 336, 146, 246, 346, 446, 156, 256, 356, 456, 556, 166, 266, 366, 466, 566, 666, 117, 127, 227, 137, 237, 337, 147, 247, 347, 447, 157, 257, 357, 457, 557, 167, 267, 367, 467, 567, 667, 177, 277, 377, 477, 577, 677, 777, 118, 128, 228, 138, 238, 338, 148, 248, 348, 448, 158, 258, 358, 458, 558, 168, 268, 368, 468, 568, 668, 178, 278, 378, 478, 578, 678, 778, 188, 288, 388, 488, 588, 688, 788, 888, 119, 129, 229, 139, 239, 339, 149, 249, 349, 449, 159, 259, 359, 459, 559, 169, 269, 369, 469, 569, 669, 179, 279, 379, 479, 579, 679, 779, 189, 289, 389, 489, 589, 689, 789, 889, 199, 299, 399, 499, 599, 699, 799, 899, 999 };
	int idealIndex = 0;
	
//	Dictionary<long, long> debug = new Dictionary<long, long>();
	
	Question34_enum en = new Question34_enum();
	while (en.MoveNext())
	{
		if (idealIndex == 0)
		{
			if (idealList[idealIndex] == en.Current) ++idealIndex;
		}
		else if (idealIndex < idealList.Length)
		{
			if (idealList[idealIndex] != en.Current) ("FAIL ideal: " + en.Current + " != " + idealList[idealIndex]).Dump();
			++idealIndex;
		}
		
		(en.Current + "\t" + Question34_FactorialDigitSum(fc, en.Current)).Dump();
//		debug.Add(en.Current, Question34_FactorialDigitSum(fc, en.Current));
		if (en.Current > 1000) break;
	}
	
//	var idealList = debug.Where(f => f.Key >= 10 && f.Key < 100).OrderBy(f => f.Value).Select(f => f.Key).Dump();
//	long last = 0;
//	foreach (long l in idealList)
//	{
//		((l - last) + " -- " + l).Dump();
//		last = l;
//	}
//	
//	("int[] idealList = new int[] { " + string.Join(", ", ideal.Select(i => i.ToString()).ToArray()) + " };").Dump();
	
	return long.MaxValue;
}
//public static long Question34_recurse(int[] fc, int sum, int largest, long resultSum)
//{
//	if (largest > 9) return resultSum;
//	
//	//Check if its a match
//	if (sum == Question34_FactorialDigitSum(fc, sum))
//	{
//		resultSum += sum.Dump();
//	}
//	
//	for (int i = largest; i <= 9; ++i)
//	{
//		Question34_recurse(fc, sum + fc[i], largest + 1, resultSum);
//	}
//	
//	return resultSum;
//}
//public static long Question34_FactorialDigitSum(FactorialCache fc, int num)
public static long Question34_FactorialDigitSum(int[] fc, long num)
{
	long sum = 0;
	while (num > 0)
	{
		sum += fc[num % 10];
		num /= 10;
	}
	return sum;
}
public class Question34_enum : IEnumerator<long>
{
	long TYPE_MAX_VALUE = 8999999999999999999;
	int TYPE_MAX_LENGTH = 19;

	long current = 0;  //Reset to placeLength 1's each place enlargement
	//long digitReset = 0;

	int endMultplier = 1; //Grows by *10 each place enlargement, not reset
	int placeLength = 1;  //Grows by +1 each place enlargement, not reset
	int currentMultiplier = 1; //Grows by *10 each place increment, reset on place enlargement
	int currentDigit = 1;
	int digitResetMultiplier = 1;

	public bool PlaceEnlargement()
	{
		endMultplier *= 10;
		placeLength += 1;
		currentMultiplier = 1;
		currentDigit += 1;

		if (placeLength > TYPE_MAX_LENGTH)
		{
			return false;
		}

		current = 0;
//		for (int i = 0; i < placeLength; ++i)
//		{
//			current = (current + 1) * 10;
//		}
//		digitReset = current;
		for (int i = 0; i < placeLength; ++i)
		{
			current = current * 10 + 1;
		}
		currentDigit = 1;
		return true;
	}

	#region IEnumerator<long> Members

	public long Current
	{
		get { return current; }
	}

	#endregion

	#region IDisposable Members

	public void Dispose()
	{
		return;
	}

	#endregion

	#region IEnumerator Members

	object System.Collections.IEnumerator.Current
	{
		get { return Current; }
	}

	public bool MoveNext()
	{
		if (current / endMultplier % 10 == 9)
		{
			PlaceEnlargement();
			endMultplier.Dump("PlaceEnlargement");
			return true;
		}
		if (currentMultiplier < endMultplier
			&& current / currentMultiplier % 10 == currentDigit)
		{
			currentMultiplier.Dump("inc currentMultiplier");
			currentMultiplier *= 10;
			////digitResetMultiplier
		}
		if (currentMultiplier >= 10
			&& current / currentMultiplier % 10 == currentDigit)
		{
			//currentDigit.Dump("cur digit");
			current.Dump("dec current");
			current -= (currentMultiplier * currentDigit - 1).Dump("minus");
			++currentDigit;
			//current.Dump("dec current b");
		}
//		if (current / currentMultiplier % 10 == 9)
//		{
//			//currentMultiplier *= 10;
//			currentDigit += 1;
//			current += currentMultiplier * 10;;
//			current -= 10 - currentDigit;
//			currentMultiplier.Dump("currentMultiplier inc");
//		}

		//limited by long type:
		if (current == TYPE_MAX_VALUE)
		{
			return false;
		}

		current += currentMultiplier;
		return true;
	}

	public void Reset()
	{
		current = 0;
		currentDigit = 1;

		endMultplier = 1;
		placeLength = 1;
		currentMultiplier = 1;
	}

	#endregion
}
//Found a Lexographical Permutation algorithm, implimented it as an Enumerator
// #2 on: http://www.cut-the-knot.org/do_you_know/AllPerm.shtml
public static long Question24()
{
	var per = new HelperEnumerators.LexicographicPermutations(10);
	
	int LOOKING_FOR = 1000000;
	
	per.Current.Dump();
	for (int i = 1; i < LOOKING_FOR; ++i)
	{
		per.MoveNext();
	}
	int[] output = per.Current.Dump();
	long outputNum = 0;
	foreach (int i in output)
	{
		outputNum = outputNum * 10 + i;
	}
	return outputNum;
}
public static long Question24_TODO()
{
//	//With: int[] BASE = new int[] { 0, 1, 2 };
//	int[] correct = new int[] { 0, 012, 021, 102, 120, 201, 210 };
//	Debug.Assert(Question24_GetPermutation(3, 1) == correct[1], "1");
//	Debug.Assert(Question24_GetPermutation(3, 2) == correct[2], "2");
//	Debug.Assert(Question24_GetPermutation(3, 3) == correct[3], "3");
//	Debug.Assert(Question24_GetPermutation(3, 4) == correct[4], "4");
//	Debug.Assert(Question24_GetPermutation(3, 5) == correct[5], "5");
//	Debug.Assert(Question24_GetPermutation(3, 6) == correct[6], "6");

	int[] correct = new int[] { 0, 0123, 0132, 0213, 0231, 0312, 0321,
									1023, 1032, 1203, 1230, 1302, 1320,
									2013, 2031, 2103, 2130, 2301, 2310,
									3012, 3021, 3102, 3120, 3201, 3210 };
	for (int i = 1; i < correct.Length; ++i)
	{
		Debug.Assert(Question24_GetPermutation(4, i) == correct[i], i.ToString() + " :: " + correct[i]);
	}

	
	//Somewhat cheating, wikipedia'd the concepts, havn't seen any code or anything
	//  was close to deriving it... but its tricky
	
	
//	int[] BASE = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//							// 8, 7, 6, 5, 4, 3, 2, 1, 0 };
//							// 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//	
//	int getNum = 2;
//	
//	getNum = getNum-1;
//	
//	BASE[getNum % 10].Dump();
//	BASE[getNum % 10 + 1].Dump();
//	BASE[getNum % 10 + 2].Dump();
//	BASE[getNum % 10 + 3].Dump();
//	BASE[getNum % 10 + 4].Dump();
//	BASE[getNum % 10 + 5].Dump();
//	BASE[getNum % 10 + 6].Dump();
//	BASE[getNum % 10 + 7].Dump();
//	BASE[getNum / 10 % 10 + 8].Dump();
//	BASE[getNum % 10 + 9].Dump();
	
	return -1;
}
public static long Question24_GetPermutation(int digits, long number)
{
	//Consecutivly found: Could step through and swap each position up from the bottom...
	//  Once the largest value is at the top, its ended
	
	
	//reduce to 0-index
	--number;
	
	int[] BASE = new int[digits];
	//int[] LEHMER = new int[] { 5, 2, 5, 0, 1, 3, 2, 0, 0 };
	for (int i = 0; i < digits; ++i)
	{
		BASE[i] = i;
	}
	bool[] CHK = new bool[digits];
	
	int baseIndex = 0;
	long output = 0;
	long lehmerOutput = 0;
	
	for (int i = digits; i > 0; --i)
	{
		long lehmer = number % i;
		lehmerOutput = lehmerOutput * 10 + lehmer;
		int skip = 0;
		
		while (CHK[baseIndex + lehmer + skip])
		{
			++skip;
		}
		//(baseIndex + lehmer + skip).Dump();
		output = output * 10 + BASE[baseIndex + lehmer + skip];
		CHK[baseIndex + lehmer + skip] = true;
		
		if (lehmer == 0) ++baseIndex;
	}
	(number + " : " + lehmerOutput.ToString("d" + digits.ToString()) + " => " + output).Dump();
	return output;
	
	
//	int[] BASE = new int[] { 0, 1, 2, 3 };
//	
//	//reduce to 0-index
//	--number;
//	
//	int[] output = BASE;
//	Array.Reverse(output);
//	if ((number & (1 << 3)) > 0)
//	{
//		"4 idk".Dump();
////		int tmp = output[0];
////		output[0] = output[1];
////		output[1] = output[2];
////		output[2] = tmp;
//	}
//	if ((number & (1 << 2)) > 0)
//	{
//		"3 Shift".Dump();
//		int tmp = output[0];
//		output[0] = output[1];
//		output[1] = output[2];
//		output[2] = tmp;
//	}
//	if ((number & (1 << 1)) > 0)
//	{
//		"2 Swap".Dump();
//		int tmp = output[1];
//		output[1] = output[2];
//		output[2] = tmp;
//	}
//	if ((number & (1 << 0)) > 0)
//	{
//		"1 Swap".Dump();
//		int tmp = output[0];
//		output[0] = output[1];
//		output[1] = tmp;
//	}
//	long retOutput = 0;
//	for (int i = output.Length-1; i >= 0; --i)
//	{
//		retOutput = retOutput * 10 + output[i];
//	}
//	return retOutput.Dump((number+1).ToString());
	
	
	
//	int baseVal = BASE.Length;
//	
//	int placeValue = baseVal;
//	for (int x = 0; x < baseVal; ++x)
//	{
//		int placeDigit = number % placeValue;
//		BASE[(placeDigit + x) % baseVal].Dump();
//		//placeDigit.Dump();
//		number /= placeValue;
//	}
//	
//	return -2;
}
public static long Question25()
{
	int LOOKING_FOR_LENGTH = 1000;
	
	BigFib bf = new BigFib();
	
	while (bf.EstimateLength(BigFib.EstimateLengthType.Ceiling) < LOOKING_FOR_LENGTH)
	{
		bf.Next();
	}
	bf.CurrentSequenceNumber.Dump("Within ballpark at");
	
	//Find exact...
	while (bf.ExactLength() < LOOKING_FOR_LENGTH)
	{
		bf.Next();
	}
	
	return bf.CurrentSequenceNumber;
	
//	//Thought about trying to do it with the condensed formula or whatever.. but that doesn't help
//	double phi = 1.61803398874989; //1.61803398874989484820458683436563811772030917980576286213544862270526046281890244970720720418939113748475;
//	double sqrt5 = Math.Sqrt(5);
//	
//	//Get Fibonacci(n)
//	int n = 5;
//	int Fn = (int)(Math.Pow(phi, n) / sqrt5 + 0.5);
//	Fn.Dump();
//	
//	int SEQ_COUNT = 12;
//	
//	//Fibonacci
//	for (int i = 3; i < SEQ_COUNT; ++i)
//	{
//		
//	}
//	
//	return -2;
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
	throw new NotImplementedException("Started working on this...");
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



#region Helpers
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
//	public static List<int> PrimesLessThan(int maxValue)
//	{
//		int number = 200;
//		List<int> output = new List<int>(number);
//		
//		output.Add(2);
//		
//		for (int i = 3;
//			i < int.MaxValue
//			&& i <= maxValue
//			; ++i, ++i)
//		{
////			bool hasFactor = false;
////			foreach (int j in output)
////			{
////				if (i % j == 0)
////				{
////					hasFactor = true;
////					break;
////				}
////			}
////			if (!hasFactor) output.Add(i);
//			if (isPrime_Q7PDF(i)) output.Add(i);
//		}
//		
//		return output;
//	}
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
	
	public static bool isPrime_Q7PDF(long n)
	{
		if (n < 0) return false;
		if (n == 1) return false;
		else if (n < 4) return true;	//2 and 3 are prime
		else if (n % 2 == 0) return false;
		else if (n < 9) return true;	//we have already excluded 4,6 and 8.
		else if (n % 3 == 0) return false;
		else
		{
			long r = (long)Math.Floor(Math.Sqrt(n));	// sqrt(n) rounded to the greatest integer r so that r*r<=n
			for (long f = 5; f <= r; f = f+6)
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
	public static bool IsPalindrome(string num)
	{
		for (int i = 0; i < num.Length - i -1; ++i)
		{
			if (num[i] != num[num.Length - i -1]) return false;
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
	public static List<long> GetPrimeFactors(long n)  //Based on Question21_CountDivisorsPDF
	{
		List<long> divisors = new List<long>();
	////	if (Helpers.isPrime_Q7PDF(n)) return 2;
	////	"not prime".Dump();
		//This is using prime factorization
	//	int sum = 1;
		int count = 0;
		int p = 2;
		while (p*p <= n && n > 1) //Prevents us from checking prime factors greater than sprt(n)
		{
			if (n % p == 0)
			{
				int j = p*p;
				n /= p;
				while (n % p == 0)
				{
					j *= p;
					n /= p;
					++count;
					divisors.Add(p);
				}
	//			sum *= j-1;
	//			sum /= p-1;
				++count;
				divisors.Add(p);
			}
			if (p == 2)
			{
				p = 3;
			}
			else
			{
				p +=2;
			}
		}
		if (n > 1) //covers the case that one prime factor greater than sqrt(n) remains
		{
	//		sum *= n+1;
			++count;
			divisors.Add(n); //Add itself as a divisor
//			divisors.Dump("WHEN DOES THIS HAPPEN: " + n);
//			//throw new ApplicationException("WHEN DOES THIS HAPPEN");
		}
		return divisors;
	}
//	public static List<long> GetProperDivisors(long n)
//	{
//		List<long> divisors = new List<long>();
//		divisors.Add(1);
//		for (int i = 2; i <= n/2; ++i)
//		{
//			if (n % i == 0)
//			{
//				divisors.Add(i);
//			}
//		}
//		return divisors;
//	}
//	public static long GetProperDivisorSum(long n)
//	{
//		long outputSum = 1;
//		for (int i = 2; i <= n/2; ++i)
//		{
//			if (n % i == 0)
//			{
//				outputSum += i;
//			}
//		}
//		return outputSum;
//	}
	public static int sum_of_divisors(int n)  //Forum023_20041024_Alvaro
	{
		int prod=1;
		for (int k=2; k*k <= n; ++k)
		{
			int p=1;
			while(n % k == 0)
			{
				p=p*k+1;
				n/=k;
			}
			prod*=p;
		}
		if(n>1)
			prod*=1+n;
		return prod;
	}
}
#endregion Helpers
#region BigFib
public class BigFib
{
	const int TRUNC_VALUE = 100000;
	const int TRUNC_LENGTH = 5;
	
//	List<int> arrayN1 = new List<int>();
//	List<int> arrayN2 = new List<int>();
	List<long> arrayN1 = new List<long>();
	List<long> arrayN2 = new List<long>();
	bool arrayN1IsCurrent = true;
	long sequenceNumber = 2;
	
	public long CurrentSequenceNumber
	{
		get
		{
			return sequenceNumber;
		}
	}
	
	public BigFib()
	{
		arrayN1.Add(1);
		arrayN2.Add(1);
	}
	
	public void TestLengths()
	{
		long testlength = 1234567890123456;
		
		int index = 0;
		while (testlength > 0)
		{
			if (index == arrayN1.Count)
			{
				arrayN1.Add(0);
			}
			arrayN1[index] = testlength % TRUNC_VALUE;
			testlength /= TRUNC_VALUE;
			++index;
		}
		
		EstimateLength(EstimateLengthType.Nearest).Dump("Estimated Length");
		ExactLength().Dump("Exact Length");
	}
	public void TestFromSocky()
	{
		BigFib bf = new BigFib();
		
		//Looked up 38th number
//		bf.ProgressTo(38);
//		"39088169".Dump("38th number");
//		bf.OutputNumber().Dump("my 38th number");
//		bf.CurrentList().Dump();
		
		//Socky gave me huge number
		bf.ProgressTo(150014);  //Takes 9 seconds on Work computer, has length of 31351
		//bf.OutputNumber().Dump();
		bf.ExactLength().Dump("length");
	}
	
	public long Next()
	{
		if (arrayN1IsCurrent)
		{
			SumArrays(ref arrayN2, ref arrayN1);
		}
		else
		{
			SumArrays(ref arrayN1, ref arrayN2);
		}
		arrayN1IsCurrent = !arrayN1IsCurrent;
		
		return ++sequenceNumber;
	}
	public void ProgressTo(int FibonacciN)
	{
		if (sequenceNumber > FibonacciN)
		{
			throw new NotSupportedException("Sequence has progressed past that number already");
		}
		
		while (sequenceNumber < FibonacciN)
		{
			Next();
		}
	}
	void SumArrays(ref List<long> arrayOut, ref List<long> arrayIn)
	{
		//Assuming that arrayIn is larger than or equal to arrayOut
//		arrayIn.Count.Dump("arrayIn");
//		arrayOut.Count.Dump("arrayOut");
		
		//increase size of arrayOut if needed
		while (arrayIn.Count > arrayOut.Count)
		{
			arrayOut.Add(0);
		}
		
//		arrayIn.Dump();
//		arrayOut.Dump();
		
		long overflow = 0;
		int i = 0;
		for (; i < arrayIn.Count; ++i)
		{
			long loaded = arrayOut[i] + arrayIn[i] + overflow;
			overflow = 0;
			if (loaded > TRUNC_VALUE)
			{
				overflow = loaded / TRUNC_VALUE;
				loaded = loaded % TRUNC_VALUE;
			}
			arrayOut[i] = loaded;
		}
		if (overflow > 0)
		{
			if (i == arrayOut.Count)
			{
				arrayOut.Add(0);
			}
			arrayOut[i] = overflow;
		}
	}
	
	public enum EstimateLengthType
	{
		Nearest,
		Floor,
		Ceiling
	}
	/// <summary>
	/// This returns the length of the number floored by TRUNC_LENGTH
	/// //This returns the length of the number within half TRUNC_LENGTH
	///  //if TRUNC_LENGTH=5, and the number is length of 21, should return between [19 and 23]
	/// </summary>
	public long EstimateLength(EstimateLengthType type)
	{
		long estLength = 1;
		if (arrayN1IsCurrent)
		{
			estLength = (arrayN1.Count-1) * TRUNC_LENGTH;
		}
		else
		{
			estLength = (arrayN2.Count-1) * TRUNC_LENGTH;
		}
		
		switch (type)
		{
			case EstimateLengthType.Nearest:
				int halfTrunc = (TRUNC_LENGTH + 1)/2;
				estLength += halfTrunc;
				return estLength;
			
			case EstimateLengthType.Ceiling:
				return estLength + TRUNC_LENGTH;
				
			case EstimateLengthType.Floor:
			default:
				return estLength;
		}
	}
	public long ExactLength()
	{
		int len = 0;
		long t = -1;
		if (arrayN1IsCurrent)
		{
			len = (arrayN1.Count-1) * TRUNC_LENGTH;
			t = arrayN1[arrayN1.Count-1];
		}
		else
		{
			len = (arrayN2.Count-1) * TRUNC_LENGTH;
			t = arrayN2[arrayN2.Count-1];
		}
		
		//Find the size of the final number
		while (t > 0)
		{
			++len;
			t /= 10;
		}
		
		return len;
	}
	public string OutputNumber()
	{
		StringBuilder sb = new StringBuilder();
		
		string format = "{0:d" + TRUNC_LENGTH + "}";
		List<long> currentList = CurrentList();
		
		//Add the first number without padding
		sb.Append(currentList[currentList.Count-1]);
		//Add the rest of the numbers, padding to TRUNC_LENGTH
		for (int i = currentList.Count-2; i >= 0 ; --i)
		{
			long l = currentList[i];
			sb.AppendFormat(format, l);
		}
		return sb.ToString();
	}
	public List<long> CurrentList()
	{
		if (arrayN1IsCurrent)
		{
			return arrayN1;
		}
		else
		{
			return arrayN2;
		}
	}
}
#endregion BigFib

#region Helper Enumerators
public class HelperEnumerators
{
	public class LexicographicPermutations : IEnumerator<int[]>
	{
		//Set size
		int N = -1;
		int[] Value = null;
		
		void swap(int i, int j)
		{
			int tmp = Value[i];
			Value[i] = Value[j];
			Value[j] = tmp;
		}
		
		public LexicographicPermutations(int length) :
			this(length, true)
		{ }
		public LexicographicPermutations(int length, bool zeroOffset)
		{
			if ((!zeroOffset && length > 9) || length > 10)
			{
				throw new ArgumentException("Cannot go past 9");
			}
			
			N = length;
			Value = new int[length];
			for (int i = 0; i < length; ++i)
			{
				Value[i] = i + (zeroOffset ? 0 : 1);
			}
		}
		
		#region IEnumerator<long> Members
	
		public int[] Current
		{
			get { return Value; }
		}
		
		public long CurrentLong
		{
			get
			{
				long l = 0;
				for (int i = 0; i < N; ++i)
				{
					l = l * 10 + Value[i];
				}
				return l;
			}
		}
	
		#endregion
	
		#region IDisposable Members
	
		public void Dispose()
		{
			return;
		}
	
		#endregion
	
		#region IEnumerator Members
	
		object System.Collections.IEnumerator.Current
		{
			get { return Current; }
		}
	
		//http://www.cut-the-knot.org/do_you_know/AllPerm.shtml
		public bool MoveNext()
		{
			int i = N - 1;
			while (i > 0 && Value[i-1] >= Value[i]) 
				i = i-1;
		
			if (i == 0) return false;
		
			int j = N;
			while (Value[j-1] <= Value[i-1]) 
				j = j-1;
		
			swap(i-1, j-1);    // swap values at positions (i-1) and (j-1)
		
			i++; j = N;
			while (i < j)
			{
				swap(i-1, j-1);
				i++;
				j--;
			}
			
			return true;
		}
	
		public void Reset()
		{
			
		}
	
		#endregion
	}
}
#endregion Helper Enumerators

public class FactorialCache
{
	public FactorialCache(int highest)
	{
		dict.Add(0, 0);
		for (int i = 1; i <= highest; ++i)
		{
			dict.Add(i, Helpers.Factorial(i));
		}
		dict.Dump();
	}
	Dictionary<int, long> dict = new Dictionary<int, long>();
	public long this[int number]
	{
		get
		{
			return dict[number];
		}
	}
}

#region InfiniteInt
//TODO if wanted to be more memeory efficent:
//  do all maths in (long)
//  use int.MaxValue as the Modular... but then have to convert everything when outputtint to string
public class InfiniteInt
{
	const int TRUNC_SIZE = 100000;
	const int TRUNC_LENGTH = 5;
//	const int TRUNC_SIZE = 100000;
//	const int TRUNC_LENGTH = 5;
	List<int> array = new List<int>();
	
	public InfiniteInt(int start)
	{
		array.Add(start);
	}
	
	public void Add(InfiniteInt num)
	{
		//TODO could check if num.array has more items, if so copy that array and add this.array into that instead
		int remainder = 0;
		for (int j = 0; j < num.array.Count || remainder > 0; ++j)
		{
			//Need to add new item
			if (j == array.Count) array.Add(0);
			//Add into the current item
			array[j] += num.array[j] + remainder;
			remainder = 0;
			//Check for overflow
			if (array[j] >= TRUNC_SIZE)
			{
				remainder = array[j] / TRUNC_SIZE;
				array[j] %= TRUNC_SIZE;
			}
		}
	}
	[Obsolete("Untested", false)]
	public void Add(int num)
	{
		for (int j = 0; j < array.Count && num > 0; ++j)
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
		if (factor < 0) Console.Error.WriteLine("InfiniteInt.Multiply is not not tested for negative numbers");
		
		int pos = array.Count-1;
		if (array[pos] * factor >= TRUNC_SIZE)
		{
			//TODO idk whats the error: if (array.Count == int.MaxValue) throw new ArgumentOutOfRangeException();
			array.Add(0);
		}
		
		//Multiply each array element by factor
		for (int j = pos; j >= 0; --j)
		{
			array[j] *= factor;
			if (array[j] >= TRUNC_SIZE)
			{
				array[j+1] += array[j] / TRUNC_SIZE;
				array[j] %= TRUNC_SIZE;
				
				//Keep pushing the overflow up one
				for (int o = 1; array[j+o] >= TRUNC_SIZE; ++o)
				{
					if (j+o+1 == array.Count) array.Add(0);
					//Util.HorizontalRun(true, j+o+1, "==", array.Count).Dump();
					array[j+o+1] += array[j+o] / TRUNC_SIZE;
					array[j+o] %= TRUNC_SIZE;
					//Util.HorizontalRun(false, j+o+1, ": ", array[j+o+1]).Dump();
				}
			}
		}

////THIS IS FAIL:
//		//Multiply each array element by factor
//		for (int j = 0; j < array.Count; ++j)
//		{
//			array[j] *= factor;
//			
//			//Keep pushing the overflow up one
//			for (int o = 0; array[j+o] > TRUNC_SIZE; ++o)
//			{
//				if (j+o+1 == array.Count) array.Add(0);
//				//Util.HorizontalRun(true, j+o+1, "==", array.Count).Dump();
//				//FUCK going forward like this, array[j+o+1] has not yet been multiplied, but i'm trying to add something that has been
//				array[j+o+1] += array[j+o] / TRUNC_SIZE;
//				array[j+o] %= TRUNC_SIZE;
//				Util.HorizontalRun(false, j+o+1, ": ", array[j+o+1]).Dump();
//			}
//		}
	}
	public void Divide(int factor)
	{
		if (factor < 0) Console.Error.WriteLine("InfiniteInt.Divide is not not tested for negative numbers");
		
		int pos = array.Count-1;
		
		//Divide each array element by factor
		for (int j = pos; j >= 0; --j)
		{
			int remainder = array[j] % factor;
			array[j] /= factor;
			if (remainder > 0 && j == 0)
			{
				throw new NotSupportedException("DIVIDE RESULTED IN A DECIMAL");
			}
			if (remainder > 0)
			{
				array[j-1] += remainder * TRUNC_SIZE;
				
//				//Keep pushing the overflow up one
//				for (int o = 1; array[j+o] >= TRUNC_SIZE; ++o)
//				{
//					if (j+o+1 == array.Count) array.Add(0);
//					//Util.HorizontalRun(true, j+o+1, "==", array.Count).Dump();
//					array[j+o+1] += array[j+o] / TRUNC_SIZE;
//					array[j+o] %= TRUNC_SIZE;
//					//Util.HorizontalRun(false, j+o+1, ": ", array[j+o+1]).Dump();
//				}
			}
		}
	}
	public long Length()
	{
		long filled = (array.Count-2) * TRUNC_LENGTH;
		throw new ApplicationException();
		//THIS IS NOT SUM DIGITS
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
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		
		string format = "{0:d" + TRUNC_LENGTH + "}";
		
		//Add the first number without padding
		int pos = array.Count-1;
		//Skip any leading 0's
		while (array[pos] == 0) --pos;
		sb.Append(array[pos]);
		//Add the rest of the numbers, padding to TRUNC_LENGTH
		for (int i = pos-1; i >= 0 ; --i)
		{
			long l = array[i];
			sb.AppendFormat(format, l);
		}
		return sb.ToString();
	}
	public string ToString(int startIndex, int length)
	{
		StringBuilder sb = new StringBuilder();
		
		string format = "{0:d" + TRUNC_LENGTH + "}";
		
		//Add the first number without padding
		int pos = array.Count-1;
		//Skip any leading 0's
		while (array[pos] == 0) --pos;
		sb.Append(array[pos]);
		//Add the rest of the numbers, padding to TRUNC_LENGTH
		for (int i = pos-1; i >= 0 ; --i)
		{
			long l = array[i];
			sb.AppendFormat(format, l);
		}
		//Remove anything before the startIndex
		sb.Remove(0, sb.Length - (startIndex + length));
		//Trim length to specified
		sb.Length = length;
		return sb.ToString();
	}
}
public static void InfiniteIntTest()
{
	InfiniteInt iint = new InfiniteInt(1);
	iint.Multiply(999);
	iint.Multiply(999);
	iint.Multiply(999);
	iint.Multiply(999);
	iint.Multiply(999);
	iint.Multiply(999);
	//"994014980014994001" //InfiniteInt 07/05/2011
	//"994014980014994001" //Windows calculator
	Debug.Assert(iint.ToString().Dump() == "994014980014994001", "MULITIPLY FAIL");
	iint.Divide(999);
	Debug.Assert(iint.ToString().Dump() == "995009990004999", "DIVIDE FAIL");
	iint.Divide(9);
	Debug.Assert(iint.ToString().Dump() == "110556665556111", "DIVIDE2 FAIL");
	iint.Divide(999);
	iint.Divide(999);
	Debug.Assert(iint.ToString().Dump() == "110778111", "DIVIDE3 FAIL");
//	iint.Divide(2);
//	Debug.Assert(iint.ToString().Dump() == "55389055.5", "DIVIDE3 FAIL");
}
#endregion InfiniteInt