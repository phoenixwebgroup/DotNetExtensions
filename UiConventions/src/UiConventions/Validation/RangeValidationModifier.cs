namespace HtmlTags.UI.Validation
{
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using BclExtensionMethods;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class RangeValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			return accessor.Accessor.HasAttribute<RangeAttribute>();
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			var range = request.Accessor.GetAttribute<RangeAttribute>();
			tag.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => AddRangeValidation(t, range));
		}

		private void AddRangeValidation(HtmlTag tag, RangeAttribute range)
		{
			if (range.Minimum != null)
			{
				tag.Attr("min", range.Minimum);
			}
			if (range.Maximum != null)
			{
				tag.Attr("max", range.Maximum);
			}
		}
	}
}