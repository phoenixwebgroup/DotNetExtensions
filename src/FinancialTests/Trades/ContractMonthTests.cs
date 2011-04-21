namespace FinancialTests.Trades
{
	using Financial.Trades;
	using NUnit.Framework;

	[TestFixture]
	public class ContractMonthTests : AssertionHelper
	{
		[Test]
		public void FromRelativeInput_ValidInput_ReturnsContractMonth()
		{
			var validInput = "H0";
			var relativeYear = 2010;
			var expectMonth = ContractMonth.FromCode("H", 2010);

			var contractMonth = ContractMonth.FromRelativeInput(validInput, relativeYear);

			Expect(contractMonth, Is.EqualTo(expectMonth));
		}

		[Test]
		public void FromRelativeInput_InValidInput_ReturnsNothing()
		{
			var invalidInput = "A";

			var contractMonth = ContractMonth.FromRelativeInput(invalidInput);

			Expect(contractMonth, Is.Null);
		}
	}
}