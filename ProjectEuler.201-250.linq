<Query Kind="Program" />

void Main()
{
	Problem234();
}

// Define other methods and classes here
public static long Problem234()
{
	long MAX = 16;//1000000;
	BitArray SQUARES = Helpers.GetPrimeSieve(MAX);
	
	for (int i = 2; i < MAX; ++i)
	{
		
	}
}
public static long Problem234_lps(BitArray SQUARES, int sqrN)
{
	//CRAP need to work in: 2*i+1
	for (int i = sqrN; i > 0; --i)
	{
		
	}
}
public static long Problem234_ups(BitArray SQUARES, int sqrN)
{
	for (int i = sqrN; i < SQUARES.Count; ++i)
	{
		
	}
}
public static long Problem204()
{
	long MAX = 100000000;
	//long MAX = 1000000000;
	int type = 5;
	
	//Super slow
//	int count = 0;
//	for (long i = 1; i <= MAX; ++i)
//	{
//		Util.Progress = (int)(i * 100 / MAX);
//		if (Problem204_IsHammingNumber(type, i))
//		{
//			++count;
//			//i.Dump();
//		}
//	}
	
	Stopwatch sw = new Stopwatch();
	sw.Start();
	int count = 0;
	for (long i = 1; i <= MAX; ++i)
	{
		Util.Progress = (int)(i * 100 / MAX);
		if (Problem204_IsHammingNumber(type, i))
		{
			++count;
			//i.Dump();
			if (sw.ElapsedMilliseconds > 3000)
			{
				(count + " : " + i).Dump();
				sw.Reset();
				sw.Start();
			}
		}
	}
	
	return count;
}
public static bool Problem204_IsHammingNumber(int type, long n)  //Based on Helpers.GetPrimeFactors  //Based on Question21_CountDivisorsPDF
{
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
//				divisors.Add(p);
				if (p > type) return false;
			}
//			sum *= j-1;
//			sum /= p-1;
//			divisors.Add(p);
			if (p > type) return false;
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
//		divisors.Add(n); //Add itself as a divisor
		if (n > type) return false;
//			divisors.Dump("WHEN DOES THIS HAPPEN: " + n);
//			//throw new ApplicationException("WHEN DOES THIS HAPPEN");
	}
	return true;
}

#region Helpers
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
}
#endregion Helpers