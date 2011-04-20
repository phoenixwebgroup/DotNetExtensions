namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class WeightedAverageExtensions
	{
		public static decimal? WeightedAverage<TSource>(this IEnumerable<TSource> source,
		                                                Func<TSource, decimal?> valueSelector,
		                                                Func<TSource, decimal?> weightSelector)
		{
			var weightTotal = source.Select(weightSelector).Sum();

			var weightedSum = source.Sum(item => (valueSelector(item) ?? 0)*(weightSelector(item) ?? 0));

			return weightTotal != 0 ? weightedSum/weightTotal : null;
		}

		/// <summary>
		/// 	Performs a weighted average on every pair of values and weights in which both have values.
		/// </summary>
		/// <typeparam name = "TSource">The object type which holds the value.</typeparam>
		/// <param name = "source">The IEnumerable holding the objects.</param>
		/// <param name = "valueSelector">The function which extracts the values.</param>
		/// <param name = "weightSelector">The function which extracts the weights.</param>
		/// <returns>The weighted average of every pair of values and weights in which both have values
		/// 	or null if there are no pairs or the total weight is zero.</returns>
		public static decimal? WeightedAverageValues<TSource>(this IEnumerable<TSource> source,
		                                                      Func<TSource, decimal?> valueSelector,
		                                                      Func<TSource, decimal?> weightSelector)
		{
			decimal? valueTotal = null;
			decimal? weightTotal = null;
			if (source != null)
			{
				foreach (var item in source)
				{
					var value = valueSelector(item);
					var weight = weightSelector(item);

					if (value.HasValue && weight.HasValue)
					{
						valueTotal = (valueTotal ?? 0m) + value.Value*weight.Value;
						weightTotal = (weightTotal ?? 0m) + weight.Value;
					}
				}
			}

			return ((valueTotal.HasValue && weightTotal.HasValue && weightTotal.Value != 0m)
			        	? (valueTotal.Value/weightTotal.Value)
			        	: (decimal?) null);
		}
	}
}