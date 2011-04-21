namespace Financial.Trades
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	[Serializable]
	public struct ContractMonth
	{
		/// <summary>
		/// 	A mapping from numeric month to month code.
		/// </summary>
		public static readonly Dictionary<int, string> Months = new Dictionary<int, string>();

		public static bool IsValidMonthCode(string code)
		{
			return Months.ContainsValue(code);
		}

		public readonly string Code;
		public readonly int Month;
		public readonly int Year;

		static ContractMonth()
		{
			Months.Add(1, "F");
			Months.Add(2, "G");
			Months.Add(3, "H");
			Months.Add(4, "J");
			Months.Add(5, "K");
			Months.Add(6, "M");
			Months.Add(7, "N");
			Months.Add(8, "Q");
			Months.Add(9, "U");
			Months.Add(10, "V");
			Months.Add(11, "X");
			Months.Add(12, "Z");
		}

		public ContractMonth(string code, int year)
		{
			code = code.ToUpper();
			Code = code;
			Month = Months.FirstOrDefault(p => p.Value == code).Key;
			Year = year;
		}

		///<summary>
		///	Creates a contract month from a valid month code (F,G,H,J,K,M,N,Q,U,V,X,Z)
		///	Following the Single Choice principle, no other class should contain this list and/or logic to map to/from this type.
		///</summary>
		///<exception cref = "ArgumentException"></exception>
		public static ContractMonth FromCode(string code, int year)
		{
			code = code.ToUpper();
			if (!Months.ContainsValue(code))
			{
				throw new ArgumentException("Invalid month code", "code");
			}
			return new ContractMonth(code, year);
		}

		///<exception cref = "ArgumentException"></exception>
		public static ContractMonth FromCode(char code, int year)
		{
			return FromCode(code.ToString(), year);
		}

		///<summary>
		///	This creates a contract month from a month (numbers 1 - 12).
		///</summary>
		///<exception cref = "ArgumentException"></exception>
		public static ContractMonth FromMonth(int month, int year)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentException("Invalid month", "month");
			}
			return new ContractMonth(Months[month], year);
		}

		/// <summary>
		/// 	Returns the month code or a null string
		/// </summary>
		/// <param name = "month"></param>
		/// <returns></returns>
		public static string GetCode(int month)
		{
			if (month < 1 || month > 12)
			{
				return null;
			}
			return Months[month];
		}

		public static Regex ProductContractRegex = new Regex(@"([A-Za-z]*)([FGHJKMNQUVXZfghjkmnquvxz])([0-9]{1,4})");

		public static Regex MonthYearRegex = new Regex(@"([FGHJKMNQUVXZfghjkmnquvxz])([0-9]{1,4})");

		/// <summary>
		/// Parses a contract month
		/// </summary>
		/// <param name="input">H0 or H2010</param>
		/// <param name="relativeYear">Defaults to use DateTime.Today.Year</param>
		public static ContractMonth? FromRelativeInput(string input, int? relativeYear = null)
		{
			var match = MonthYearRegex.Match(input);
			if (!match.Success)
			{
				return null;
			}
			var monthSymbol = match.Groups[1].Value;
			var year = match.Groups[2].Value;
			return FromRelativeInput(monthSymbol, year, relativeYear);
		}

		/// <summary>
		/// Parses a contract month from month and year strings, expected H and 0 or H and 2010 for example.
		/// </summary>
		/// <param name="year">0 or 2010</param>
		/// <param name="relativeYear">Defaults to use DateTime.Today.Year</param>
		/// <param name="monthSymbol">H or X etc</param>
		public static ContractMonth? FromRelativeInput(string monthSymbol, string year, int? relativeYear = null)
		{
			var referenceYear = relativeYear ?? DateTime.Today.Year;
			var contractYear = GetYearFromRelativeText(year, referenceYear);
			if (!contractYear.HasValue)
			{
				return null;
			}

			if (!IsValidMonthCode(monthSymbol))
			{
				return null;
			}

			return FromCode(monthSymbol, contractYear.Value);
		}

		public static int? GetYearFromRelativeText(string text, int referenceYear)
		{
			int yearInput;
			if (text.Length == 3 || !int.TryParse(text, out yearInput))
			{
				return null;
			}

			if (text.Length >= 3)
			{
				return yearInput;
			}

			var referenceIndex = (yearInput > 10) ? 100 : 10;
			var referenceOffset = referenceYear % referenceIndex;
			var referenceBase = referenceYear - referenceOffset;

			if (yearInput >= referenceOffset)
			{
				return referenceBase + yearInput;
			}

			return referenceBase + referenceIndex + yearInput;
		}
	}
}