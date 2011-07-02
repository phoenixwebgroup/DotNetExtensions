namespace HtmlTags.UI.Builders
{
	using System;
	using Attributes;
	using BclExtensionMethods;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class DateAndTimePickerBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.PropertyType.In(typeof (DateTime), typeof (DateTime?))
			       && def.Accessor.HasAttribute<DateAndTimeAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var tag = new DateAndTimeTextBoxTag(request.ElementId);

			if (request.ValueIsEmpty())
			{
				return tag;
			}

			var value = request.Value<DateTime>();
			return tag.SetDateAndTime(value);
		}
	}
}