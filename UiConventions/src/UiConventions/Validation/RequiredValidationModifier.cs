namespace HtmlTags.UI.Validation
{
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using BclExtensionMethods;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class RequiredValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			return accessor.Accessor.HasAttribute<RequiredAttribute>();
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			tag
				.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => t.AddClass("required"));
		}
	}
}