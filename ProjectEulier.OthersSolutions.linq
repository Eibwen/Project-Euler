<Query Kind="Program" />

void Main()
{
	.main();
}

// Define other methods and classes here

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