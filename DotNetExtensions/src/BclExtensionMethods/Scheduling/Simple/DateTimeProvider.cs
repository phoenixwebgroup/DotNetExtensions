namespace BclExtensionMethods.Scheduling.Simple
{
	using System;

	public static class DateTimeProvider
	{
		public static Func<DateTime> Now = () => DateTime.Now;
	}
}