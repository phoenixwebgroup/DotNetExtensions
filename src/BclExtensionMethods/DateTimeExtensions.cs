namespace BclExtensionMethods
{
	using System;

	public static class DateTimeExtensions
	{
		/// <summary>
		/// Most of this are modified from ideas on http://www.extensionmethod.net
		/// </summary>

		public static bool IsWeekend(this DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Sunday
			       || date.DayOfWeek == DayOfWeek.Saturday;
		}

		public static bool IsWeekday(this DateTime date)
		{
			return !IsWeekday(date);
		}

		public static DateTime FirstDayOfTheMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		/// <summary>
		/// 	Gets the next week day after the given date
		/// </summary>
		public static DateTime GetNextWeekDay(this DateTime date)
		{
			if (date.DayOfWeek == DayOfWeek.Friday)
			{
				return date.AddDays(3);
			}
			if (date.DayOfWeek == DayOfWeek.Saturday)
			{
				return date.AddDays(2);
			}
			return date.AddDays(1);
		}

		/// <summary>
		/// 	Gets the previous week day before the given date
		/// </summary>
		public static DateTime GetPreviousWeekDay(this DateTime date)
		{
			if (date.DayOfWeek == DayOfWeek.Monday)
			{
				return date.AddDays(-3);
			}
			if (date.DayOfWeek == DayOfWeek.Sunday)
			{
				return date.AddDays(-2);
			}
			return date.AddDays(-1);
		}
	}
}