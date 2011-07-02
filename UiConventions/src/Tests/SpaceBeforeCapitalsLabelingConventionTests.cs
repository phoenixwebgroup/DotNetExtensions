namespace Tests
{
	using HtmlTags.UI.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class SpaceBeforeCapitalsLabelingConventionTests : AssertionHelper
	{
		[Test]
		[TestCase("TheDog", "The Dog")]
		[TestCase("CIAPuppies", "CIA Puppies")]
		[TestCase("CIAP", "CIAP")]
		[TestCase("theDog", "the Dog")]
		[TestCase("theCIA", "the CIA")]
		[TestCase("theCIAPuppies", "the CIA Puppies")]
		public void Test(string input, string expectedOutput)
		{
			var convention = new SpaceBeforeCapitalsLabelingConvention();

			var output = convention.GetLabelText(input);

			Expect(output, Is.EqualTo(expectedOutput));
		}
	}
}