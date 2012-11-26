namespace BclExtensionMethods.Friendly
{
	using System;

	public static class Friendly
	{
		// todo this could use some tests, also looks like someone else's code, we should attribute it

		/// <summary>
		/// 	Get the time since in human readable terms given time in the past.
		/// </summary>
		/// <param name="timeInThePast"> </param>
		/// <param name="currentTime"> Optionally override the time compared to to determine the elapsed time. </param>
		public static string TimeSince(this DateTime timeInThePast, DateTime? currentTime = null)
		{
			var utcNow = (currentTime ?? DateTime.UtcNow).ToUniversalTime();
			var utcTimeInThePast = timeInThePast.ToUniversalTime();
			var elapsedTime = utcNow.Subtract(utcTimeInThePast);
			return TimeSince(elapsedTime);
		}

		/// <summary>
		/// 	Get the time since in human readable terms given a pre calculated timespan from the current time.
		/// </summary>
		public static string TimeSince(this TimeSpan elapsedTime)
		{
			// 2.
			// Get total number of days elapsed.
			var dayDiff = (int) elapsedTime.TotalDays;

			// 3.
			// Get total number of seconds elapsed.
			var secDiff = (int) elapsedTime.TotalSeconds;

			// 4.
			// Don't allow out of range values.
			if (dayDiff < 0)
			{
				return null;
			}

			// 5.
			// Handle same-day times.
			if (dayDiff == 0)
			{
				// A.
				// Less than one minute ago.
				if (secDiff < 60)
				{
					return "just now";
				}
				// B.
				// Less than 2 minutes ago.
				if (secDiff < 120)
				{
					return "1 minute ago";
				}
				// C.
				// Less than one hour ago.
				if (secDiff < 3600)
				{
					return string.Format("{0} minutes ago",
					                     Math.Floor((double) secDiff/60));
				}
				// D.
				// Less than 2 hours ago.
				if (secDiff < 7200)
				{
					return "1 hour ago";
				}
				// E.
				// Less than one day ago.
				if (secDiff < 86400)
				{
					return string.Format("{0} hours ago",
					                     Math.Floor((double) secDiff/3600));
				}
			}
			// 6.
			// Handle previous days.
			if (dayDiff == 1)
			{
				return "yesterday";
			}
			if (dayDiff < 7)
			{
				return string.Format("{0} days ago",
				                     dayDiff);
			}
			return string.Format("{0} weeks ago", Math.Ceiling((double) dayDiff/7));
			// todo in the future it would be nice to show months/years, but this would probably require the relative dates or suffer from error in # days in month / year/leap years
		}
	}
}