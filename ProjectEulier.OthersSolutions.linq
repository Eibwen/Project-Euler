<Query Kind="Program" />

void Main()
{
	//Problem47.kevingong_20060204().Dump();
	//Problem91.grimbal_20050218().Dump();
	//(Problem76.CalcPn(100) -1).Dump();
	//Problem76.hultan_20040830().Dump();
	Problem77.cyph1e_20050222().Dump();
}

// Define other methods and classes here
public static class Problem77
{
	static List<int> _primes = null;
	public static List<int> primes
	{
		get
		{
			if (_primes == null)
			{
				_primes = Helpers.PrimesLessThan(200);
				_primes.Insert(0, 0);
			}
			return _primes;
		}
	}
	public static int cyph1e_20050222()
	{
		int i=1;
		while(part(++i,primes.Count-1) <= 5000);
		return i;
	}
	
	private static long part(long a, long b)
	{
		if(a == 0)
			return 1;
		else if(a < 0 || b == 0)
			return 0;
		else return (part(a, b-1) +
					part(a-primes[(int)b], b));
	}
	
	//hk_20050410
	// This is Sloane's A000607.
	// On this page a formula can be found:
	// a(n):=1/n*Sum_{k=1..n} A008472(k)*a(n-k).
	// Here A008472 is the sum of the distict primefactors of k.
	// I used this formula and it worked pretty fast.
	
	//arn.zarn_20060419
	//Maple Code:
	// N := 5000;
	// g := 1: p := 2: maxcoeff := 0:
	// for i while maxcoeff < N do
	//   g := g * 1/(1 - x^p);
	//   s := series(g, x=0, i+1);
	//   maxcoeff := max(coeffs(convert(s,polynom))); 
	//   p := nextprime(p);
	// od:
	// answer := i - 1:
	// print(answer): # 0.8 seconds
	public static long arn.zarn_20060419()
	{
		long N = 5000;
		int g = 1;
		int p = 2;
		int maxcoeff = 0;
		
		int i = 0;
		for (; maxcoeff < N; ++i)
		{
			g *= 1/(1 - x^p);
			s = series(g, x=0, i+1);
			maxcoeff = max(coeffs(convert(s,polynom)));
			p = nextprime(p);
		}
		return i-1;
	}
	
	//schnappi 20070712
//	primes = primes_below_limit(5000)
//	solutions = [0]*primes[-1]
//	solutions[0] = 1
//	limit = len(solutions)
//	
//	for p in primes:
//		if p > limit:
//			break
//	
//	# this is a copy of problem 31
//	for idx in xrange(p, limit):
//		solutions[idx] += solutions[idx - p]
//	
//	if solutions[idx] > cutoff:
//		limit = min(limit, idx)
//		break
//	
//	print "Solutions:", limit 
}
public static class Problem76
{
	//harpanet_20040827 == 0.001 seconds
	//This is my interpretation of the function. Things to note:
	// aiPn is an array to cache the values already calculated.
	// aiPn[0] is initialised to 1 (I got this info from another website)
	// aiPn[<0] = 0. Another convention not mentioned on the Mathworld page.
	// It is recursive
	//snq_20040827
	// Mine looks almost identical to harpanet's, except my loop runs while k <= sqrt(n) instead of k <= n. 
	static int[] aiPn = null;
	public static int CalcPn(long n)
	{
		//I added this:
		if (aiPn == null)
		{
			aiPn = new int[n+1];
			aiPn[0] = 1;
		}
		
		// P(<0) = 0 by convention
		if(n < 0)
			return 0;
		
		// Use cached value if already calculated
		if(aiPn[n] > 0)
			return aiPn[n];
		
		int Pn = 0;
		for(long k = 1; k <= Math.Sqrt(n); k++)
		{
			// A little bit of recursion
			long n1 = n - k * (3 * k - 1) / 2;
			long n2 = n - k * (3 * k + 1) / 2;
			
			int Pn1 = CalcPn(n1);
			int Pn2 = CalcPn(n2);
			
			// elements are alternately added and subtracted
			if(k % 2 == 1)
				Pn = Pn + Pn1 + Pn2;
			else
				Pn = Pn - Pn1 - Pn2;
		}
		
		// Cache calculated valued
		aiPn[n] = Pn;
		return Pn;
	}
	
	//hultan_20040830 == 8 seconds
	public static long hultan_20040830()
	{
	// Calling code
		long sum=0;
		for (long k=1;k<100;k++)
			sum+=TNK(100,k);
		return sum;
	}
	// The recursive routine that calculates T(n,k)
	private static long TNK(long n, long k)
	{
		if (k==1 || n==k) return 1;
		if (k>n) return 0;
		
		return TNK(n-1,k-1) + TNK(n-k,k);
	}
}
//public static class Problem96
//{
//	//DeX_20061031
//	private static bool solveSudoku(int s) {
//		int x = s % 9;
//		int y = s / 9;
//		if (s == 81) return true;	//reached max depth
//		if (sudoku[y][x] == 0) {
//			for (int n = 1; n <= 9; n++) {
//				if (testPlace(x, y, n)) {
//					sudoku[y][x] = n;
//					if (solveSudoku(s+1)) return true;
//				}
//			}
//			//if we reach here then we've tried all numbers 1-9 without success
//			sudoku[y][x] = 0;
//			return false;
//		}
//		return solveSudoku(s+1);
//	}
//	
//	//Checks whether the given number fits in the sudoku
//	private static bool testPlace(int nx, int ny, int n) {
//		//Check the box
//		int bx = (int)(nx / 3) * 3;
//		int by = (int)(ny / 3) * 3;
//		for (int y = by; y < by + 3; y++) {
//			for (int x = bx; x < bx + 3; x++) {
//				if (sudoku[y][x] == n) return false;
//			}
//		}
//		//Check the row
//		for (int x = 0; x < 9; x++) {
//			if (sudoku[ny][x] == n) return false;
//		}
//		//Check the column
//		for (int y = 0; y < 9; y++) {
//			if (sudoku[y][nx] == n) return false;
//		}
//		return true;
//	}
//}
public static class Problem91
{
	//Here is my code.
	//- MAX*MAX counts the triangles with a right angle at (0,0).
	//- "x0*y1 - x1*y0 != 0" tests that the surface is non-zero
	//- "x0*(x1-x0) + y0*(y1-y0) == 0" checks for a right angle at (x0,y0).
	//There is no duplication problem since (x0,y0) and (x1,y1) don't have the same role. 
	public static int grimbal_20050218()
	{
		int MAX = 50;
		
		int x0, y0, x1, y1;
		int count = MAX*MAX;
		
		for( x0=0 ; x0<=MAX ; x0++ )
		for( y0=0 ; y0<=MAX ; y0++ )
		for( x1=0 ; x1<=MAX ; x1++ )
		for( y1=0 ; y1<=MAX ; y1++ )
		{
			if( x0*y1 - x1*y0 != 0 && x0*(x1-x0) + y0*(y1-y0) == 0 )
				count++;
		}
		return count;
	}
}
public static class Problem47
{
	//Hui's description of similar method:
	//My solution goes the other way around: After filling an array with the primes below one thousend I set a boolean array up to 1000000 false. Every product of four primes and multiples like 2*2*2*3*3*5*7 below 1000000 are set true. Then I look for the first 4 trues in sequence. 
	//kevingong's description
	//I basically used a sieve method so that I don't need to factor anything. The downside, of course, is that you need to set the maximum value appropiately. But it runs so fast that I simply ran it 3 times, increasing the max from 10000 to 100000 and then 1000000 until I got the answer.
	//COMPARISON: Mine would run in 0.140seconds, this runs in 0.009seconds, on work computer
	public static int kevingong_20060204()
	{
		int max = 1000000;
		int[] numFactors = new int[max];
 
		for ( int index = 0; index < max; index++ )
			numFactors[index] = 0;
 
		int maxSqrt = (int) Math.Sqrt(max);
		for ( int index = 2; index < maxSqrt; index++ )
		{
			if ( numFactors[index] != 0 )
				continue;
 
			for ( int a = index * 2; a < max; a += index )
				numFactors[a]++;
		}
 
		int run = 0;
		for ( int index = 644; index < max; index++ )
		{
			if ( numFactors[index] < 4 )
			{
				run = 0;
				continue;
			}
 
			run++;
			if ( run == 4 )
				return index - 3;
		}
 
		return 0;
	}
}
public class Problem38_kevingong_20060203
{
	//Hrm i like this one... bitshifting, this is win
	public static bool isPanDigital9(int n)
	{
		int result = 0;
	
		while ( n > 0 )
		{
			int digit = (n % 10);
			if ( digit == 0 )
				return false;
	
			result |= (1 << (digit - 1));
	
			n = n / 10;
		}
	
		return (result == 0x1ff);
	}
}
public class Problem38_paddington1_20060125
{
 	[Obsolete("Yeah this totally doesn't work as far as i can see", true)]
	public static void main()
	{
		long max=0;
		for (int i=1; i<10000; i++)
		{
			string str = "";
			for (int j=1; j<10; j++)
			{
				if (str.Length<9)
				{
					str=str + (i*j).ToString();
				}
				if (str.Length==9&& (pandigital(str)) && long.Parse(str)>max) 
				{
					max=int.Parse(str);
					Console.WriteLine(max+" i="+i+" j="+j);
					(i*j).Dump();
				}
			}
		}
	}
 
	private static bool pandigital(string str)
	{
		int[] array=new int[9];
		for (int i=0; i<str.Length; i++)
		{
			if (int.Parse(str.Substring(i,1))==0) return false;
			array[int.Parse(str.Substring(i,1))-1]=1;
		}
		for (int i=0; i<9; i++)
			if (array[i]!=1) return false;
		return true;
	}
}
public static class Helpers
{
	public static BitArray GetPrimeSieve(int limit)
	{
		int sieveBound = (limit-1) / 2; //last index of sieve
		BitArray sieve = new BitArray(sieveBound);
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
		BitArray sieve = GetPrimeSieve(limit);
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
}