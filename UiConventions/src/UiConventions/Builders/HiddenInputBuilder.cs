namespace HtmlTags.UI.Builders
{
	using Attributes;
	using Constants;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using FubuMVC.UI.Tags;

	public class HiddenInputBuilder : ElementBuilder
	{
		protected override bool matches(AccessorDef accessorDefinition)
		{
			return accessorDefinition.Accessor.HasAttribute<HiddenAttribute>();
		}

		public override HtmlTag Build(ElementRequest request)
		{
			return
				TagActionExpression.BuildTextbox(request)
					.Id(request.ElementId)
					.Attr(HtmlAttributeConstants.Type, InputTypeConstants.Hidden);
		}
	}
}