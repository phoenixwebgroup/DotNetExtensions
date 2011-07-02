namespace HtmlTags.UI.Helpers
{
	using System.Web.Mvc;
	using HtmlTags;

	public static class JQuerySelectList
	{
		public const string Template = @"$('select#{0}').selectList({{ sort: true }});";

		public static HtmlTag SelectList(this HtmlHelper helper, string name)
		{
			return SelectList(name);
		}

		public static HtmlTag SelectList(string name)
		{
			var script = string.Format(Template, name);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}
	}
}