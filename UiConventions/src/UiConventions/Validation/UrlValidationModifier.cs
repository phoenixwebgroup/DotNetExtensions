namespace HtmlTags.UI.Validation
{
	using System.Linq;
	using BclExtensionMethods;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using HtmlTags;
	using Extensions;
	using Modifiers;

	public class UrlValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			return accessor.Accessor.HasAttribute<UrlAttribute>();
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			tag
				.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => t.AddClass("url"));
		}
	}
}