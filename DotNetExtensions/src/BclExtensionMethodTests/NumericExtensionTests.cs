namespace BclExtensionMethodTests
{
	using System.Collections.Generic;
	using System.Configuration;
	using BclExtensionMethods;
	using NUnit.Framework;

	public class NumericExtensionTests : AssertionHelper
	{
		[Test]
		public void ShiftDecimalBy_TenShiftedOnePlace_IsOneHundred()
		{
			var ten = 10m;
			var onePlace = 1;

			var shifted = ten.ShiftDecimalBy(onePlace);

			Expect(shifted, Is.EqualTo(100m));
		}

		[Test]
		public void ShiftDecimalBy_TenShiftedByNegativeOnePlace_IsOne()
		{
			var ten = 10m;
			var negativeOnePlace = -1;

			var shifted = ten.ShiftDecimalBy(negativeOnePlace);

			Expect(shifted, Is.EqualTo(1m));
		}

		[Test]
		[TestCase(null)]
		[TestCase(MedianExtensions.MedianOptions.Default)]
		[TestCase(MedianExtensions.MedianOptions.IgnoreNulls)]
		[TestCase(MedianExtensions.MedianOptions.IgnoreZeroes)]
		[TestCase(MedianExtensions.MedianOptions.IgnoreZeroesAndNulls)]
		public void Median_AllOptions(MedianExtensions.MedianOptions? option)
		{
		
			var nullableNumbers = new List<decimal?> {null, 1m, 5m, null, 4m, 0m};
			var nullableMedian = nullableNumbers.Median(option);
			switch (option)
			{
				case MedianExtensions.MedianOptions.IgnoreNulls:
					Expect(nullableMedian, Is.EqualTo(2.5m));
					break;
				case MedianExtensions.MedianOptions.IgnoreZeroes:
					Expect(nullableMedian, Is.EqualTo(1m));
					break;
				case MedianExtensions.MedianOptions.IgnoreZeroesAndNulls:
					Expect(nullableMedian, Is.EqualTo(4m));
					break;
				default:
					Expect(nullableMedian, Is.EqualTo(0.5m));
					break;
			}
			var numbers = new[] {0m, 1m, 5m, 0m, 4m};
			var normalMedian = numbers.Median(option);
			switch (option)
			{
				case MedianExtensions.MedianOptions.IgnoreZeroes:
					Expect(normalMedian, Is.EqualTo(4m));
					break;
				case MedianExtensions.MedianOptions.IgnoreZeroesAndNulls:
					Expect(normalMedian, Is.EqualTo(4m));
					break;
				default:
					Expect(normalMedian, Is.EqualTo(1m));
					break;
			}
		}
	}
}