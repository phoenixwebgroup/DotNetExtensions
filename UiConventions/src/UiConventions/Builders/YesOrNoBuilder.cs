namespace HtmlTags.UI.Builders
{
	using Attributes;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	/// <summary>
	/// For display purposes, shows a span tag with the text yes or no.
	/// </summary>
	public class YesOrNoBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.HasAttribute<YesOrNoAttribute>()
			       && def.Accessor.PropertyType == typeof (bool);
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var text = request.Value<bool>() ? "Yes" : "No";
			return Tags.Span.Text(text);
		}
	}
}