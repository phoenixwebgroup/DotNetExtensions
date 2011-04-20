namespace BclExtensionMethodTests
{
	using System;
	using System.Linq;
	using BclExtensionMethods;
	using NUnit.Framework;

	[TestFixture]
	public class DaysTests : AssertionHelper
	{
		[Test]
		public void BackwardsFrom_TakeTwo_ReturnsTwoDates()
		{
			var startDate = new DateTime(2010, 1, 2);
			var previousDate = startDate.AddDays(-1);

			var twoDaysBackwards = Days.BackwardsFrom(startDate)
				.Take(2);

			Expect(twoDaysBackwards, Is.EquivalentTo(new[] { startDate, previousDate }));
		}
		
		[Test]
		public void ForwardsFrom_TakeTwo_ReturnsTwoDates()
		{
			var startDate = new DateTime(2010, 1, 2);
			var nextDate = startDate.AddDays(1);

			var twoDaysBackwards = Days.ForwardsFrom(startDate)
				.Take(2);

			Expect(twoDaysBackwards, Is.EquivalentTo(new[] { startDate, nextDate }));
		}
	}
}