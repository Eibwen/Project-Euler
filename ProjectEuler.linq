<Query Kind="Program" />

void Main()
{
	//TODO test List<int> PrimesLessThan(int maxValue) on laptop vs work, switch it to long?
//	Question29().Dump("Result");

	InfiniteInt prod = new InfiniteInt(99);
//	for (int i = 1; i < 100; ++i)
//	{
//		prod.Multiply(99);
//	}
	prod.Multiply(999999);
	prod.Multiply(999999);
	prod.ToString().Dump();
	prod.ToString().Length.Dump();
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
	int UNDER = 10000;
	long outputSum = 0;
	for (int i = 2; i < UNDER; ++i)
	{
		//if (Helpers.isPrime_Q7PDF(i)) continue;
		int sum = Question21_DivisorsSum(i);
		if (sum > UNDER || sum == 1
			|| sum == i) continue;
		if (Question21_DivisorsSum(sum) == i) outputSum += i;
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
public static long Question34()
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
//					break;
//				}
//			}
//			if (!hasFactor) output.Add(i);
			if (isPrime_Q7PDF(i)) output.Add(i);
		}
		
		return output;
	}
	
	public static bool isPrime_Q7PDF(int n)
	{
		if (n < 0) return false;
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
		
		public LexicographicPermutations(int length)
		{
			N = length;
			Value = new int[length];
			for (int i = 0; i < length; ++i)
			{
				Value[i] = i;
			}
		}
		
		#region IEnumerator<long> Members
	
		public int[] Current
		{
			get { return Value; }
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
public class InfiniteInt
{
	const int TRUNC_SIZE = 100;
	const int TRUNC_LENGTH = 2;
//	const int TRUNC_SIZE = 100000;
//	const int TRUNC_LENGTH = 5;
	List<int> array = new List<int>();
	
	public InfiniteInt(int start)
	{
		array.Add(start);
	}
	
	[Obsolete("Untested", true)]
	public void Add(InfiniteInt num)
	{
		
	}
	[Obsolete("Untested", true)]
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
			}
				//I think this thought isn't working because its reverseing through...
				//FUCK WHEN I ADD IT IT GETS ADDED TO THE END, not where i am
////			//Keep pushing the overflow up one
////			for (int o = 0; array[j+o] > TRUNC_SIZE; ++o)
////			{
////				if (j+o+1 == array.Count) array.Add(0);
////				//Util.HorizontalRun(true, j+o+1, "==", array.Count).Dump();
////				array[j+o+1] += array[j+o] / TRUNC_SIZE;
////				array[j+o] %= TRUNC_SIZE;
////				Util.HorizontalRun(false, j+o+1, ": ", array[j+o+1]).Dump();
////			}
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
		sb.Append(array[array.Count-1]);
		//Add the rest of the numbers, padding to TRUNC_LENGTH
		for (int i = array.Count-2; i >= 0 ; --i)
		{
			long l = array[i];
			sb.AppendFormat(format, l);
		}
		return sb.ToString();
	}
}
#endregion InfiniteInt