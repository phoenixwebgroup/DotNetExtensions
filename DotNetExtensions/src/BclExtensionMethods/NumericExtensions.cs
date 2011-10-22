namespace BclExtensionMethods
{
	using System;

	public static class NumericExtensions
	{
		/// <summary>
		/// Decimal Shift Operation
		/// </summary>
		/// <param name="places">+ shifts decimal right, - shifts decimal left, this is done via Powers of 10</param>
		/// <example>10.ShiftDecimalBy(1) == 100</example>
		public static decimal ShiftDecimalBy(this decimal number, int places)
		{
			if (places == 0)
			{
				return number;
			}
			return number*(decimal) Math.Pow(10, places);
		}

		public static bool IsMultipleOf(this int number, int multiple)
		{
			return number % multiple == 0;
		}

		public static bool IsEven(this int value)
		{
			return value % 2 == 0;
		}

		public static bool IsOdd(this int value)
		{
			return value % 2 == 1;
		}
	}
}