namespace BclExtensionMethodTests
{
	using System;
	using System.Linq;
	using BclExtensionMethods.ValueTypes;
	using NUnit.Framework;

	[TestFixture]
	public class DateTests : AssertionHelper
	{
		[Test]
		public void Equality()
		{
			var firstDate = new Date(2011, 1, 1);
			var sameAsFirstDate = new Date(2011, 1, 1);
			var laterDate = new Date(2011, 1, 2);

			Expect(firstDate == sameAsFirstDate);
			Expect(firstDate == laterDate, Is.False);
			Expect(firstDate != sameAsFirstDate, Is.False);
			Expect(firstDate != laterDate);
		}

		[Test]
		public void Comparison()
		{
			var firstDate = new Date(2011, 1, 1);
			var sameAsFirstDate = new Date(2011, 1, 1);
			var laterDate = new Date(2011, 1, 2);

			Expect(firstDate < laterDate);
			Expect(laterDate < firstDate, Is.False);

			Expect(firstDate > laterDate, Is.False);
			Expect(laterDate > firstDate);

			Expect(firstDate <= laterDate);
			Expect(laterDate <= firstDate, Is.False);

			Expect(firstDate >= laterDate, Is.False);
			Expect(laterDate >= firstDate);

			Expect(firstDate <= sameAsFirstDate);
			Expect(firstDate >= sameAsFirstDate);
			Expect(firstDate < sameAsFirstDate, Is.False);
			Expect(firstDate > sameAsFirstDate, Is.False);

			Expect(firstDate, Is.EqualTo(sameAsFirstDate));
			Expect(firstDate.Equals(sameAsFirstDate));
		}

		[Test]
		public void Equals_FromCastedDateWithTime_DoesNotCompareTime()
		{
			var dayWithTime = (Date) new DateTime(2011, 1, 1, 1, 0, 0);
			var sameDayWithOutTime = (Date) new DateTime(2011, 1, 1);

			Expect(dayWithTime, Is.EqualTo(sameDayWithOutTime));
		}

		[Test]
		public void Equals_ConstructorDateWithTime_DoesNotCompareTime()
		{
			var dayWithTime = new Date(new DateTime(2011, 1, 1, 1, 0, 0));
			var sameDayWithOutTime = new Date(new DateTime(2011, 1, 1));

			Expect(dayWithTime, Is.EqualTo(sameDayWithOutTime));
		}

		[Test]
		public void Equals_FromString_DoesNotCompareTime()
		{
			var dayWithTime = new Date("2011-1-1 1:00:00");
			var sameDayWithOutTime = new Date("2011-1-1");

			Expect(dayWithTime, Is.EqualTo(sameDayWithOutTime));
		}

		[Test]
		public void CompareTo()
		{
			var firstDate = new Date(2011, 1, 1);
			var sameAsFirstDate = new Date(2011, 1, 1);
			var laterDate = new Date(2011, 1, 2);

			Expect(firstDate.CompareTo(sameAsFirstDate), Is.EqualTo(0));
			Expect(firstDate.CompareTo(laterDate), Is.EqualTo(-1));
			Expect(laterDate.CompareTo(firstDate), Is.EqualTo(1));

			var min = new[] {laterDate, firstDate}.Min();
			Expect(min, Is.EqualTo(firstDate));
		}
	}
}