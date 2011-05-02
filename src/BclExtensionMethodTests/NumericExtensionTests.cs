namespace BclExtensionMethodTests
{
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
	}
}