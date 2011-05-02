namespace BclExtensionMethods.Pagination
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Dynamic;

	public static class SortFieldExtensions
	{
		public static string GetSortString(this IEnumerable<SortField> sortFields)
		{
			return sortFields
				.Select(f => f.ToSortString())
				.StringJoin(", ");
		}

		public static IQueryable<TEntity> SortBySortField<TEntity>(this IEnumerable<TEntity> source, params SortField[] sortFields)
		{
			return source.AsQueryable().SortBySortField(sortFields);
		}

		public static IQueryable<TEntity> SortBySortField<TEntity>(this IQueryable<TEntity> source, params SortField[] sortFields)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (sortFields.None())
			{
				return source;
			}

			var sortString = sortFields.GetSortString();
			try
			{
				return source.OrderBy(sortString);
			}
			catch (ParseException parseException)
			{
				throw new InvalidSortException(parseException);
			}
		}
	}
}