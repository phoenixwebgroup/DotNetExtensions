namespace HtmlTags.UI.Helpers
{
	public class JQueryHelpers
	{
		public static string WrapWithJQueryReady(string script)
		{
			var template = @"$(function(){{{0}}});";
			return string.Format(template, script);
		}

		public static HtmlTag WrapWithJQueryReadyAndScriptTag(string script)
		{
			return Tags.Script(WrapWithJQueryReady(script));
		}
	}
}