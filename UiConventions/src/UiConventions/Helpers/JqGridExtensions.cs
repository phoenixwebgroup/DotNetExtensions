namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using AutoComplete;
	using BclExtensionMethods.Reflection;
	using HtmlTags;

	public static class JqGridExtensions
	{
		// todo move this to javascript and obsolete it all, it might already be there... would be interesting to figure out.

		/// <summary>
		/// This generates javascript to use jquery forms pluging to serialize a filter form into a querystring for the jqgrid url
		/// </summary>
		public static string JqGridUrlFromFilterForm<T>(this HtmlHelper<T> helper, string url, string filterFormId)
			where T : class
		{
			const string template =
				@"function() {{ $(defaultGridName).setGridParam({{url: '{0}?' + $('#{1}').formSerialize()}}); }}";
			return string.Format(template, url, filterFormId);
		}

		public static HtmlTag JqGridTriggerReload<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
			where T : class
		{
			var current = ReflectionUtilities.GetMemberExpression(expression);
	
			var template =
				@"$('#{0}').change(function(){{$(defaultGridName).trigger('reloadGrid');}});";
			var propertyInfo = (current.Member as PropertyInfo);
			if (propertyInfo != null && propertyInfo.PropertyType.IsGenericType &&
			    propertyInfo.PropertyType == typeof (IList<int>))
			{
				template = @"$('input[name ={0}]').each(function(){{";
				template += @"$(this).change(function(){{";
				template += @"$(defaultGridName).trigger('reloadGrid');}});}});";
			}

			var memberName = current.Member.Name;

			if (propertyInfo != null && propertyInfo.PropertyType.BaseType == typeof (AutoCompleteInput))
			{
				memberName += "Value";
			}

			var script = string.Format(template, memberName);
			return Tags.Script(script);
		}
	}
}