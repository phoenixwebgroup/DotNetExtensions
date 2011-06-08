namespace BclExtensionMethods
{
	using System;

	public static class TimeSpanExtensions
	{
		public static string ToShortTimeString(this TimeSpan timeOf)
		{
			return new DateTime().Add(timeOf).ToShortTimeString();
		}
	}
}