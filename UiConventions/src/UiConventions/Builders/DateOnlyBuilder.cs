namespace HtmlTags.UI.Builders
{
	using System;
	using Attributes;
	using BclExtensionMethods;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class DateOnlyBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.PropertyType.In(typeof (DateTime), typeof (DateTime?))
			       && def.Accessor.HasAttribute<DateOnlyAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var tag = Tags.Span;

			if (request.ValueIsEmpty())
			{
				return tag;
			}

			var value = request.Value<DateTime>()
				.ToShortDateString();
			return tag.Text(value);
		}
	}
}