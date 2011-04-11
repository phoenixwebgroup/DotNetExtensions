namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableExtensions
	{
		/// <summary>
		/// 	Execute the action on every item, null safe
		/// </summary>
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var item in source)
			{
				action(item);
			}
		}

		/// <summary>
		/// 	Null safe empty check
		/// </summary>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source == null
			       || !source.Any();
		}

		///<summary>
		///	Returns true if no items match the given predicate, null safe
		///</summary>
		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source == null
			       || !source.Any(predicate);
		}

		/// <summary>
		/// 	Take until the end condition is true.
		/// </summary>
		public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Predicate<T> endCondition)
		{
			return source.TakeWhile(item => !endCondition(item));
		}

		/// <summary>
		/// 	Skip until the end condition is true.
		/// </summary>
		public static IEnumerable<T> SkipUntil<T>(this IEnumerable<T> source, Predicate<T> endCondition)
		{
			return source.SkipWhile(item => !endCondition(item));
		}
	}
}