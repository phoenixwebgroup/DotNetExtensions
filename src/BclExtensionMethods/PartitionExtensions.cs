namespace BclExtensionMethods
{
	using System.Collections.Generic;
	using System.Linq;

	public static class PartitionExtensions
	{
		/// <summary>
		/// Partition the items
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="countPerPartition"></param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int countPerPartition)
		{
			var partition = new T[countPerPartition];
			var paritionCount = 0;
			foreach (var item in source)
			{
				if (paritionCount == countPerPartition)
				{
					paritionCount = 0;
					yield return partition;
					partition = new T[countPerPartition];
				}
				partition[paritionCount] = item;
				paritionCount++;
			}
			if (paritionCount > 0)
			{
				yield return partition.Take(paritionCount);
			}
		}
	}
}