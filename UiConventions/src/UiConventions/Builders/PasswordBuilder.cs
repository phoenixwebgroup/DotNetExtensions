namespace HtmlTags.UI.Builders
{
	using Attributes;
	using BclExtensionMethods;
	using Constants;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using FubuMVC.UI.Tags;

	public class PasswordBuilder : ElementBuilder
	{
		protected override bool matches(AccessorDef accessorDefinition)
		{
			return accessorDefinition.Accessor.PropertyType.In(typeof (string))
			       && accessorDefinition.Accessor.HasAttribute<PasswordAttribute>();
		}

		public override HtmlTag Build(ElementRequest request)
		{
			return
				TagActionExpression.BuildTextbox(request)
					.Id(request.ElementId)
					.Attr(HtmlAttributeConstants.Type, InputTypeConstants.Password);
		}
	}
}