namespace HtmlTags.UI.Builders
{
	using System;
	using BclExtensionMethods;
	using Extensions;
	using FubuMVC.UI.Configuration;

	public class DatePickerBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.PropertyType.In(typeof (DateTime), typeof (DateTime?));
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var tag = new CalendarTextBox(request.ElementId);

			if (request.ValueIsEmpty())
			{
				return tag;
			}

			var value = request.Value<DateTime>();
			return tag.SetDate(value);
		}
	}
}