namespace BclExtensionMethodTests
{
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
		}
	}
}