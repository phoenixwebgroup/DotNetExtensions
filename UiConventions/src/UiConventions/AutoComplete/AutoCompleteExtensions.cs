namespace HtmlTags.UI.AutoComplete
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using BclExtensionMethods;

	public static class AutoCompleteExtensions
	{
		/// <summary>
		/// 	Build an autocomplete result
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "items"></param>
		/// <param name = "textSelector"></param>
		/// <param name = "valueSelector"></param>
		/// <returns></returns>
		public static ContentResult ToAutoCompleteResult<T>(this IEnumerable<T> items, Func<T, string> textSelector,
		                                                    Func<T, object> valueSelector)
		{
			var pairs = items.Select(i => string.Format("{0}|{1}", textSelector(i), valueSelector(i))).Distinct().ToArray();
			var content = string.Join(Environment.NewLine, pairs);

			return new ContentResult
			       	{
			       		Content = content
			       	};
		}


		/// <summary>
		/// 	Build an autocomplete result, applying criteria to filter results
		/// 	Note: this will not defer to IQueryable filters
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "items"></param>
		/// <param name = "textSelector"></param>
		/// <param name = "valueSelector"></param>
		/// <param name = "criteria"></param>
		/// <returns></returns>
		public static ContentResult ToAutoCompleteResult<T>(this IEnumerable<T> items, Func<T, string> textSelector,
		                                                    Func<T, object> valueSelector, AutoCompleteCriteria criteria)
		{
			if (!string.IsNullOrEmpty(criteria.Filter))
			{
				items = items.Where(c => textSelector(c).Has(criteria.Filter, StringComparison.InvariantCultureIgnoreCase));
			}
			return items
				.Take(criteria.Limit)
				.ToAutoCompleteResult(textSelector, valueSelector);
		}
	}
}