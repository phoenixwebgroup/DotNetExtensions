namespace HtmlTags.UI.Builders
{
	using System;
	using BclExtensionMethods;
	using FubuMVC.UI.Configuration;

	public class EnumDisplayBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return RemoveNullableIfNecessary(def.Accessor.PropertyType).IsEnum;
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var tag = Tags.Span;
			if (request.ValueIsEmpty())
			{
				return tag;
			}

			var text = ((Enum) request.RawValue).ToDescription();

			return tag
				.Text(text);
		}
	}
}