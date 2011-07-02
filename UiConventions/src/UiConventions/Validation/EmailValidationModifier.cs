namespace HtmlTags.UI.Validation
{
	using System.Linq;
	using BclExtensionMethods;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Modifiers;

	public class EmailValidationModifier : ElementModifier
	{
		protected override bool matches(AccessorDef accessor)
		{
			return accessor.Accessor.HasAttribute<EmailAttribute>();
		}

		public override void Build(ElementRequest request, HtmlTag tag)
		{
			tag
				.AllTags()
				.Where(t => t.IsInputElement())
				.ForEach(t => t.AddClass("email"));
		}
	}
}