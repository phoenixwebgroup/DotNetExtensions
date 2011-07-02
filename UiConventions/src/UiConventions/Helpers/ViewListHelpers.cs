namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using BclExtensionMethods.Reflection;
	using Constants;
	using FubuCore.Reflection;

	[Obsolete("Use Knockoutjs")]
	public static class ViewListHelpers
	{
		[Obsolete]
		public static HtmlTag ListIndex<T, V>(this T model, Expression<Func<T, IEnumerable<V>>> list, int count)
		{
			var property = ReflectionHelper.GetProperty(list);
			var id = property.Name + ".Index";

			return new HiddenTag()
				.Id(id)
				.Attr(HtmlAttributeConstants.Value, count);
		}

		[Obsolete("Use Knockoutjs")]
		public static HtmlTag ListHiddenValue<T, V>(this T model, Expression<Func<T, IEnumerable<V>>> listSelector, V listItem,
		                                            Expression<Func<V, object>> propertySelector, int count)
			where T : class
		{
			var listProperty = ReflectionHelper.GetProperty(listSelector);
			var propertyProperty = ReflectionUtilities.GetMemberExpression(propertySelector);
			var name = string.Format("{0}[{1}].{2}", listProperty.Name, count, propertyProperty.Member.Name);
			var value = propertySelector.Compile().DynamicInvoke(listItem);

			return new HiddenTag()
				.Attr(HtmlAttributeConstants.Name, name)
				.Id(name)
				.Attr(HtmlAttributeConstants.Value, value);
		}

		[Obsolete("Use Knockoutjs")]
		public static HtmlTag ListInputFor<T, V>(this T model, Expression<Func<T, IEnumerable<V>>> listSelector, V listItem,
		                                         Expression<Func<V, object>> propertySelector, int count)
			where T : class
			where V : class
		{
			var listProperty = ReflectionHelper.GetProperty(listSelector);
			var propertyProperty = ReflectionUtilities.GetMemberExpression(propertySelector);
			var name = string.Format("{0}[{1}].{2}", listProperty.Name, count, propertyProperty.Member.Name);
			var id = string.Format("{0}_{1}__{2}", listProperty.Name, count, propertyProperty.Member.Name);

			return listItem
				.InputFor(propertySelector)
				.Attr(HtmlAttributeConstants.Name, name)
				.Id(id);
		}
	}
}