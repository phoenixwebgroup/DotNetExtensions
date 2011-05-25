namespace BclExtensionMethodTests
{
	using System.Collections.Generic;
	using System.Linq;
	using BclExtensionMethods;
	using NUnit.Framework;

	public class PartitionExtensionTests : AssertionHelper
	{
		[Test]
		public void Partition_OneOverBoundary_ReturnsTwoPartitionsWithSecondPartial()
		{
			var list = Enumerable.Range(1, 4);

			var partitions = list.Partition(3);

			Expect(partitions.Count(), Is.EqualTo(2));
			var secondPartition = partitions.Last();
			Expect(secondPartition, Is.EquivalentTo(new[] {4}));
		}

		[Test]
		public void Partition_UnderOneBoundary_ReturnsPartialPartition()
		{
			var list = Enumerable.Range(1, 2);

			var partitions = list.Partition(3);

			var partialPartition = partitions.Single();
			Expect(partialPartition, Is.EquivalentTo(new[] {1, 2}));
		}

		[Test]
		public void Partition_AtBoundary_ReturnsFullPartition()
		{
			var list = Enumerable.Range(1, 3);

			var partitions = list.Partition(3);

			var fullPartition = partitions.Single();
			Expect(fullPartition, Is.EquivalentTo(new[] {1, 2, 3}));
		}

		[Test]
		public void Partition_None_ReturnsNoPartitions()
		{
			var list = Enumerable.Empty<int>();

			var partitions = list.Partition(1);

			Expect(partitions.Count(), Is.EqualTo(0));
		}

		[Test]
		public void Partition_InfiniteStream_AllowsPartialReadOfPartitions()
		{
			var positiveIntegersPartitionedBy10 = Numbers.PositiveIntegers().Partition(10);

			var first10Numbers = positiveIntegersPartitionedBy10.First();

			Expect(first10Numbers, Is.EquivalentTo(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}));
		}

		private class Numbers
		{
			public static IEnumerable<int> PositiveIntegers()
			{
				var current = 1;
				while (true)
				{
					yield return current;
					current++;
				}
			}
		}
	}
}