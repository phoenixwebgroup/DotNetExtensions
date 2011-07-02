namespace HtmlTags.UI.Validation
{
	using System;
	using System.Linq;
	using BclExtensionMethods;
	using BclExtensionMethods.ValueTypes;
	using Extensions;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class DateValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			var propertyType = accessor.Accessor.PropertyType;

			return propertyType == typeof (DateTime)
			       || propertyType == typeof (DateTime?)
			       || propertyType == typeof (Date)
			       || propertyType == typeof (Date?);
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			tag.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => t.AddClass("date"));
		}
	}
}