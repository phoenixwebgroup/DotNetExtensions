namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Web.Mvc;

	public static class AjaxForms
	{
		[Obsolete("Use javascript $.ajaxFormsExtensions.FilterForm")]
		public static string FilterForm(this AjaxHelper helper, string formSelector, string targetSelector)
		{
			const string template =
				@"$.ajaxFormsExtensions.FilterForm('{0}','{1}');";
			var script = string.Format(template, formSelector, targetSelector);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script).ToString();
		}

		[Obsolete("Use javascript $.ajaxFormsExtensions.MakeFormsAjax")]
		public static string MakeFormsAjax(this AjaxHelper helper)
		{
			const string template =
				@"$.ajaxFormsExtensions.MakeFormsAjax();";
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(template).ToString();			
		}
	}
}