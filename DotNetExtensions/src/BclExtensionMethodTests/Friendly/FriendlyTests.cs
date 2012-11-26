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

		[Test]
		public void TimeSince_WithinLastMinute_ReturnsJustNow()
		{
			Expect(TimeSpan.Parse("00:00:00").TimeSince(), Is.EqualTo("just now"));
			Expect(TimeSpan.Parse("00:00:59").TimeSince(), Is.EqualTo("just now"));
		}

		[Test]
		public void TimeSince_BetweenOneAndTwoMinutes_ReturnsOneMinuteAgo()
		{
			Expect(TimeSpan.Parse("00:01:00").TimeSince(), Is.EqualTo("1 minute ago"));
			Expect(TimeSpan.Parse("00:01:59").TimeSince(), Is.EqualTo("1 minute ago"));
		}

		[Test]
		public void TimeSince_BetweenTwoMinutesAndOneHour_ReturnsMinutesRoundedDown()
		{
			Expect(TimeSpan.Parse("00:02:00").TimeSince(), Is.EqualTo("2 minutes ago"));
			Expect(TimeSpan.Parse("00:59:59").TimeSince(), Is.EqualTo("59 minutes ago"));
		}

		[Test]
		public void TimeSince_BetweenOneHourAndTwoHours_ReturnsOneHourAgo()
		{
			Expect(TimeSpan.Parse("01:00:00").TimeSince(), Is.EqualTo("1 hour ago"));
			Expect(TimeSpan.Parse("01:59:59").TimeSince(), Is.EqualTo("1 hour ago"));
		}

		[Test]
		public void TimeSince_BetweenTwoHoursAndOneDay_ReturnsHours()
		{
			Expect(TimeSpan.Parse("02:00:00").TimeSince(), Is.EqualTo("2 hours ago"));
			Expect(TimeSpan.Parse("23:59:59").TimeSince(), Is.EqualTo("23 hours ago"));
		}

		[Test]
		public void TimeSince_BetweenOneDayAndTwoDays_ReturnsYesterday()
		{
			Expect(TimeSpan.FromDays(1).TimeSince(), Is.EqualTo("yesterday"));
			Expect(TimeSpan.FromDays(2).Subtract(TimeSpan.FromSeconds(1)).TimeSince(), Is.EqualTo("yesterday"));
		}

		[Test]
		public void TimeSince_BetweenTwoDaysAndOneWeek_ReturnsDays()
		{
			Expect(TimeSpan.FromDays(2).TimeSince(), Is.EqualTo("2 days ago"));
			Expect(TimeSpan.FromDays(7).Subtract(TimeSpan.FromSeconds(1)).TimeSince(), Is.EqualTo("6 days ago"));
		}

		[Test]
		public void TimeSince_GreaterThanOneWeekButLessThan31Days_ReturnsWeeks()
		{
			Expect(TimeSpan.FromDays(7).TimeSince(), Is.EqualTo("1 weeks ago"));
			Expect(TimeSpan.FromDays(14).TimeSince(), Is.EqualTo("2 weeks ago"));
			Expect(TimeSpan.FromDays(21).TimeSince(), Is.EqualTo("3 weeks ago"));
			Expect(TimeSpan.FromDays(28).TimeSince(), Is.EqualTo("4 weeks ago"));
			Expect(TimeSpan.FromDays(30).TimeSince(), Is.EqualTo("5 weeks ago"));
		}

		[Test]
		public void TimeSince_GreaterThanOneMonth_ReturnsNull()
		{
			Expect(TimeSpan.FromDays(32).TimeSince(), Is.Null);
		}
	}
}