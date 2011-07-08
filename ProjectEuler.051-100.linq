<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	Problem96().Dump("Result");
}

// Define other methods and classes here
public static long Problem96()
{
	//Speed: 0.5 on work computer
	//		0.6 with Problem96_sudokuCheck(grid, cell)
	
	//Thinking about the minimal storage for it
	//Minimal bits: 324 (4 * 81) (4bits required to store 0-9 in an array)
	// 11uints, 41bytes
	//Another way would be 2 ints for each row
	//  [64 total:](36: 4 4 4  4 4 4  4 4 4) (9: flags) (19: extra)
	//Or 987654321 would take up 30 bits... so 9ints
	//  Then use a byte and the top bit to store the needed/used digits
	//I think the best would be 9 ints, one for each row, then what is used/not somewhere else... 81bits for each row/column/grid (81*3), then could and those groups of 9
	//BUT: I'm not going to bother with any of this for this problem
	
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem96_sodoku17sOnForum.txt");
	string[] lines = File.ReadAllLines(PATH);
	
	int loadedLines = 0;
	StringBuilder gridBuilder = new StringBuilder();
	long outputSum = 0;
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
				
				//Now start solving
				if (!Problem96_recurse(grid, possibilities, 0))
				{
					"".Dump("FAILURE line: " + l);
				}
				outputSum += grid[0]*100 + grid[1]*10 + grid[2];
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
public static bool Problem96_recurse(int[] grid, BitArray possibles, int lastCellPlusOne)
{
	int jStart = lastCellPlusOne % 9;
	for (int i = lastCellPlusOne / 9; i < 9; ++i)
	{
		for (int j = jStart; j < 9; ++j)
		{
			int cell = 9 * i + j;
			//Find first cell that is zero
			if (grid[cell] == 0)
			{
				Util.Progress = cell *100/81;
				
				List<int> possibleValues = GetPossibles(possibles, cell);
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
					
//					string test = Problem96_OutputGrid(grid, null);
//					if (test.StartsWith("483921657"))
//					{
//						test.Dump();
//						Problem96_sudokuCheck(grid, cell).Dump("Check to cell: " + cell);
//					}
					
					if (Problem96_recurse(grid, possibles, cell+1))
					{
						//"WIN!!!!!!!!!!!!!!!!!".Dump();
						return true;
					}
					//Unmark cell and bit array
					grid[cell] = 0;
					MarkBitArray(possibles, cell, value, true);
					//Continue loop
				}
				
				//"Checking".Dump();
				return Problem96_sudokuCheck(grid);
			}
		}
		jStart = 0;
	}
	//Found no zero cells, so just check it
	return Problem96_sudokuCheck(grid);
	//throw new ApplicationException("Found no zero cells?: " + lastCellPlusOne);
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
public static bool Problem96_sudokuCheck(int[] grid, int uptoCell)
{
	//If under 9, can't check anything, so just say its alright
	if (uptoCell < 8) return true;
	
	for (int i = 0; i < 9; ++i)
	{
		int hLineCheck = SudokuTotal;
//		int vLineCheck = SudokuTotal;
//		int gridCheck = SudokuTotal;
//		
//		//hLine will simply be the if (cell > uptoCell) check
//		bool vLineValid = true;
//		bool gridValid = true;
		
		for (int j = 0; j < 9; ++j)
		{
			int cell = i*9+j;
			if (cell > uptoCell) return true;
			
			hLineCheck -= grid[9*i+j];
//			vLineCheck -= grid[9*j+i];
//			if (9*j+i > uptoCell) vLineValid = false;
//			gridCheck -= grid[9*((i/3)*3 + j/3) + ((i%3)*3 + j%3)];
//			if (9*((i/3)*3 + j/3) + ((i%3)*3 + j%3) > uptoCell) gridValid = false;
		}
		if (hLineCheck != 0) return false;
//		if (vLineCheck != 0 && vLineValid) return false;
//		if (gridCheck != 0 && gridValid) return false;
	}
	return true;
}
public static long Problem89()
{
	//Option 1:
	//  First write a parser which takes in all valid forms, converting them to decimal
	//  Then write a roman numeral converter
	//Option 2:
	//  Or find replacements (semi-manually) and code each to make the roman numerals minimal
	
	
	string PATH = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "ProjectEuler_Problem79_keylog.txt");
	
	string[] lines = File.ReadAllLines(PATH);
	
	
	
	return -89;
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