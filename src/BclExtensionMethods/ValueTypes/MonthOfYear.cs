namespace BclExtensionMethods.ValueTypes
{
	using System;

	public struct MonthOfYear
	{
		public readonly int Month;
		public readonly int Year;

		public MonthOfYear(int month, int year)
		{
			Month = month;
			Year = year;
		}

		public static explicit operator DateTime(MonthOfYear monthOfYear)
		{
			return new DateTime(monthOfYear.Year, monthOfYear.Month, 1);
		}

		/// <summary>
		/// Creates a MonthOfYear 
		/// </summary>
		/// <param name="monthOfYear">text in the form of yyyy-MM</param>
		public static MonthOfYear MonthOfYearFrom(string monthOfYear)
		{
			var parts = monthOfYear.Split('-');
			var year = Convert.ToInt32(parts[0]);
			var month = Convert.ToInt32(parts[1]);
			return new MonthOfYear(month, year);
		}

		public string ToStringYearDashMonth()
		{
			return ToStringDateTime("yyyy-MM");
		}

		public string ToStringDateTime(string format, IFormatProvider formatProvider)
		{
			return ((DateTime)this).ToString(format, formatProvider);
		}

		public static MonthOfYear Current
		{
			get { return new MonthOfYear(DateTime.Now.Month, DateTime.Now.Year); }
		}

		public MonthOfYear AddMonths(int months)
		{
			return (MonthOfYear)((DateTime)this).AddMonths(months);
		}

		public MonthOfYear AddYears(int years)
		{
			return (MonthOfYear)((DateTime)this).AddYears(years);
		}

		public static explicit operator MonthOfYear(DateTime dateTime)
		{
			return new MonthOfYear(dateTime.Month, dateTime.Year);
		}

		public string ToStringDateTime(string format)
		{
			return ((DateTime)this).ToString(format);
		}
	}
}