namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;
	using FubuCore.Reflection;

	public static class Html
	{
		[Obsolete("Use conventions and mark your item as a hidden input")]
		public static MvcHtmlString HiddenInput<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			var prop = ReflectionHelper.GetProperty(expression);
			var accessor = ReflectionHelper.GetAccessor(expression);
			return helper.Hidden(prop.Name, accessor.GetValue(helper.ViewData.Model));
		}

		public static bool CurrentActionIs(this HtmlHelper helper, string toCompare)
		{
			var actionName = helper.ViewContext.RouteData.Values["action"].ToString().ToLowerInvariant();
			return actionName == toCompare.ToLowerInvariant();
		}
	}
}