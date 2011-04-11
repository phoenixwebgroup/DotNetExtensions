namespace BclExtensionMethods
{
	using System;

	public class Dates
	{
		public static DateTime Tomorrow
		{
			get { return DateTime.Today.AddDays(1); }
		}

		public static DateTime Yesterday
		{
			get { return DateTime.Today.AddDays(-1); }
		}
	}
}