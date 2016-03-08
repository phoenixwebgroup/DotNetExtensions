namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class MedianExtensions
	{
		public static decimal Median(this IEnumerable<decimal> list, MedianOptions? options = MedianOptions.Default)
		{
			var sorted = list
				.OrderBy(numbers => numbers)
				.ToList();
			if (options.In(MedianOptions.IgnoreZeroes, MedianOptions.IgnoreZeroesAndNulls))
			{
				sorted.RemoveAll(x => x == 0);
			}
			var listSize = sorted.Count;

			if (listSize%2 != 0)
			{
				return sorted.ElementAt(listSize/2);
			}
			var midIndex = listSize/2;
			return ((sorted.ElementAt(midIndex - 1) +
			         sorted.ElementAt(midIndex))/2);
		}

		public static decimal Median<T>(this IEnumerable<T> list, Func<T, decimal> function, MedianOptions? options = MedianOptions.Default)
		{
			return list.Select(function.Invoke).Median(options);
		}

		public static decimal? Median(this IEnumerable<decimal?> list, MedianOptions? options = MedianOptions.Default)
		{
			var sorted = list
				.OrderBy(numbers => numbers)
				.ToList();
			if (options.In(MedianOptions.IgnoreZeroes, MedianOptions.IgnoreZeroesAndNulls))
			{
				sorted.RemoveAll(x => x == 0);
			}
			if (options.In(MedianOptions.IgnoreNulls, MedianOptions.IgnoreZeroesAndNulls))
			{
				sorted.RemoveAll(x => x == null);
			}
			var listSize = sorted.Count;

			if (listSize%2 != 0)
			{
				return sorted.ElementAt(listSize/2);
			}
			var midIndex = listSize/2;
			return ((sorted.ElementAt(midIndex - 1) +
			         sorted.ElementAt(midIndex))/2);
		}

		public static decimal? Median<T>(this IEnumerable<T> list, Func<T, decimal?> function, MedianOptions? options = MedianOptions.Default)
		{
			return list.Select(function.Invoke).Median(options);
		}

		public enum MedianOptions
		{
			Default,
			IgnoreZeroes,
			IgnoreNulls,
			IgnoreZeroesAndNulls
		}
	}
}