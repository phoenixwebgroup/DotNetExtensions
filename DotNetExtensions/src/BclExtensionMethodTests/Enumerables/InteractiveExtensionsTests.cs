namespace BclExtensionMethodTests.Enumerables
{
	using System.Collections.Generic;
	using System.Linq;
	using BclExtensionMethods.Enumerables;
	using NUnit.Framework;

	[TestFixture]
	public class InteractiveExtensionsTests : AssertionHelper
	{
		[Test]
		public void Run_DoToCopyItemsToAnotherList_CopiesItems()
		{
			var items = new[] {1, 2};
			var copiedItems = new List<int>();

			items
				.Do(copiedItems.Add)
				.Run();

			Expect(copiedItems.SequenceEqual(items));
		}
	}
}