namespace HtmlTags.UI.Validation
{
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using BclExtensionMethods;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class StringLengthValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			return accessor.Accessor.HasAttribute<StringLengthAttribute>();
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			var length = request.Accessor.GetAttribute<StringLengthAttribute>();
			tag.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => AddRangeValidation(t, length));
		}

		private void AddRangeValidation(HtmlTag tag, StringLengthAttribute length)
		{
			tag.Attr("minlength", length.MinimumLength);
			tag.Attr("maxlength", length.MaximumLength);
		}
	}
}