namespace HtmlTags.UI.ClickToEdit
{
	using System.Web.Mvc;

	public static class ClickToEditExtensions
	{
		public const string InitializationScript = "InitializeClickToEdit('{0}',{1});";

		public static HtmlTag ClickToEditContext(this HtmlTag tag, object context)
		{
			tag.Attr("ClickToEditContext", JsonUtil.ToJson(context));
			return tag;
		}

		public static HtmlTag ClickToEditAction(this HtmlTag tag, string actionUrl)
		{
			tag.Attr("ClickToEditAction", actionUrl);
			return tag;
		}

		public static HtmlTag InitializeClickToEdit(this HtmlHelper helper, string selector, bool changeContext = false)
		{
			var script = string.Format(InitializationScript,selector, changeContext.ToString().ToLower());
			return Tags.Script(script);
		}
	}
}