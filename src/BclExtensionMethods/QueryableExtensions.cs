namespace BclExtensionMethods
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	public static class QueryableExtensions
	{
		/// <summary>
		/// 	Null safe empty check, deferred query
		/// </summary>
		public static bool IsEmpty<TSource>(this IQueryable<TSource> source)
		{
			return source == null
			       || !source.Any();
		}

		///<summary>
		///	Returns true if no items match the given predicate, null safe, deferred query
		///</summary>
		public static bool None<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			return source == null
			       || !source.Any(predicate);
		}
	}
}