<Query Kind="Program" />

void Main()
{
	//Problem47.kevingong_20060204().Dump();
	Problem91.grimbal_20050218().Dump();
}

// Define other methods and classes here

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