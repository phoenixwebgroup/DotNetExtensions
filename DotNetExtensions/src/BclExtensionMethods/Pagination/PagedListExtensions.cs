namespace BclExtensionMethods.Pagination
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PagedList;

	public static class PagedListExtensions
	{
		/// <summary>
		/// 	Map the items in a paged list
		/// </summary>
		public static IPagedList<TResult> Select<TSource, TResult>(this IPagedList<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new StaticPagedList<TResult>(source.AsEnumerable().Select(selector), source.PageIndex, source.PageSize, source.TotalItemCount);
		}

		public static IPagedList<TSource> SortToPagedList<TSource>(this IEnumerable<TSource> source, PagingCriteria pagingCriteria)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.AsQueryable().SortToPagedList(pagingCriteria);
		}

		public static IPagedList<TSource> SortToPagedList<TSource>(this IQueryable<TSource> source, PagingCriteria pagingCriteria)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (pagingCriteria == null)
			{
				throw new ArgumentNullException("pagingCriteria");
			}
			var sortedSource = source.SortBySortField(pagingCriteria.SortFields.ToArray());
			if (pagingCriteria.IsNotPaged)
			{
				var count = sortedSource.Count();
				var pageSize = count == 0 ? 1 : count;
				return sortedSource.ToPagedList(0, pageSize);
			}
			return sortedSource.ToPagedList(pagingCriteria.PageIndex, pagingCriteria.PageSize);
		}
	}
}