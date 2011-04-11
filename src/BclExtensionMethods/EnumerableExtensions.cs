namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;

	public static class EnumerableExtensions
	{
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var item in source)
			{
				action(item);
			}
		}
	}
}