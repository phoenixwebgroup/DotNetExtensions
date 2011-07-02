namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using Conventions;
	using HtmlTags;

	public static class PageActions
	{
		public static HtmlTag ButtonActionForSelected(string buttonName, string action)
		{
			const string template =
				@"$.pageActions.ButtonActionForSelected('{0}','{1}');";
			var script = string.Format(template, buttonName, action);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		public static HtmlTag ButtonCommandForSelected(string buttonName, string action)
		{
			const string template =
				@"$.pageActions.ButtonCommandForSelected('{0}','{1}');";
			var script = string.Format(template, buttonName, action);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		public static HtmlTag ButtonAction(string buttonName, string action)
		{
			const string template =
				@"$.pageActions.ButtonAction('{0}','{1}');";
			var script = string.Format(template, buttonName, action);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		public static HtmlTag RefreshFilterForm(this HtmlHelper helper, string filterForm)
		{
			const string template =
				@"$.pageActions.RefreshFilterForm('{0}')";
			var script = string.Format(template, filterForm);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		public static HtmlTag ContextDataForRequests(this HtmlHelper helper, object parameters)
		{
			const string template = @"$.pageActions.contextDataForRequests = {0};";
			var parametersJson = JsonUtil.ToJson(parameters);
			var script = string.Format(template, parametersJson);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		public static IPageActionConvention Convention { get; set; }

		public static HtmlTag ButtonsFor<TController>(HtmlHelper helper,
													  params Expression<Action<TController>>[] expressions)
			where TController : IController
		{
			return Convention.ButtonsFor(helper, expressions);
		}

		public static HtmlTag MenuItemFor<TController>(UrlHelper helper, Expression<Action<TController>> action,
													   MethodInfo currentMethod, object routeValues)
		{
			return Convention.MenuItemFor(helper, action, currentMethod, routeValues);
		}

		public static HtmlTag MenuItemFor(string url, string text, bool selected)
		{
			return Convention.MenuItemFor(url, text, selected);
		}
	}
}