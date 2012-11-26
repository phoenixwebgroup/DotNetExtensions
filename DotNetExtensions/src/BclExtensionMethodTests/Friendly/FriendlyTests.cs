namespace BclExtensionMethodTests.Friendly
{
	using System;
	using BclExtensionMethods.Friendly;
	using NUnit.Framework;

	[TestFixture]
	public class FriendlyTests : AssertionHelper
	{
		[Test]
		public void TimeSince_FiveMinutesAgoInLocalTime_ReturnsFiveMinutesAgo()
		{
			var fiveMinutesAgoLocalTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));

			var friendly = fiveMinutesAgoLocalTime.TimeSince();

			Expect(friendly, Is.EqualTo("5 minutes ago"));
		}

		[Test]
		public void TimeSince_FiveMinutesAgoInUtc_ReturnsFiveMinutesAgo()
		{
			var fiveMinutesAgoUtc = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5));

			var friendly = fiveMinutesAgoUtc.TimeSince();

			Expect(friendly, Is.EqualTo("5 minutes ago"));
		}

		[Test]
		public void TimeSince_FiveMinutesAgoWithCurrentLocalTime_ReturnsFiveMinutesAgo()
		{
			var fiveMinutesAgoLocalTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));

			var currentLocalTime = DateTime.Now;
			var friendly = fiveMinutesAgoLocalTime.TimeSince(currentLocalTime);

			Expect(friendly, Is.EqualTo("5 minutes ago"));
		}

		[Test]
		public void TimeSince_FiveMinutesAgoWithCurrentUtcTime_ReturnsFiveMinutesAgo()
		{
			var fiveMinutesAgoLocalTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));

			var currentUtcTime = DateTime.UtcNow;
			var friendly = fiveMinutesAgoLocalTime.TimeSince(currentUtcTime);

			Expect(friendly, Is.EqualTo("5 minutes ago"));
		}

		[Test]
		public void TimeSince_FiveMinutesAgoUnspecifiedTime_ReturnsFiveMinutesAgo()
		{
			// note this isn't a test so much as an indication of expected behavior as .net treats unspecified as local time when converting to UTC
			var fiveMinutesAgoUnspecifiedTime = new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified).Subtract(TimeSpan.FromMinutes(5));

			var friendly = fiveMinutesAgoUnspecifiedTime.TimeSince();

			Expect(friendly, Is.EqualTo("5 minutes ago"));
		}
	}
}