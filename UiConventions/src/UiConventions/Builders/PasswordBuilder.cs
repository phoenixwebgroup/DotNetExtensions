namespace HtmlTags.UI.Builders
{
	using System;
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
			       && (AccessorHasPasswordAttribute(accessorDefinition) || AccessorNameContainsPassword(accessorDefinition));
		}

		private static bool AccessorHasPasswordAttribute(AccessorDef accessorDefinition)
		{
			return accessorDefinition.Accessor.HasAttribute<PasswordAttribute>();
		}

		private static bool AccessorNameContainsPassword(AccessorDef accessorDefinition)
		{
			return accessorDefinition.Accessor.Name.IndexOf("Password", StringComparison.InvariantCultureIgnoreCase) >= 0;
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