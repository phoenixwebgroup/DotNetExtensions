namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;

	public class Days
	{
		public static DateTime Tomorrow
		{
			get { return DateTime.Today.AddDays(1); }
		}

		public static DateTime Yesterday
		{
			get { return DateTime.Today.AddDays(-1); }
		}

		/// <summary>
		/// 	Returns an infinite day stream backwards (inclusive) from the startDate
		/// </summary>
		public static IEnumerable<DateTime> BackwardsFrom(DateTime startDate)
		{
			while (true)
			{
				yield return startDate;
				startDate = startDate.AddDays(-1);
			}
		}

		/// <summary>
		/// 	Returns an infinite day stream forwards (inclusive) from the startDate
		/// </summary>
		public static IEnumerable<DateTime> ForwardsFrom(DateTime startDate)
		{
			while (true)
			{
				yield return startDate;
				startDate = startDate.AddDays(1);
			}
		}
	}
}