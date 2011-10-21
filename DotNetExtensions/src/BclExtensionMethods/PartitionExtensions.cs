namespace BclExtensionMethods
{
	using System;
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

		/// <summary>
		/// Partition a list of items and then perform a select on each item of each parition, then aggregate the results back into one enumeration.
		/// This is helpful when hitting a database by a list of ids and avoiding maxing out parameter limitations
		/// This is a deferred enumerable, keep in mind that it is not enumerated at the completion of this method.
		/// </summary>
		public static IEnumerable<T> PartitionSelect<K, T>(this IEnumerable<K> keys, Func<IEnumerable<K>, IEnumerable<T>> selector, int size = 1000)
		{
			return keys
				.ToArray()
				.Distinct()
				.Partition(size)
				.Select(p => (IEnumerable<K>)p.ToArray())
				.SelectMany(selector);
		}
	}
}