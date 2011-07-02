namespace HtmlTags.UI.Validation
{
	using System.Linq;
	using BclExtensionMethods;
	using Extensions;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class NumericValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			var propertyType = accessor.Accessor.PropertyType;

			return propertyType == typeof (decimal)
			       || propertyType == typeof (decimal?)
				   || propertyType == typeof(int)
				   || propertyType == typeof(int?) 
				   || propertyType == typeof(short)
				   || propertyType == typeof(short?)
			       || propertyType == typeof (float)
			       || propertyType == typeof (float?)
			       || propertyType == typeof (double)
			       || propertyType == typeof (double?)
			       || propertyType == typeof (long)
			       || propertyType == typeof (long?);
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			// todo can we narrow down for numbers that it's a whole number (probably need a regex to allow commas too)
			tag.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => t.AddClass("number"));
		}
	}
}