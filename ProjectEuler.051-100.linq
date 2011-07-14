<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	Problem83().Dump("Result");
}

// Define other methods and classes here
public static long Problem83_basedOnOthers81()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem81_matrix_test.txt");
	
	int SIZE = 5;
	long[] array;// = new int[SIZE*SIZE];
	long[] smallestPath = new long[SIZE*SIZE];
	BitArray visited = new BitArray(SIZE*SIZE);
	
	array = (from s in File.ReadAllText(PATH).Split(',', '\n', '\r')
				where s.Length > 0
				select Int64.Parse(s)).ToArray();
	
	if (array.Length != SIZE*SIZE)
	{
		throw new ApplicationException("Error size");
	}
	
	try
	{
		long output = Problem83_trace(SIZE, array, smallestPath, visited, 0, 0);
		
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < SIZE; ++i)
		{
			for (int j = 0; j < SIZE; ++j)
			{
				sb.Append(smallestPath[j+i*SIZE]).Append("\t");
			}
			sb.AppendLine();
		}
		sb.ToString().Dump();
		
		return output;
	}
	catch (Exception ex)
	{
		ex.ToString().Dump();
		return -83;
	}
}
public static long Problem83_trace(int SIZE, long[] array, long[] smallestPath, BitArray visited, int x, int y) //Based on Problem81_FindBestFromAbove
{
	if (x < 0 || y < 0) throw new ApplicationException("Went off top or left");
	
	int indexThis = x+(y*SIZE);
	
//	//Already seen this path
//	if (smallestPath[indexThis] != 0) return smallestPath[indexThis];
	//Reached end point
	if (x == SIZE-1 && y == SIZE-1)
	{
		return array[indexThis];
	}
	
	//These don't work when you can move in 4 directions...
//	//Reached right edge, sum them until it reaches bottom corner
//	if (x >= SIZE-1)
//	{
//		long count = 0;
//		for (int a = y + 0; a < SIZE; ++a)
//		{
//			count += array[x+(a*SIZE)];
//		}
//		return count;
//	}
//	//Reached bottom edge, sum them until it reaches bottom corner
//	if (y >= SIZE-1)
//	{
//		long count = 0;
//		for (int a = x + 0; a < SIZE; ++a)
//		{
//			count += array[a+(y*SIZE)];
//		}
//		return count;
//	}
	
	if (visited[indexThis])
	{
		return 99999999;
	}
	//Make a recursive copy
	BitArray vistedRecurse = new BitArray(visited);
	vistedRecurse[indexThis] = true;
	
	//Done with special cases, recurse in all directions
	
	int indexLeft = x-1+(y*SIZE);
	int indexRight = x+1+(y*SIZE);
	int indexUp = x+((y-1)*SIZE);
	int indexDown = x+((y+1)*SIZE);
	
	//Left
	if (x > 0)
	{
		Problem83_Min(array, smallestPath, indexThis, Problem83_trace(SIZE, array, smallestPath, vistedRecurse, x-1, y));
	}
	//Right
	if (x < SIZE-1)
	{
		Problem83_Min(array, smallestPath, indexThis, Problem83_trace(SIZE, array, smallestPath, vistedRecurse, x+1, y));
	}
	//Up
	if (y > 0)
	{
		Problem83_Min(array, smallestPath, indexThis, Problem83_trace(SIZE, array, smallestPath, vistedRecurse, x, y-1));
	}
	//Down
	if (y < SIZE-1)
	{
		Problem83_Min(array, smallestPath, indexThis, Problem83_trace(SIZE, array, smallestPath, vistedRecurse, x, y+1));
	}
	
	if (smallestPath[indexThis] == 0) smallestPath[indexThis] = array[indexThis];
	
	//vistedRecurse[indexThis] = false;
	
	return smallestPath[indexThis];
}
public static void Problem83_Min(long[] array, long[] smallestPath, int indexThis, long recursedValue)
{
	//Find the final recurse value
	long temp = array[indexThis] + recursedValue;
	//If nothing, just take the recurse value
	if (smallestPath[indexThis] == 0) smallestPath[indexThis] = temp;
	//Otherwise take the lower
	else smallestPath[indexThis] = Math.Min(smallestPath[indexThis], temp);
}
#region Problem83
public static long Problem83()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem81_matrix.txt");
	
	int SIZE = 80;
	long[] array;// = new int[SIZE*SIZE];
	long[] smallestPath = new long[SIZE*SIZE];
	
	array = (from s in File.ReadAllText(PATH).Split(',', '\n', '\r')
				where s.Length > 0
				select Int64.Parse(s)).ToArray();
	
	if (array.Length != SIZE*SIZE)
	{
		throw new ApplicationException("Error size");
	}
	
	
	//A single loop solves for 81
	//Three loops solves for 83 test...
	//Four loops solves for 83 full
	long bestSolution = long.MaxValue;
	while (true)
	{
		for (int x = 0; x < SIZE; ++x)
		{
			for (int y = 0; y < SIZE; ++y)
			{
				Problem83_trace(SIZE, array, smallestPath, x, y);
			}
		}
		if (smallestPath[SIZE*SIZE-1] < bestSolution)
		{
			bestSolution = smallestPath[SIZE*SIZE-1];
		}
		else
		{
			return bestSolution;
		}
	}
	
//	StringBuilder sb = new StringBuilder();
//	for (int i = 0; i < SIZE; ++i)
//	{
//		for (int j = 0; j < SIZE; ++j)
//		{
//			sb.Append(smallestPath[j+i*SIZE]).Append("\t");
//		}
//		sb.AppendLine();
//	}
//	sb.ToString().Dump();
	
	return smallestPath[SIZE*SIZE-1];
}
public static void Problem83_trace(int SIZE, long[] array, long[] smallestPath, int x, int y) //Based on Problem81_FindBestFromAbove
{
	//Attempting a non-recursive one... not sure how it will work
	//And would most likely be O(n^2) so not sure if its worth bothering at all hah
	
	int indexThis = x+(y*SIZE);
	
	int indexLeft = x-1+(y*SIZE);
	int indexRight = x+1+(y*SIZE);
	int indexUp = x+((y-1)*SIZE);
	int indexDown = x+((y+1)*SIZE);
	
	long setValue = smallestPath[indexThis];
	if (setValue == 0) setValue = long.MaxValue;
	
	//Left
	if (x > 0)
	{
		setValue = Problem83_min(smallestPath, setValue, indexLeft);
	}
	//Right
	if (x < SIZE-1)
	{
		setValue = Problem83_min(smallestPath, setValue, indexRight);
	}
	//Up
	if (y > 0)
	{
		setValue = Problem83_min(smallestPath, setValue, indexUp);
	}
	//Down
	if (y < SIZE-1)
	{
		setValue = Problem83_min(smallestPath, setValue, indexDown);
	}
	
	smallestPath[indexThis] = setValue + array[indexThis];
	
	//If found no values, take this as the starting point
	if (setValue == long.MaxValue) smallestPath[indexThis] = array[indexThis];
}
public static long Problem83_min(long[] smallestPath, long curValue, int index)
{
	if (smallestPath[index] > 0 && smallestPath[index] < curValue)
	{
		return smallestPath[index];
	}
	return curValue;
}
#endregion Problem83
public static long Problem82()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem82_matrix.txt");
	
	int SIZE = 80;
	long[] array;// = new int[SIZE*SIZE];
	
	array = (from s in File.ReadAllText(PATH).Split(',', '\n', '\r')
				where s.Length > 0
				select Int64.Parse(s)).ToArray();
	
	if (array.Length != SIZE*SIZE)
	{
		throw new ApplicationException("Error size");
	}
	
	for (int waveCol = 0; waveCol < SIZE; ++waveCol)
	{
		//Go down each column
		for (int y = 0; y < SIZE; ++y)
		{
			array[waveCol+y*SIZE] = Problem82_FindBestFromInverseF(SIZE, array, waveCol, y);
		}
	}
	
//	StringBuilder sb = new StringBuilder();
//	for (int i = 0; i < SIZE; ++i)
//	{
//		for (int j = 0; j < SIZE; ++j)
//		{
//			sb.Append(array[j+i*SIZE]).Append("\t");
//		}
//		sb.AppendLine();
//	}
//	sb.ToString().Dump();
	
	long lowestRightValue = long.MaxValue;
	for (int y = 0; y < SIZE; ++y)
	{
		int index = (SIZE-1)+y*SIZE;
		if (lowestRightValue > array[index])
		{
			lowestRightValue = array[index];
		}
	}
	return lowestRightValue;
}
public static long Problem82_FindBestFromInverseF(int SIZE, long[] array, int x, int y)
{
	long lowest = long.MaxValue;
	
	//Check from the top
	if (y > 0 && x > 0)
	{
		int index = x+((y-1)*SIZE);
		if (lowest > array[index])
		{
			lowest = array[index];
		}
	}
	//Check from the left
	if (x > 0)
	{
		int index = x-1+(y*SIZE);
		if (lowest > array[index])
		{
			lowest = array[index];
		}
	}
	//Check from the lower left.. NEED TO CHECK MULTIPLE STEPS DOWNWARD
	if (y < SIZE-1 && x > 0)
	{
//		int indexD = x+((y+1)*SIZE);
//		int indexDL = (x-1)+((y+1)*SIZE);
//		long pathSum = array[indexD] + array[indexDL];
//		if (lowest > pathSum)
//		{
//			lowest = pathSum;
//		}
		long downSum = 0;
		for (int yb = y+1; yb < SIZE; ++yb)
		{
			int indexD = x+(yb*SIZE);
			downSum += array[indexD];
			//escape early if possible
			if (downSum > lowest) break;
			
			int indexDL = (x-1)+(yb*SIZE);
			long pathSum = downSum + array[indexDL];
			if (lowest > pathSum)
			{
				lowest = pathSum;
			}
		}
	}
	
	if (lowest != long.MaxValue)
	{
		return array[x+y*SIZE] + lowest;
	}
	
	return array[x+y*SIZE];
}
public static long Problem81()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem81_matrix.txt");
	
	int SIZE = 80;
	long[] array;// = new int[SIZE*SIZE];
	
	array = (from s in File.ReadAllText(PATH).Split(',', '\n', '\r')
				where s.Length > 0
				select Int64.Parse(s)).ToArray();
	
	for (int waveRow = 0; waveRow < SIZE; ++waveRow)
	{
		//Go along the row
		for (int x = 0; x < waveRow; ++x)
		{
			//Util.HorizontalRun(true, array[x + waveRow*SIZE], "=>", Problem81_FindBestFromAbove(array, x, waveRow)).Dump();
			array[x + waveRow*SIZE] = Problem81_FindBestFromAbove(SIZE, array, x, waveRow);
		}
		//Go along the Column
		for (int y = 0; y < waveRow; ++y)
		{
			//Util.HorizontalRun(true, array[waveRow + y*SIZE], "=>", Problem81_FindBestFromAbove(array, waveRow, y)).Dump();
			array[waveRow + y*SIZE] = Problem81_FindBestFromAbove(SIZE, array, waveRow, y);
		}
		//Go to the point of it after both have been calculated
		array[waveRow + waveRow*SIZE] = Problem81_FindBestFromAbove(SIZE, array, waveRow, waveRow);
	}
	
//	StringBuilder sb = new StringBuilder();
//	for (int i = 0; i < SIZE; ++i)
//	{
//		for (int j = 0; j < SIZE; ++j)
//		{
//			sb.Append(array[j+i*SIZE]).Append("\t");
//		}
//		sb.AppendLine();
//	}
//	sb.ToString().Dump();
	
	return array[SIZE*SIZE - 1];
}
public static long Problem81_FindBestFromAbove(int SIZE, long[] array, int x, int y)
{
	long lowest = long.MaxValue;
	
	//Check from the left
	if (x > 0)
	{
		int index = x-1+(y*SIZE);
		//Since this is the first check possible, just use it
		lowest = array[index];
	}
	//Check from the top
	if (y > 0)
	{
		int index = x+((y-1)*SIZE);
		if (lowest > array[index])
		{
			lowest = array[index];
		}
	}
	
	if (lowest != long.MaxValue)
	{
		return array[x+y*SIZE] + lowest;
	}
	
	return array[x+y*SIZE];
}
public static long Problem51()
{
	List<int> PRIMES = Helpers.PrimesLessThan(1000000);
	
	int length = 1;
	int lengthLimit = 9;
	int lengthCount = 0;
//	
//	for (int i = 0; i < PRIMES.Count; ++i)
//	{
//		if (PRIMES[i] > lengthLimit)
//		{
//			lengthLimit = lengthLimit * 10 + 9;
//			++length;
//			lengthCount.Dump();
//			lengthCount = 0;
//			
//			
//			
//		}
//		++lengthCount;
//	}
	
	List<Helpers.Tuple<int, int>> ReplaceList = new List<Helpers.Tuple<int, int>>();
	
	for (int i = 0; i < PRIMES.Count; ++i)
	{
		//could replace each number by letters
		//  eg. 56113 => abccd, 56333 => abccd, 98435 => abcdef
		
		if (PRIMES[i] > lengthLimit)
		{
			lengthLimit = lengthLimit * 10 + 9;
			++length;
			lengthCount.Dump();
			lengthCount = 0;
			
			if (length > 4)
			{
				//Check for eight prime value family
				ReplaceList.GroupBy(n => n.objB).Where(g => g.Count() >= 8).Dump();
			}
			
			
			//Reset list
			ReplaceList = new List<Helpers.Tuple<int, int>>();
		}
		++lengthCount;
		
		
		ReplaceList.Add(new Helpers.Tuple<int, int>(PRIMES[i], Problem51_swapNumbers(PRIMES[i])));
	}
	
	return 48;
}
public static int Problem51_swapNumbers(int num)
{
	int[] replace = new int[10];
	
	int output = 0;
	int next = 1;
	
	while (num > 0)
	{
		int index = num % 10;
		if (replace[index] == 0) replace[index] = next++;
		output = output * 10 + replace[index];
		num /= 10;
	}
	
	return output;
}
public static long Problem94()
{
//	Problem94_isTriangleAreaWhole(6, 5).Dump();
//	Problem94_isTriangleAreaWhole(5, 6).Dump();
	long MAX = 1000000000;
	//long MAX = 600000;
	
	// http://oeis.org/A120893
	int[] expected = new int[] { 5, 17, 65, 241, 901, 3361, 12545, 46817, 174725, 652081, 2433601, 9082321, 33895685, 126500417, 472105985 };
	(expected.Sum() * 3).Dump("Near Expected Result");
	//  15 answers...
	
	
#region Testing doubles
	//I need to impliment this: http://social.msdn.microsoft.com/Forums/en-US/csharplanguage/thread/c13d3fec-21d9-4f74-92de-a6132d5e9915/
	Problem94TESTTEST();
//	Problem94TEST_long();
//	"".Dump();
//	Problem94TEST_double();
	return -94;
#endregion Testing doubles
	
	
	long outputSum = 0;
	long count = 0;
	
	for (long i = 2; i*3 <= MAX+3; ++i)
	{
		Util.Progress = (int)(i * 300 / MAX);
		
//		double areaPlus = Math.Sqrt(((3*a+1)*(a+1)*(a+1)*(a-1)) / 16);
//		double areaMinus = Math.Sqrt(((3*a-1)*(a-1)*(a-1)*(a+1)) / 16);
//		double areaPlus = Math.Sqrt((double)(((3m*i+1)*(i+1)*(i+1)*(i-1)) / 16));
//		double areaMinus = Math.Sqrt((double)(((3m*i-1)*(i-1)*(i-1)*(i+1)) / 16));
//		double areaPlus = Math.Sqrt((3*i+1)*(i+1)*(i+1)*(i-1)) / 4;
//		double areaMinus = Math.Sqrt((3*i-1)*(i-1)*(i-1)*(i+1)) / 4;
//		double areaPlus = Math.Sqrt(3*i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i-1) / 4;
//		double areaMinus = Math.Sqrt(3*i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i+1) / 4;
		double areaPlus = Math.Sqrt(3*i+1)
						* Math.Sqrt((i+1) * (i+1) * (i-1)) / 4;
		double areaMinus = Math.Sqrt((3*i-1))
						* Math.Sqrt((i-1) * (i-1) * (i+1)) / 4;
		
		//if (Problem94_isTriangleAreaWhole(i-1, i))
		if ((long)areaMinus == areaMinus)
		{
			long per = i*2 + i-1;
			(i + "-" + i + "-" + (i-1) + " : " + per).Dump();
			if (per <= MAX)
			{
				outputSum += per;
				++count;
			}
			else per.Dump();
		}
		//if (Problem94_isTriangleAreaWhole(i+1, i))
		if ((long)areaPlus == areaPlus)
		{
			long per = i*2 + i+1;
			(i + "-" + i + "-" + (i+1) + " : " + per).Dump();
			if (per <= MAX)
			{
				outputSum += per;
				++count;
			}
			else per.Dump();
		}
		if (outputSum > 291730143224420738) "Error".Dump();
	}
	count.Dump("Count");
	//312531789417673499.Dump("First result");	//36 seconds
	//312531791417673503.Dump("Second result");
	//New area function -- 33 seconds
	//291730145224420748
	//291730142224420744
	//291730143224420738  //Including 1000000000?
	//Casting to (int) and comparing is faster than mod: 23 seconds
	//Yet another Problem94_isTriangleAreaWhole, now supporting all triangles, 21 seconds
	//37026344060
	//178477667381
	//178477667386
	//double areaPlus and areaMinus 17 seconds
	//88076575199
	//40552
	//82498623397 //Oops dividing by 16 out of the sqrt
	//88076575199 //FUCK: ((int)areaPlus == areaMinus) WON'T WORK
	//178477667381
	//191856	//Using 3.0
	//Split sqrts out 24 seconds
	//48490
	return outputSum;
}
public static void Problem94TESTTEST()
{
	string g = "good";
	string b = "bad";
	
	//long areaAbout = 2433601*2433601/2;
	
	//KNOWN
	long known1 = 2433601;
	GetArea(known1, g, b);
	long known2 = 126500417;
	GetArea(known2, null, null);
	
	//Known NOT
	long not1 = 472105988;
	GetArea(not1, b, b);
	
	//Largest valid:
	long largest = 472105985;
	GetArea(largest, null, null);
}
public static void GetArea(double i, string plus, string minus)
{
	double areaPlus = Math.Sqrt((3*i+1) * (i+1))
					* Math.Sqrt((i+1) * (i-1)) / 4;
	double areaMinus = Math.Sqrt((3*i-1) * (i-1))
					* Math.Sqrt((i-1) * (i+1)) / 4;
	
	((long)areaPlus).Dump(plus);
	areaMinus.Dump(minus);
}
public static void Problem94TEST_long()
{
	long i = 472105985;

//		double areaPlus = Math.Sqrt(((3*a+1)*(a+1)*(a+1)*(a-1)) / 16);
//		double areaMinus = Math.Sqrt(((3*a-1)*(a-1)*(a-1)*(a+1)) / 16);
//		double areaPlus = Math.Sqrt((double)(((3m*i+1)*(i+1)*(i+1)*(i-1)) / 16));
//		double areaMinus = Math.Sqrt((double)(((3m*i-1)*(i-1)*(i-1)*(i+1)) / 16));
//		double areaPlus = Math.Sqrt((3*i+1)*(i+1)*(i+1)*(i-1)) / 4;
//		double areaMinus = Math.Sqrt((3*i-1)*(i-1)*(i-1)*(i+1)) / 4;
//		double areaPlus = Math.Sqrt(3*i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i-1) / 4;
//		double areaMinus = Math.Sqrt(3*i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i+1) / 4;
	
	var a = Math.Sqrt((3*i+1)*(i-1)).Dump();
	var b = Math.Sqrt((i-1)*(i+1)).Dump();
	var x = (a % 10000 / 2).Dump();
	var y = (b % 10000 / 2).Dump();
	(x * y).Dump("area mod");
	
	double areaPlus = Math.Sqrt((3*i+1) * (i+1)) % 1000000
					* Math.Sqrt((i+1) * (i-1)) % 1000000 / 4;
	double areaMinus = Math.Sqrt((3*i-1) * (i-1)) % 1000000
					* Math.Sqrt((i-1) * (i+1)) % 1000000 / 4;

	areaPlus.Dump();
	((long)areaPlus).Dump();
	((long)areaPlus == areaPlus).Dump();
	areaMinus.Dump();
	((long)areaMinus).Dump();
	((long)areaMinus == areaMinus).Dump();
}
public static void Problem94TEST_double()
{
	double i = 472105985;
	
//		double areaPlus = Math.Sqrt(((3*a+1)*(a+1)*(a+1)*(a-1)) / 16);
//		double areaMinus = Math.Sqrt(((3*a-1)*(a-1)*(a-1)*(a+1)) / 16);
//		double areaPlus = Math.Sqrt((double)(((3m*i+1)*(i+1)*(i+1)*(i-1)) / 16));
//		double areaMinus = Math.Sqrt((double)(((3m*i-1)*(i-1)*(i-1)*(i+1)) / 16));
//		double areaPlus = Math.Sqrt((3*i+1)*(i+1)*(i+1)*(i-1)) / 4;
//		double areaMinus = Math.Sqrt((3*i-1)*(i-1)*(i-1)*(i+1)) / 4;
//		double areaPlus = Math.Sqrt(3*i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i+1)
//						* Math.Sqrt(i-1) / 4;
//		double areaMinus = Math.Sqrt(3*i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i-1)
//						* Math.Sqrt(i+1) / 4;
	
	var a = Math.Sqrt((3*i+1)*(i-1)).Dump();
	var b = Math.Sqrt((i-1)*(i+1)).Dump();
	var x = (a % 10000).Dump();
	var y = ((b % 10000) / 4).Dump();
	(x * y).Dump("area mod");
	
	double areaPlus = Math.Sqrt((3*i+1) * (i+1)) % 1000000
					* Math.Sqrt((i+1) * (i-1)) % 1000000 / 4;
	double areaMinus = Math.Sqrt((3*i-1) * (i-1)) % 1000000
					* Math.Sqrt((i-1) * (i+1)) % 1000000 / 4;
	
	areaPlus.Dump();
	((long)areaPlus).Dump();
	((long)areaPlus == areaPlus).Dump();
	areaMinus.Dump();
	((long)areaMinus).Dump();
	((long)areaMinus == areaMinus).Dump();
}
//public static double Problem94_triangleArea(long a, long b)
//{
//	//Math.Sqrt(((double)b*b)/(a*a) - (double)1/4).Dump();
//	return a*a*Math.Sqrt(((double)b*b)/(a*a) - (double)1/4) / 2;
//}
/////b side occurs twice!!
//public static bool Problem94_isTriangleAreaWhole(long a, long b)
//{
//	//Math.Sqrt(((double)b*b)/(a*a) - (double)1/4).Dump();
//	return a*a*Math.Sqrt(((double)b*b)/(a*a) - (double)1/4) % 2 == 0;
//}
///b side occurs twice!!
//public static bool Problem94_isTriangleAreaWhole(long a, long b)
//{
//	double ha = (double)a/2;
//	double height = Math.Sqrt(b*b - ha*ha);
//	
////	double area = height*a;
////	//area.Dump();
////	return area % 4.0 == 0;
//	double area = height*a/4;
//	//area.Dump();
//	return (long)area == area;
//}
/////b side occurs twice!!
//public static bool Problem94_isTriangleAreaWhole(long a, long b)
//{
//	//Area =.25*sqrt{(3a+1)(a+1)(a+1)(a-1)}
//}
/// a ≥ b ≥ c
//public static bool Problem94_isTriangleAreaWhole(long a, long b, long c)
//{
////	double area = Math.Sqrt(((a + (b + c))*(c - (a - b))*(c + (a - b))*(a + (b - c))) / 16);
////	double area = Math.Sqrt(((a + (b + c))*(c - (a - b))*(c + (a - b))*(a + (b - c)))) / 4;
////	return (long)area == area;
//	
//	double area = -1.1;
//	
//	//Given in problem description
//	if (a == b && b == c) return false;
//	if (a == b)
//	{
//		area = Math.Sqrt(((2*a+c)*c*c*(2*a-c)) / 16);
//	}
//	if (b == c)
//	{
//		area = Math.Sqrt(((a+2*b)*(2*b-a)*a*a) / 16);
//	}
//	
//	return (long)area == area;
//}
public static long Problem97()
{
	//28433×2^7830457+1
	long start = 28433;
	
	int loopCount = 7830457;
	for (int i = 0; i < loopCount / 16; ++i)
	{
		start *= 65536;
		start %= 10000000000;
	}
	for (int i = 0; i < loopCount % 16; ++i)
	{
		start *= 2;
		start %= 10000000000;
	}
	
	start += 1;
	8739992577.Dump();
	return start;
}
public static long Problem92()
{
	long MAX = 10000000;
	
	long count = 0;
	for (int i = 2; i < MAX; ++i)
	{
		long curSum = Problem92_SumSquareDigits(i);
		while (curSum != 1 && curSum != 89)
		{
			curSum = Problem92_SumSquareDigits(curSum);
		}
		if (curSum == 89)
		{
			//i.Dump();
			++count;
		}
//		else
//		{
//			("Not: " + i).Dump();
//		}
	}
	
	return count;
}
public static long Problem92_SumSquareDigits(long num)
{
	long sum = 0;
	while (num > 0)
	{
		sum += (num % 10)*(num % 10);
		num /= 10;
	}
	return sum;
}
public static long Problem78()
{
	int SKIP = 1;
	int MAX = 500;
	long[] Array = new long[MAX];
	Array[0] = 1;
	
	for (long i = SKIP; i < MAX; ++i)
	{
		Util.Progress = (int)(i-SKIP) * 100 / MAX;
		long value = Helpers.CalcPn(ref Array, i);
		(i + " : " + value).Dump();
		if (value % 1000000 == 0)
		{
			return i;
		}
	}
	
	return -78;
}
public static long Problem77()
{
	int[] Primes = Helpers.PrimesLessThan(200).ToArray();
	
	int TARGET_COUNT = 5000;
	
	//for (int i = 2000; i > 0; --i) //A lot smaller than i would have thought... so count up instead
	for (int i = 10; i < 1000; ++i)
	{
		long count = Problem77_recurse(i, ref Primes, Primes.Length-1, 0, TARGET_COUNT, 0);
		if (count > TARGET_COUNT)
		{
			i.Dump();
			return i;
		}
	}
	
	return -1;
}
public static long Problem77_recurse(int target,
	ref int[] Currencies, int index,
	long count, long MaxCount, int sum) //Based on Question31_recurse
{
	if (count > MaxCount) return count;
	
	if (sum > target) return count;
	if (sum == target) return ++count;
	
	count = Problem77_recurse(target, ref Currencies, index, count, MaxCount, sum + Currencies[index]);
	if (index > 0)
	{
		count = Problem77_recurse(target, ref Currencies, index-1, count, MaxCount, sum);
	}
	return count;
}
public static long Problem76()
{
	//TODO this recursive solution takes 25 seconds on laptop
	//		10 seconds on work computer
	
	return Problem76_recurse(Problem76_TARGET-1, 0, 0);
}
const int Problem76_TARGET = 100;
public static long Problem76_recurse(int index, long count, int sum) //Based on Question31_recurse
{
	if (sum > Problem76_TARGET) return count;
	if (sum == Problem76_TARGET) return ++count;
	
	count = Problem76_recurse(index, count, sum + index);
	if (index > 1)
	{
		count = Problem76_recurse(index-1, count, sum);
	}
	return count;
}
public static long Problem68()
{
	//TODO could figure out a way to limit this...
	//  option1: method to check if 10 is in an edge node or not, if its not that means it cannot be 16 digit solution
	//  analayse for ways to find invalid permutations quicker
	
	
	//Problem68_xGon xGon = new Problem68_xGon(5);
//	int[] testGon = new int[] { 6, 2, 5, 1, 4, 3 };
//	Problem68_xGon xGon = new Problem68_xGon(testGon);
//	
//	xGon.IsValid().Dump();
//	
//	return xGon.GetSolutionLong();
	
//	int[] testGon = new int[] { 6, 5, 9, 4, 8, 3, 7, 2, 10, 1 };
//	Problem68_xGon xGon = new Problem68_xGon(testGon);
//	xGon.IsValid().Dump();
//	if (xGon.GetSolutionLong() != 6549438327211015) "FAIL".Dump();
//	return xGon.GetSolutionLong();
	
	long MAX_VALID = 9999999999999999;
	Problem68_xGon xGon = new Problem68_xGon(5);
	
	long bestSolution = 0;
	while (xGon.MoveNextValid())
	{
		long curSolution = xGon.GetSolutionLong();
		if (curSolution < MAX_VALID && curSolution > bestSolution)
		{
			bestSolution = curSolution;
			//bestSolution.Dump();
		}
	}
	//bestSolution.ToString().Length.Dump("Length");
	return bestSolution;
}
public class Problem68_xGon
{
	int size = 0;
	int[] array = null;
	
	public int[] StorageArray { get { return array; } }
	
	public Problem68_xGon(int gonCount)
	{
		size = gonCount;
	}
	public Problem68_xGon(int[] Data)
	{
		size = Data.Length/2;
		array = Data;
	}
	public bool IsValid()
	{
		if (array == null) throw new InvalidDataException("Not initalized");
		
		//Not sure if this is a logical restriciton or what
		if (size*2 > 99) throw new ApplicationException("Max supported node count is 99");
		
		int testSum = -1;
		for (int i = 0; i < size; ++i)
		{
			int curSum = 0;
			for (int j = 0; j < 3; ++j)
			{
				//skip over one for the 3rd item
				if (j == 2) ++j;
				curSum += array[(2*i+j) % (2*size)];
			}
			//Set the testSum to the first one
			//curSum.Dump();
			if (testSum == -1) testSum = curSum;
			if (testSum != curSum) return false;
		}
		return true;
	}
	
	
	public bool MoveNextValid()
	{
		if (array == null)
		{
			array = new int[size*2];
			for (int i = 0; i < array.Length; ++i)
			{
				array[i] = i+1;
			}
			//Test the default one... unlikely
			if (IsValid()) return true;
		}
		
		while (getNextPermutation())
		{
			if (IsValid()) return true;
		}
		return false;
	}
	bool getNextPermutation()
	{
		int N = array.Length;
		
		int i = N - 1;
		while (i > 0 && array[i-1] >= array[i])
			i = i-1;
		
		if (i == 0) return false;
		
		int j = N;
		while (array[j-1] <= array[i-1])
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
	void swap(int i, int j)
	{
		int tmp = array[i];
		array[i] = array[j];
		array[j] = tmp;
	}
	
	int LowestExternalNode()
	{
		//Find lowest external node
		int lowestValue = 99;
		int offsetIndex = 0;
		for (int i = 0; i < size; ++i)
		{
			int index = 2*i;
			int val = array[index];
			if (val < lowestValue)
			{
				lowestValue = val;
				offsetIndex = index;
			}
		}
		return offsetIndex;
	}
	public long GetSolutionLong()
	{
		if (array == null) throw new InvalidDataException("Not initalized");
		
		int largestLong = "1234567891011121314".Length;
		if (size*2 > largestLong) throw new ApplicationException("Max supported solution to long exceeded");
		//Well having the lowest external node be 9 is nearly impossible i think, so it should be safe at this length
		
		int offsetIndex = LowestExternalNode();
		
		long outputSolution = 0;
		for (int i = 0; i < size; ++i)
		{
			for (int j = 0; j < 3; ++j)
			{
				//skip over one for the 3rd item
				if (j == 2) ++j;
				int index = (offsetIndex + 2*i+j) % (2*size);
				
				int value = array[index];
				
				outputSolution *= 10;
				if (value > 9) outputSolution *= 10;
				//(outputSolution + "+" + value).Dump();
				outputSolution += value;
			}
		}
		return outputSolution;
	}
	public string GetSolutionString()
	{
		if (array == null) throw new InvalidDataException("Not initalized");
		
		int offsetIndex = LowestExternalNode();
		
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < size; ++i)
		{
			for (int j = 0; j < 3; ++j)
			{
				//skip over one for the 3rd item
				if (j == 2) ++j;
				int index = (offsetIndex + 2*i+j) % (2*size);
				
				int value = array[index];
				
				sb.Append(value.ToString());
			}
		}
		return sb.ToString();
	}
}
public static long Problem73()  //71 and 72 is CLOSELY related to this
{
	int MAX = 12000;
//	int START = MAX / 3;
//	int END = MAX / 2;
	
	double LastStart = 1;
	//Go through each denominator
	for (double n = 3; n <= MAX; ++n)
	{
		
	}
	
	return -73;
}
public static long Problem63()
{
	int MAX_POWER = 30;
	
	long count = 0;
	for (int i = 1; i <= 10; ++i)
	{
		InfiniteInt sqr = new InfiniteInt(1);
		for (int p = 1; p <= MAX_POWER; ++p)
		{
			sqr.Multiply(i);
			//sqr.Length().Dump();
			if (sqr.Length() == p)
			{
//				(i + "^" + p + ": " + sqr
//					//+ " -- " + LowLimit[p] + " > " + HighLimit[p]
//					).Dump();
				++count;
			}
			if (sqr.Length() > p)
			{
				continue;
			}
		}
	}
	return count;
}
public static long Problem63_long()
{
	int MAX_POWER = 19;
//	long[] LowLimit =  new long[] { 0, 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
//	long[] HighLimit = new long[] { 0, 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999, 999999999, 9999999999 };
	
	List<long> LowLimit = new List<long>();
	List<long> HighLimit = new List<long>();
	long low = 0;
	long high = 0;
	for (int i = 0; i <= MAX_POWER; ++i)
	{
		LowLimit.Add(low);
		HighLimit.Add(high);
		low *= 10;
		if (low == 0) ++low;
		high = high * 10 + 9;
	}
	
	long count = 0;
	for (long i = 1; i <= 10; ++i)
	{
		long sqr = 1;
		for (int p = 1; p <= MAX_POWER; ++p)
		{
			sqr *= i;
			if (sqr >= LowLimit[p] && sqr <= HighLimit[p])
			{
				(i + "^" + p + ": " + sqr
					//+ " -- " + LowLimit[p] + " > " + HighLimit[p]
					).Dump();
				++count;
			}
			if (sqr > HighLimit[p])
			{
				continue;
			}
		}
	}
	return count;
}
public static long Problem100()
{
	//Good amounts: 15+6=21
	//				85+35=120
	((double)6/15).Dump();
	((double)35/85).Dump();
	
	double TARGET = 1e12;
	double fff = (TARGET*TARGET-TARGET)/2;
	TARGET.Dump();
	Math.Sqrt(fff).Dump();
	
	return -100;
}
public static long Problem99()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Question99_base_exp.txt");
	
	string[] numbers = File.ReadAllLines(PATH);
	
	//Hypothesis, if divide the bases, if the exponent is greater than that result
	Problem99_Exponent bestNum = new Problem99_Exponent(2, 2);
	int bestLineNum = 0;
	
//	var FullList = new List<Helpers.Tuple<int, Problem99_Exponent>>();
	
	for (int i = 0; i < numbers.Length; ++i)
	{
		Problem99_Exponent currentNumber = new Problem99_Exponent(numbers[i]);
		
//		FullList.Add(new Helpers.Tuple<int, Problem99_Exponent>(i+1, currentNumber));
		
		if (currentNumber.CompareTo(bestNum) > 0)
		//if (currentNumber.Exponent > bestNum.Exponent)
		{
			//("Line: " + i + " : "+ numbers[i]).Dump();
			bestNum = currentNumber;
			bestLineNum = i+1;
		}
		//bestLineNum.Dump();
	}
	
//	var workingList = FullList.Select(f => new { f.objA, f.objB.Number, f.objB.Exponent,
//						Result = f.objB.Exponent * Math.Log(f.objB.Number, 10) })
//						.OrderByDescending(a => a.Result);
//						//.ThenByDescending(a => a.Number);
//	workingList.Dump();
	
//	Problem99_Exponent a = new Problem99_Exponent(2, 11);
//	Problem99_Exponent b = new Problem99_Exponent(3, 7);
	
//	7d.CompareTo(7+Math.Pow(1.5, 4)).Dump();
//	11d.CompareTo(11-Math.Pow(0.66, 4)).Dump();
	
//	if (a.CompareTo(b) < 0) "a is smaller".Dump();
//	if (a.CompareTo(b) > 0) "b is smaller".Dump();
	
	return bestLineNum;
}
public class Problem99_Exponent : IComparable<Problem99_Exponent>
{
	public Problem99_Exponent(string line)
	{
		string[] splitLine = line.Split(',');
		
		Number = long.Parse(splitLine[0]);
		Exponent = long.Parse(splitLine[1]);
	}
	public Problem99_Exponent(long num, long exp)
	{
		Number = num;
		Exponent = exp;
	}
	public long Number { get; set; }
	public long Exponent { get; set; }
	
	public int CompareTo(Problem99_Exponent other)
	{
		if (this.Number == other.Number && this.Exponent == other.Exponent) return 0;
		
		if (this.Number == other.Number)
		{
			return this.Exponent.CompareTo(other.Exponent);
		}
		else
		{
			//return (this.Exponent - (this.Number / other.Number)).CompareTo(other.Exponent);
////			double div = (double)this.Number / other.Number;
////			
////			int FIRST = ((double)this.Exponent).CompareTo(this.Exponent - Math.Pow(div, this.Exponent - other.Exponent));
////			double divB = (double)other.Number / this.Number;
////			int SECON = ((double)other.Exponent).CompareTo(other.Exponent - Math.Pow(divB, other.Exponent - this.Exponent));
////			
////			if (FIRST != SECON) "ERROR HERE".Dump();
////			
////			return FIRST;
			
			int newBase = 10;
			
			return (this.Exponent * Math.Log(this.Number, newBase)).CompareTo(other.Exponent * Math.Log(other.Number, newBase));
			
//			Problem99_Exponent a = new Problem99_Exponent(2, 11);
//			Problem99_Exponent b = new Problem99_Exponent(3, 7);
//			
//			7d.CompareTo(7+Math.Pow(1.5, 4)).Dump();
//			11d.CompareTo(11-Math.Pow(0.66, 4)).Dump();
		}
	}
}
public static long Problem98()
{
	//Well now i've found that it could be just an array of char[26] and int[10] to do the various mappings
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Question98_words.txt");
	
//	int LONGEST_WORD = 14;
//	
//	long highestNum = 0;
//	for (int i = 0; i < LONGEST_WORD; ++i)
//	{
//		highestNum = highestNum * 10 + 9;
//	}
//	List<long> SQUARES = new List<long>();
//	long lastSquare = 0;
//	for (long i = 1; lastSquare < highestNum; ++i)
//	{
//		lastSquare = i*i;
//		SQUARES.Add(lastSquare);
//	}
//	//SQUARES.Count.Dump();
	
	string wordsFile = File.ReadAllText(PATH);
	var lines = wordsFile.Trim('"').Split(new string[] { "\",\"" }, StringSplitOptions.None).ToList();
	
//	int maxLength = 0;
//	foreach (string line in lines)
//	{
//		if (line.Length > maxLength) maxLength = line.Length;
//	}
//	maxLength.Dump();
	
	var item = new { word = "", checkSum = 99L };
	var FullList = new[] { item }.ToList();
	FullList.Remove(item);
	
	foreach (string line in lines)
	{
		long checkSum1 = 0;
		long checkSum2 = 1;
		for (int i = 0; i < line.Length; ++i)
		{
			long charVal = (line[i] - 'A') + 1;
			
			checkSum1 += charVal * 23;
			checkSum2 *= charVal + 13;
		}
		
		FullList.Add(new { word = line, checkSum = checkSum1+checkSum2 });
	}
	
	//FullList.Dump();
	
	var anagrams = FullList.GroupBy(g => g.checkSum).Where(g => g.Count() > 1)
						.SelectMany(g => g);
	
	//anagrams.Dump();
	
	//Then group by lengths
	var lengths = anagrams.GroupBy(g => g.word.Length).OrderBy(g => g.Key);
	long[] LowLimit =  new long[] { 0, 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
	long[] HighLimit = new long[] { 0, 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999, 999999999, 9999999999 };
	long lastUsedSquare = 1;
	
	List<long> fullAnagramSquaresList = new List<long>();
	
	foreach (var lengthGroup in lengths)
	{
		//Build all squares that are that length
		long currentLowLimit = LowLimit[lengthGroup.Key];
		long currentHighLimit = HighLimit[lengthGroup.Key];
//		List<long> SQUARES = new List<long>();
//		while (lastUsedSquare*lastUsedSquare < currentHighLimit)
//		{
//			SQUARES.Add(lastUsedSquare*lastUsedSquare);
//			++lastUsedSquare;
//		}
		List<string> SQUARES = new List<string>();
		while (lastUsedSquare*lastUsedSquare < currentHighLimit)
		{
			if (lastUsedSquare*lastUsedSquare > currentLowLimit)
			{
				SQUARES.Add((lastUsedSquare*lastUsedSquare).ToString());
			}
			++lastUsedSquare;
		}
//		SQUARES.Count.Dump("Found Squares for Length: " + lengthGroup.Key);
		
		//Compare them all brute force style... somehow
		var anagramGroup = lengthGroup.GroupBy(g => g.checkSum);
		foreach (var anagramPairs in anagramGroup)
		{
			//Build mappings between char and place
			List<Dictionary<char, int>> places = new List<Dictionary<char, int>>();
			List<Dictionary<char, List<int>>> placesDupeCheck = new List<Dictionary<char, List<int>>>();
			foreach (var ffff in anagramPairs)
			{
				Dictionary<char, int> anagramPlaces = new Dictionary<char, int>();
				Dictionary<char, List<int>> dupePlaces = new Dictionary<char, List<int>>();
				for (int i = 0; i < ffff.word.Length; ++i)
				{
					char c = ffff.word[i];
					if (!anagramPlaces.ContainsKey(c))
					{
						anagramPlaces.Add(c, i);
					}
					//TODO when a letter occurs twice, have a check for those two places being the same digit
					if (!dupePlaces.ContainsKey(c)) dupePlaces.Add(c, new List<int>());
					dupePlaces[c].Add(i);
				}
				places.Add(anagramPlaces);
				placesDupeCheck.Add(dupePlaces);
				//anagramPlaces.Dump();
			}
			
			var anagramPairsList = anagramPairs.ToList();
			for (int f = 0; f < anagramPairsList.Count; ++f)
			{
				for (int g = f; g < places.Count; ++g)
				{
					if (f == g) continue;
					
					//Use places from a differnet word with this word
					//  And build a number from a square
					fullAnagramSquaresList.AddRange(
						Problem98_ConvertPlaces(
							anagramPairsList[f].word,
							anagramPairsList[g].word,
							places[g],
							placesDupeCheck[g], SQUARES));
				}
			}
		}
	}
	
	//Currently finding 496, many false it seems
//	fullAnagramSquaresList.OrderByDescending(a => a).Dump();
	
	return fullAnagramSquaresList.Max(f => f);
}
///This will return numbers that are squares AND anagrams, given the correct input
public static List<long> Problem98_ConvertPlaces(
	string word,
	string fromWord,
	Dictionary<char, int> places,
	Dictionary<char, List<int>> dupes,
	List<string> SQUARES)
{
	List<long> outputList = new List<long>();
	StringBuilder sb = new StringBuilder(word.Length);
	int loop = 0;
	foreach (string sqr in SQUARES)
	{
		Util.Progress = loop * 100 / SQUARES.Count;
		sb.Length = 0;
		try
		{
			//Check dupe places
			// This will check if all occurances of the same letter are equal in this square
			bool invalid = false;
			foreach (List<int> checkplaces in dupes.Values)
			{
				if (checkplaces.Count > 1)
				{
					for (int i = 1; i < checkplaces.Count; ++i)
					{
						if (sqr[i-1] != sqr[i]) invalid = true;
					}
				}
			}
//			for (int i = 0; i < sqr.Length; ++i)
//			{
//				for (int j = i+1; j < sqr.Length; ++j)
//				{
//					if (sqr[i] == sqr[j])
//					{
//						//Duplicate numbers in the square
//						
//					}
//				}
//			}
			if (invalid) continue;
			
			//TODO, is having zero be with N and E invalid??
			Dictionary<char, char> usedDigits = new Dictionary<char, char>();
			
			//Build the anagram of the square
			for (int i = 0; i < word.Length; ++i)
			{
				//word[i].Dump();
				char c = sqr[places[word[i]]];
				if (i == 0 && c == '0')
				{
					invalid = true;
					break;
				}
				if (usedDigits.ContainsKey(c))
				{
					if (usedDigits[c] != word[i])
					{
						invalid = true;
						break;
					}
				}
				else { usedDigits.Add(c, word[i]); }
				sb.Append(c);
			}
			if (invalid) continue;
			
			//sb.ToString().Dump();
			long outputSquare = long.Parse(sb.ToString());
			if (Problem98_IsSquare(outputSquare) && !outputList.Contains(outputSquare))
			{
				outputList.Add(outputSquare);
				long thisSquare = long.Parse(sqr);
				if (outputSquare != thisSquare) outputList.Add(thisSquare);
				
//				Util.HorizontalRun(false, sqr, ":", sb.ToString(), " as ", word, ":", fromWord).Dump();
				
//				outputSquare.Dump();
//				long.Parse(sqr).Dump();
			}
		}
		catch (Exception ex)
		{
			ex.ToString().Dump();
			throw;
		}
	}
	//outputList.Dump();
	Util.Progress = null;
	return outputList;
}
public static bool Problem98_IsSquare(long num)
{
	return ((Math.Sqrt(num) % 1) == 0);
}
public static long Problem89()
{
	//Option 1:
	//  First write a parser which takes in all valid forms, converting them to decimal
	//  Then write a roman numeral converter
	//Option 2:
	//  Or find replacements (semi-manually) and code each to make the roman numerals minimal
	
	
//	//Cheating because i'm not sure which part is broken, so cheating to test my methods instead of building test cases manually or whatever
//	// --Well cheating didn't help at fuck all...
//	for (int i = 1; i < 4000; ++i)
//	{
////		//ZERO FAILURES HERE, WIN
////		string roman = Problem89_Cheating_ToRoman(i);
////		long parsed = Problem89_RomanParse(roman);
////		if (i != parsed)
////		{
////			i.Dump("Failed");
////		}
//		//ZERO FAILURES (When length for 8 is 4 which i had)
//		string roman = Problem89_Cheating_ToRoman(i);
//		int length = Problem89_RomanLength(i);
//		if (roman.Length != length)
//		{
//			i.Dump("Failed");
//		}
//	}
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem89_roman.txt");
	
	string[] lines = File.ReadAllLines(PATH);
	
//	Problem89_RomanLength(16).Dump();
//	"MMMCCLXIX".Length.Dump();
	
	long inputSum = 0;
	long outputSum = 0;
	foreach (string line in lines)
	{
		long current = Problem89_RomanParse(line);
		
		inputSum += line.Length;
		outputSum += Problem89_RomanLength(current);
		
//		Util.HorizontalRun(true, line, current,
//			"Len:", Problem89_RomanLength(current),
//			"Savings:", line.Length - Problem89_RomanLength(current)).Dump();
	}
	
//	inputSum.Dump();
//	outputSum.Dump();
	return inputSum - outputSum;
}
public static long Problem89_RomanParse(string line)
{
	long outputSum = 0;
	int lastDenomination = 9;
	long currentSum = 0;
	
	foreach (char c in line)
	{
		switch (c)
		{
			case 'M':
				Problem89_RomanParseSub(7, 1000, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'D':
				Problem89_RomanParseSub(6, 500, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'C':
				Problem89_RomanParseSub(5, 100, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'L':
				Problem89_RomanParseSub(4, 50, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'X':
				Problem89_RomanParseSub(3, 10, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'V':
				Problem89_RomanParseSub(2, 5, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			case 'I':
				Problem89_RomanParseSub(1, 1, ref lastDenomination, ref currentSum, ref outputSum);
				break;
			default:
				throw new ArgumentException("Unknown character");
		}
//		c.Dump();
//		outputSum.Dump();
//		currentSum.Dump();
	}
	return outputSum + currentSum;
}
static void Problem89_RomanParseSub(int THIS_DENOM, int THIS_VALUE,
					ref int lastDenomination, ref long currentSum, ref long outputSum)
{
	if (lastDenomination > THIS_DENOM)
	{
		outputSum += currentSum; 
		currentSum = 0;
	}
	if (lastDenomination < THIS_DENOM) { currentSum = THIS_VALUE - currentSum; }
	else currentSum += THIS_VALUE;
	lastDenomination = THIS_DENOM;
}
public static int Problem89_RomanLength2(long num)
{
	//string[] romanArraySingles = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
	int[] romanLengths = new int[] { 0, 1, 2, 3, 2, 1, 2, 3, 4, 2 };
	
	int outLength = 0;
	while (num > 0)
	{
		outLength += romanLengths[num % 10];
		num /= 10;
	}
	return outLength;
}
public static int Problem89_RomanLength(long num)
{
	string outputStr = "";
	
	//I know the max will be 4 chars...
	
	string numStr = num.ToString("0000");
	if (numStr.Length > 4) throw new NotSupportedException("This only supports 4 digit numbers");
	
	int charCount = 0;
	
	//All these are 2 chars
	//4, 40, 400, 9, 90, 900
	
	//4000 would be MMMM, not M?
	if (numStr[0] == '4')
		charCount += 2;
	
	foreach (char c in numStr)
	{
		switch (c)
		{
			case '1':
			case '5':
				charCount += 1;
				break;
			case '2':
			case '4':
			case '6':
			case '9':
				charCount += 2;
				break;
			case '3':
			case '7':
				charCount += 3;
				break;
			case '8':
				charCount += 4;
				break;
		}
	}
	
	return charCount;
}
//http://www.mgbrown.com/PermaLink88.aspx
public static string Problem89_Cheating_ToRoman(int number)
{
	if (-9999 >= number || number >= 9999)
	{
		throw new ArgumentOutOfRangeException("number");
	}

	if (number == 0)
	{
		return "NUL";
	}

	StringBuilder sb = new StringBuilder(10);

	if (number < 0)
	{
		sb.Append('-');
		number *= -1;
	}

	string[,] table = new string[,] { 
		{ "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" }, 
		{ "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" }, 
		{ "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" },
		{ "", "M", "MM", "MMM", "M(V)", "(V)", "(V)M", 
										  "(V)MM", "(V)MMM", "M(X)" } 
	};

	for (int i = 1000, j = 3; i > 0; i /= 10, j--)
	{
		int digit = number / i;
		sb.Append(table[j, digit]);
		number -= digit * i;
	}

	return sb.ToString();
}
public static long Problem96()
{
	//Speed: 0.5 on work computer
	//		0.6 with Problem96_sudokuCheck(grid, cell)
	//		0.065 with Problem96_FindCell
	//		1 minute for the 11 in ProjectEuler_Problem96_sudoku17sOnForum.txt
	//		32 seconds with Problem96_FindCell (30 for just the last one...)
	//  laptop: 2 seconds for the 50 problem puzzles
	//			1.3 seconds with FindNakedSingles
	//			0.15 seconds with Problem96_FindCell
	//		4:20 minutes on ProjectEuler_Problem96_sudoku17sOnForum.txt
	//		4:55 with Problem96_sudokuCheck(grid, cell) on those
	//		3:50 with FindNakedSingles
	//		1:30 with Problem96_FindCell (1:20 for just the last one...)
	
	
	//Thinking about the minimal storage for it
	//Minimal bits: 324 (4 * 81) (4bits required to store 0-9 in an array)
	// 11uints, 41bytes
	//Another way would be 2 ints for each row
	//  [64 total:](36: 4 4 4  4 4 4  4 4 4) (9: flags) (19: extra)
	//Or 987654321 would take up 30 bits... so 9ints
	//  Then use a byte and the top bit to store the needed/used digits
	//I think the best would be 9 ints, one for each row, then what is used/not somewhere else... 81bits for each row/column/grid (81*3), then could and those groups of 9
	//BUT: I'm not going to bother with any of this for this problem
	
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem96_sudoku17sOnForum.txt");
	string[] lines = File.ReadAllLines(PATH);
	
	int loadedLines = 0;
	StringBuilder gridBuilder = new StringBuilder();
	long outputSum = 0;
	int gridCount = 0;
	for (int l = 0; l < lines.Length; ++l)
	{
		string line = lines[l];
		if (line.StartsWith("Grid"))
		{
			//if (gridBuilder.Length > 0) return outputSum;
			gridBuilder.Length = 0;
			loadedLines = 0;
		}
		else
		{
			++loadedLines;
			gridBuilder.Append(line);
			
			if (loadedLines == 9)
			{
				//Grid ready to process
				int[] grid = gridBuilder.ToString().Select(c => Int32.Parse(c.ToString())).ToArray();
				BitArray possibilities = new BitArray(81*3, true);
				//Mark already used numbers
				for (int i = 0; i < 9; ++i)
				{
					for (int j = 0; j < 9; ++j)
					{
						int cell = 9 * i + j;
						if (grid[cell] != 0)
						{
							MarkBitArray(possibilities, cell, grid[cell], false);
						}
					}
				}
				
				//Problem96_OutputGrid(grid, possibilities).Dump();
				
				//Find any single possiblities
//				while (Problem96_FindNakedSingles(grid, possibilities) > 0)
//				{ }
				//Problem96_OutputGrid(grid, possibilities).Dump();
				
				//Now start solving
				if (!Problem96_recurse(grid, possibilities))
				{
					"".Dump("FAILURE line: " + l);
				}
				outputSum += grid[0]*100 + grid[1]*10 + grid[2];
				++gridCount;
				outputSum.Dump();
				//Problem96_OutputGrid(grid, possibilities).Dump();
			}
		}
	
	}
	
//	Dictionary<int, BitArray> possibles = new Dictionary<int, BitArray>();
//	Problem96_recurse(grid, possibles);
	
//	grid.Dump();
//	possibles.Dump();

////	//Test the check method
////	string gridStr = @"4 8 3 9 2 1 6 5 7
////9 6 7 3 4 5 8 2 1
////2 5 1 8 7 6 4 9 3
////5 4 8 1 3 2 9 7 6
////7 2 9 5 6 4 1 3 8
////1 3 6 7 9 8 2 4 5
////3 7 2 6 8 9 5 1 4
////8 1 4 2 5 3 7 6 9
////6 9 5 4 1 7 3 8 2";
////	int[] grid = gridStr.Split(' ', '\n').Select(c => Int32.Parse(c)).ToArray();
////	
////	Problem96_sudokuCheck(grid).Dump("Check Test");
////	for (int i = 0; i < 9; ++i)
////	{
////		for (int j = 0; j < 9; ++j)
////		{
////			int cell = 9 * i + j;
////			
////			(cell + ": " + Problem96_sudokuCheck(grid, cell)).Dump();
////		}
////	}
	
	
//	BitArray ba;
//	byte[] sup = new byte[1];
//	ba.CopyTo(sup, 0);
//	int Cell1 = sup % 0x1111;
//	int Cell2 = (sup >> 4) % 0x1111;
	
	return outputSum;
}
public static string Problem96_OutputGrid(int[] grid, BitArray possibilities)
{
	StringBuilder sb = new StringBuilder(90);
	for (int i = 0; i < 9; ++i)
	{
		for (int j = 0; j < 9; ++j)
		{
			int cell = 9 * i + j;
			
			sb.Append(grid[cell]);
		}
		sb.AppendLine();
	}
	
	
	if (possibilities == null) return sb.ToString();
	
	//possibilities.Dump();
	sb.AppendLine();
	sb.AppendLine("== Rows ==");
	for (int i = 0; i < 9; ++i)
	{
		sb.AppendFormat("{0}: ", i);
		for (int j = 0; j < 9; ++j)
		{
			//Rows
			if (possibilities[i*9 + j])
				sb.Append(j+1);
		}
		sb.AppendLine();
	}
	sb.AppendLine("== Columns ==");
	for (int i = 0; i < 9; ++i)
	{
		sb.AppendFormat("{0}: ", i);
		for (int j = 0; j < 9; ++j)
		{
			//Rows
			if (possibilities[i*9 + 9*9 + j])
				sb.Append(j+1);
		}
		sb.AppendLine();
	}
	sb.AppendLine("== Grids ==");
	for (int i = 0; i < 9; ++i)
	{
		sb.AppendFormat("{0}: ", i);
		for (int j = 0; j < 9; ++j)
		{
			//Rows
			if (possibilities[i*9 + 2*9*9 + j])
				sb.Append(j+1);
		}
		sb.AppendLine();
	}
	return sb.ToString();
}
//public static int Problem96_FindNakedSingles(int[] grid, BitArray possibilities)
//{
//	int count = 0;
//	for (int i = 0; i < 9; ++i)
//	{
//		for (int j = 0; j < 9; ++j)
//		{
//			int cell = 9 * i + j;
//			
//			if (grid[cell] == 0)
//			{
//				List<int> list = GetPossibles(possibilities, cell);
//				if (list.Count == 1)
//				{
//					grid[cell] = list[0];
//					MarkBitArray(possibilities, cell, list[0], false);
//					++count;
//				}
//			}
//		}
//	}
//	return count;
//}
//public static int Problem96_FindHiddenSingles(int[] grid, BitArray possibilities)
//{
//	//Where there is nowhere else in a row/col/grid that a number can go
//	
//	//Having the BitArray being a Dictionary<int, BitArray> would be much better for this operation
//	//Or a method that gets a bool[] for any given cell from BitArray maybe...
//	//Or ints, or method that would get ints from BitArray
//	
//	//Example with three:
//	// a:011, b:101, c:101
//	// if (a & b.Not() & c.Not() > 0)
//	
//}
public static bool Problem96_recurse(int[] grid, BitArray possibles)
{
	//TODO: idea from forum, instead of just finding the first 0 cell, find ones with minimum amount of possibilities
	//		This strategy would make FindSinglePossiblities redundant
	//With current way... could have a CheckLine method, call that anytime the line changes
	
	List<int> possibleValues = null;
	int cell = Problem96_FindCell(grid, possibles, out possibleValues);
	
	//Find first cell that is zero
	if (cell >= 0)
	{
		Util.Progress = cell *100/81;
		
		//possibleValues.Dump(cell.ToString());
		if (possibleValues.Count == 0)
		{
			//INVALID STATE
			//"InvalidState".Dump();
			return false;
		}
		
		foreach (int value in possibleValues)
		{
			grid[cell] = value;
			MarkBitArray(possibles, cell, value, false);
			//("Trying: " + i + "" + j + " val: " + value).Dump();
			
			if (Problem96_recurse(grid, possibles))
//					if (Problem96_sudokuCheck(grid, cell)
//						&& Problem96_recurse(grid, possibles, cell+1))
			{
				//"WIN!!!!!!!!!!!!!!!!!".Dump();
				return true;
			}
			//Unmark cell and bit array
			grid[cell] = 0;
			MarkBitArray(possibles, cell, value, true);
			//Continue loop
		}
		
//		//"Checking".Dump();
//		return Problem96_sudokuCheck(grid);
	}
	
	//Found no zero cells, so just check it
	return Problem96_sudokuCheck(grid);
	//throw new ApplicationException("Found no zero cells?: " + lastCellPlusOne);
}
public static int Problem96_FindCell(int[] grid, BitArray possibles, out List<int> bestPossibleValues)
{
	bestPossibleValues = null;
	int bestCount = 99;
	int bestCell = -1;
	
	for (int i = 0; i < 9; ++i)
	{
		for (int j = 0; j < 9; ++j)
		{
			int cell = 9 * i + j;
			
			if (grid[cell] == 0)
			{
				List<int> list = GetPossibles(possibles, cell);
				if (list.Count == 1)
				{
					bestPossibleValues = list;
					return cell;
				}
				else if (list.Count < bestCount)
				{
					bestPossibleValues = list;
					bestCount = list.Count;
					bestCell = cell;
				}
			}
		}
	}
	
	return bestCell;
}
public static void MarkBitArray(BitArray possibilities, int cell, int value, bool state)
{
	if (cell < 0 || cell > 81) throw new ArgumentException();
	//Rows will be 0-80
	//Columns will be 81-161
	//Grids will be 162-242
	int row = cell / 9;
	int col = cell % 9;
	int grid = (col/3) + ((row/3)*3);
	
	possibilities.Set(row*9 + value-1, state);
	possibilities.Set(col*9 + 9*9 + value-1, state);
	possibilities.Set(grid*9 + 2*9*9 + value-1, state);
	
	//Util.HorizontalRun(true, row, col, grid).Dump();
}
public static List<int> GetPossibles(BitArray possibilities, int cell)
{
	if (cell < 0 || cell > 81) throw new ArgumentException();
	
	int row = cell / 9;
	int col = cell % 9;
	int grid = (col/3) + ((row/3)*3);
	
	List<int> outputList = new List<int>();
	
	for (int value = 0; value < 9; ++value)
	{
		if (possibilities[row*9 + value]
			&& possibilities[col*9 + 9*9 + value]
			&& possibilities[grid*9 + 2*9*9 + value])
		{
			outputList.Add(value+1);
		}
	}
	
	return outputList;
}
public const int SudokuTotal = 1+2+3+4+5+6+7+8+9;
public static bool Problem96_sudokuCheck(int[] grid)
{
	for (int i = 0; i < 9; ++i)
	{
		int hLineCheck = SudokuTotal;
		int vLineCheck = SudokuTotal;
		int gridCheck = SudokuTotal;
		for (int j = 0; j < 9; ++j)
		{
			hLineCheck -= grid[9*i+j];
			vLineCheck -= grid[9*j+i];
			gridCheck -= grid[9*((i/3)*3 + j/3) + ((i%3)*3 + j%3)];
		}
		if (hLineCheck != 0
			|| vLineCheck != 0
			|| gridCheck != 0)
		{
			return false;
		}
	}
	return true;
}
[Obsolete("NOT TESTED", true)]
public static bool Problem96_sudokuCheckPossible(int[] grid, BitArray possibles)
{
	//TODO: This too would be better with the flag data being in seperate objects...
	
	
	//Check each row
	for (int i = 0; i < 9; ++i)
	{
		int check = SudokuTotal;
		for (int j = 0; j < 9; ++j)
		{
			if (!possibles[i*9 + j])
			{
				//Means this section is not complete
				check = 0;
				break;
			}
			check -= grid[9*i+j];
		}
		if (check != 0) return false;
	}
	//Check each column
	for (int i = 0; i < 9; ++i)
	{
		int check = SudokuTotal;
		for (int j = 0; j < 9; ++j)
		{
			if (!possibles[i*9 + 9*9 + j])
			{
				//Means this section is not complete
				check = 0;
				break;
			}
			check -= grid[9*j+i];
		}
		if (check != 0) return false;
	}
	//Check each grid
	for (int i = 0; i < 9; ++i)
	{
		int check = SudokuTotal;
		for (int j = 0; j < 9; ++j)
		{
			int gridCell = (j/3) + ((i/3)*3);
			int value = ((i%3)*3 + j%3);
			if (!possibles[gridCell*9 + 2*9*9 + value])
			{
				//Means this section is not complete
				check = 0;
				break;
			}
			check -= grid[9*((i/3)*3 + j/3) + ((i%3)*3 + j%3)];
		}
		if (check != 0) return false;
	}
	return true;
}
public static long Problem79()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem79_keylog.txt");
	
	string[] lines = File.ReadAllLines(PATH);
	
	var first = from c in lines
				select c[0];
	var secon = from c in lines
				select c[1];
	var third = from c in lines
				select c[2];
	
	List<char> allChars = new List<char>();
	allChars.AddRange(first);
	allChars.AddRange(secon);
	allChars.AddRange(third);
	int MinLength = allChars.Distinct().Count();
	MinLength.Dump("Min Length");
	
	var firstOut = from c in first
					group c by c into g
					orderby g.Count() descending
					select new { g.Key, Count = g.Count() };
	firstOut.Dump();
	var seconOut = from c in secon
					group c by c into g
					orderby g.Count() descending
					select new { g.Key, Count = g.Count() };
	seconOut.Dump();
	var thirdOut = from c in third
					group c by c into g
					orderby g.Count()
					select new { g.Key, Count = g.Count() };
	thirdOut.Dump();
	
	
	
	//Frequency analysis:
	//Start Char: 7
	//First: 736128**
	//Mid  : *361289*
	//End  : **162809
	//End Char: 0
	//string freqAnaPasscode = "73612890";
	
	//After testing:
	string freqAnaPasscode = "73162890";
	
	
	//Testing
	Problem79_IsValidPasscode(freqAnaPasscode);
	
	
	return -79;
}
public static bool Problem79_IsValidPasscode(string passcode)
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem79_keylog.txt");
	
	string[] lines = File.ReadAllLines(PATH);
	
	
	bool failed = false;
	foreach (string log in lines)
	{
		int logIndex = 0;
		for (int i = 0; i < passcode.Length; ++i)
		{
			if (log[logIndex] == passcode[i]) ++logIndex;
			if (logIndex == log.Length) break;
		}
		if (logIndex != log.Length)
		{
			("Fail: " + log).Dump();
			failed = true;
		}
	}
	return !failed;
}
public static long Problem53()
{
	int count = 0;
	
//	int n = 24;
//	for (int i = 0; i <= n; ++i)
//	{
//		long value = Problem53_C(n, i).Dump();
//		if (value > 1000000)
//		{
//			++count;
//		}
//	}
	for (int n = 1; n <= 100; ++n)
	{
		for (int i = n; i > 0; --i)
		{
			long value = Problem53_C(n, i);
			if (value > 1000000)
			{
				count += i * 2 - n + 1;
				break;
			}
		}
	}
	
	return count;
}
public static long Problem53_C(int n, int r)
{
	if (r > n) return -1;
	
	//if (r < n/2) r = n-r;
	
	long numer = 1;
	for (int i = r+1; i <= n; ++i)
	{
		numer *= i;
	}
	long denom = 1;
	for (int i = 1; i <= (n-r); ++i)
	{
		denom *= i;
	}
	return numer / denom;
}
public static long Problem54()
{
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem54_poker.txt");
	
	int player1Wins = 0;
	
	string[] hands = File.ReadAllLines(PATH);
	foreach (string s in hands)
	{
		var p1 = new Problem54_PokerHand(s.Substring(0, 14));
		var p2 = new Problem54_PokerHand(s.Substring(15, 14));
		
//		s.Dump();
//		p1.Dump();
//		p2.Dump();
		if (p1.CompareTo(p2) == 0) throw new ApplicationException("No winner, FAIL");
		
		if (p1.CompareTo(p2) > 0)
		{
			//"won".Dump();
			++player1Wins;
		}
	}
	
	return player1Wins;
}
public class Problem54_PokerHand : IComparable<Problem54_PokerHand>
{
	public Problem54_PokerHand()
	{
	}
	public Problem54_PokerHand(string hand)
	{
		SetCards(hand);
	}
	
	int HAND_SIZE = 5;
	public void SetCards(string hand)
	{
		cards = new List<PokerCard>(HAND_SIZE);
		
		string[] cardStrings = hand.Split(' ');
		foreach (string s in cardStrings)
		{
			cards.Add(new PokerCard(s));
		}
		
		HandTypeRank = LoadHandType();
		
		if (cards.Count != HAND_SIZE) throw new ArgumentException("Hand size is not correct");
	}
	
	List<PokerCard> cards = null;
	
	enum HandType
	{
		HighCard = 1,
		OnePair = 2,
		TwoPair = 3,
		ThreeOfKind = 4,
		Straight = 5,
		Flush = 6,
		FullHouse = 7,
		FourOfKind = 8,
		StraightFlush = 9,
		RoyalFlush = 10
	}
	public string HandTypeRankStr
	{
		get
		{
			return HandTypeRank.Key.ToString() + " = " + HandTypeRank.Value.Value + " Suit:" + HandTypeRank.Value.Suit;
		}
	}
	KeyValuePair<HandType, PokerCard> HandTypeRank { get; set; }
	KeyValuePair<HandType, PokerCard> LoadHandType()
	{
		//Check Flush -- This uses all cards
		if (IsFlush())
		{
			//Check Royal/Straight Flush
			if (IsStraight())
			{
				PokerCard highCard = GetHighCard();
				if (highCard.Value == PokerCard.Ace)
					return new KeyValuePair<HandType, PokerCard>(HandType.RoyalFlush, highCard);
				else
					return new KeyValuePair<HandType, PokerCard>(HandType.StraightFlush, highCard);
			}
			else
			{
				return new KeyValuePair<HandType, PokerCard>(HandType.Flush, GetHighCard());
			}
		}
		else if (IsStraight())
		{
			return new KeyValuePair<HandType, PokerCard>(HandType.Straight, GetHighCard());
		}
		else
		{
			var groups = cards.GroupBy(g => g.Value);
			int maxCount = groups.Max(g => g.Count());
			if (maxCount >= 2)
			{
				//At least a pair
				//Can only have up to 4 with a single valid deck
				if (maxCount == 4)
				{
					PokerCard highCard = groups.Where(g => g.Count() == 4).First().First();
					return new KeyValuePair<HandType, PokerCard>(HandType.FourOfKind, highCard);
				}
				else if (maxCount == 3)
				{
					PokerCard highCard = groups.Where(g => g.Count() == 3).First().First();
					if (groups.Any(g => g.Count() == 2))
					{
						return new KeyValuePair<HandType, PokerCard>(HandType.FullHouse, highCard);
					}
					else
					{
						return new KeyValuePair<HandType, PokerCard>(HandType.ThreeOfKind, highCard);
					}
				}
				else //if (maxCount == 2)
				{
					PokerCard highCard = groups.OrderByDescending(g => g.Key)
												.Where(g => g.Count() == 2).First().First();
					if (groups.Count(g => g.Count() == 2) > 1)
					{
						return new KeyValuePair<HandType, PokerCard>(HandType.TwoPair, highCard);
					}
					else
					{
						return new KeyValuePair<HandType, PokerCard>(HandType.OnePair, highCard);
					}
				}
			}
			else
			{
				return new KeyValuePair<HandType, PokerCard>(HandType.HighCard, GetHighCard());
			}
		}
	}
	bool IsFlush()
	{
		char firstSuit = cards[0].Suit;
		foreach (PokerCard card in cards)
		{
			if (card.Suit != firstSuit)
				return false;
		}
		return true;
	}
	bool IsStraight()
	{
		var straightTest = cards.OrderBy(c => c.Value).ToList();
		
		for (int i = 1; i < straightTest.Count; ++i)
		{
			if (straightTest[i].Value - 1 != straightTest[i-1].Value)
				return false;
		}
		return true;
	}
	PokerCard GetHighCard()
	{
		return cards.OrderByDescending(c => c.Value).First();
	}
	
	public int CompareTo(Problem54_PokerHand other)
	{
		var thisHand = this.HandTypeRank;
		var otherHand = other.HandTypeRank;
		
		if (thisHand.Key == otherHand.Key)
		{
			if (thisHand.Value.Value == otherHand.Value.Value)
			{
				//Compare the highest cards
				var thisHandCards = this.cards.OrderByDescending(c => c.Value).ToList();
				var otherHandCards = other.cards.OrderByDescending(c => c.Value).ToList();
				
				for (int i = 0; i < thisHandCards.Count; ++i)
				{
					if (thisHandCards[i].Value.CompareTo(otherHandCards[i].Value) != 0)
					{
						return thisHandCards[i].Value.CompareTo(otherHandCards[i].Value);
					}
				}
				return 0;
			}
			else
			{
				//Compare the high card of the hand
				return thisHand.Value.Value.CompareTo(otherHand.Value.Value);
			}
		}
		return thisHand.Key - otherHand.Key;
	}
	
	public class PokerCard
	{
		public const int Ace = 14;
		public const int King = 13;
		public const int Queen = 12;
		public const int Jack = 11;
		public const int Ten = 10;
		
		public PokerCard(string card)
		{
			card = card.Trim();
			if (card.Length > 2) throw new ApplicationException();
			
			if (card[0] == 'T') Value = Ten;
			else if (card[0] == 'J') Value = Jack;
			else if (card[0] == 'Q') Value = Queen;
			else if (card[0] == 'K') Value = King;
			else if (card[0] == 'A') Value = Ace;
			else Value = Int32.Parse(card.Substring(0, 1));
			
			Suit = card[1];
		}
		public int Value { get; set; }
		public char Suit { get; set; }
	}
}
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
	
	List<Helpers.Tuple<List<int>, List<int>>> finishedList = 
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
public static List<Helpers.Tuple<List<int>, List<int>>> Problem90_PadDice(List<Helpers.Tuple<List<int>, List<int>>> dicePairs)
{
	//List<Helpers.Tuple<List<int>, List<int>>> newOutputList = new List<Helpers.Tuple<List<int>, List<int>>>(dicePairs.Count);
	
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
					
					dicePairs.Add(new Helpers.Tuple<List<int>, List<int>>(newDie, dicePairs[n].objB));
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
					
					dicePairs.Add(new Helpers.Tuple<List<int>, List<int>>(dicePairs[n].objA, newDie));
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
public static List<Helpers.Tuple<List<int>, List<int>>> Problem90_Recurse(int[] requiredPairs, int index, List<int> die1, List<int> die2)
{
	List<Helpers.Tuple<List<int>, List<int>>> output = new List<Helpers.Tuple<List<int>, List<int>>>();
	
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
			
			output.Add(new Helpers.Tuple<List<int>, List<int>>(die1, die2));
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
	
	public class Tuple<A, B>
	{
		public Tuple(A a, B b)
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
	public class Tuple<A, B, C>
	{
		public Tuple(A a, B b, C c)
		{
			_a = a;
			_b = b;
			_c = c;
		}
		A _a;
		B _b;
		C _c;
		
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
		public C objC
		{
			get { return _c; }
			set { _c = value; }
		}
	}
	public static long CalcPn(ref long[] aiPn, long n)
	{
		//I added this:
		if (aiPn[0] != 1)
		{
			throw new ApplicationException("Array failure");
		}
		if (n > 405)
		{
			throw new ApplicationException("Overflows long at this point");
		}
		
		// P(<0) = 0 by convention
		if(n < 0)
			return 0;
		
		// Use cached value if already calculated
		if(aiPn[n] > 0)
			return aiPn[n];
		
		long Pn = 0;
		for(long k = 1; k*k <= n; k++)
		{
			// A little bit of recursion
			long n1 = n - k * (3 * k - 1) / 2;
			long n2 = n - k * (3 * k + 1) / 2;
			
			long Pn1 = CalcPn(ref aiPn, n1);
			long Pn2 = CalcPn(ref aiPn, n2);
			
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
	public static long CalcPnTrunc(ref long[] aiPn, long n)
	{
		//I added this:
		if (aiPn[0] != 1)
		{
			throw new ApplicationException("Array failure");
		}
		
		//long TRUNC_TO = 1000000000000000000;
		long TRUNC_TO = 10000000000;
		
		// P(<0) = 0 by convention
		if(n < 0)
			return 0;
		
		// Use cached value if already calculated
		if(aiPn[n] > 0)
			return aiPn[n];
		
		long Pn = 0;
		for(long k = 1; k*k <= n; k++)
		{
			// A little bit of recursion
			long n1 = n - k * (3 * k - 1) / 2;
			long n2 = n - k * (3 * k + 1) / 2;
			
			long Pn1 = CalcPnTrunc(ref aiPn, n1);
			long Pn2 = CalcPnTrunc(ref aiPn, n2);
			
			// elements are alternately added and subtracted
			if(k % 2 == 1)
				Pn = Pn + Pn1 + Pn2;
			else
				Pn = Pn - Pn1 - Pn2;
		}
		
		//Truncate
		Pn %= TRUNC_TO;
		
		// Cache calculated valued
		aiPn[n] = Pn;
		return Pn;
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
	[Obsolete("Not tested", true)]
	public void MultiplyByPow(int baseNum, long exponent)
	{
		for (int i = 0; i < exponent; ++i)
		{
			this.Multiply(baseNum);
		}
	}
	public long Length()
	{
//		//Never allowed to have zero
//		long filled = (array.Count-2) * TRUNC_LENGTH;
//		if (array.Count == 1) filled = 0;
//		if (array.Count == 2) filled = TRUNC_LENGTH;
//		
//		int tmp = array[array.Count-1];
//		while (tmp > 0)
//		{
//			tmp /= 10;
//			++filled;
//		}
//		return filled;
		long filled = 0;
		int i = array.Count-1;
		for (; i >= 0; --i)
		{
			//Find first non-zero
			if (array[i] > 0)
			{
				//Count digits in this number
				int tmp = array[i];
				while (tmp > 0)
				{
					tmp /= 10;
					++filled;
				}
				break;
			}
		}
		//Calculate the length of the rest of the numbers
		if (i > 0)
		{
			filled += i * TRUNC_LENGTH;
		}
		return filled;
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
	Debug.Assert(iint.Length().Dump() == "994014980014994001".Length, "LENGTH FAIL");
	iint.Divide(999);
	Debug.Assert(iint.ToString().Dump() == "995009990004999", "DIVIDE FAIL");
	Debug.Assert(iint.Length().Dump() == "995009990004999".Length, "LENGTH FAIL");
	iint.Divide(9);
	Debug.Assert(iint.ToString().Dump() == "110556665556111", "DIVIDE2 FAIL");
	Debug.Assert(iint.Length().Dump() == "110556665556111".Length, "LENGTH FAIL");
	iint.Divide(999);
	iint.Divide(999);
	Debug.Assert(iint.ToString().Dump() == "110778111", "DIVIDE3 FAIL");
	Debug.Assert(iint.Length().Dump() == "110778111".Length, "LENGTH FAIL");
//	iint.Divide(2);
//	Debug.Assert(iint.ToString().Dump() == "55389055.5", "DIVIDE3 FAIL");
}
#endregion InfiniteInt