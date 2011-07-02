namespace HtmlTags.UI.Builders
{
	using System;
	using Attributes;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	[Obsolete("I don't know why this was ever made")]
	public class HiddenDisplayBuilder : HiddenLabelBuilder
	{
		// not sure why we had two separate implementations of this
	}

	[Obsolete("I don't know why this was ever made")]
	public class HiddenLabelBuilder : ElementBuilder
	{
		protected override bool matches(AccessorDef accessorDefinition)
		{
			return accessorDefinition.Accessor.HasAttribute<HiddenAttribute>();
		}

		public override HtmlTag Build(ElementRequest request)
		{
			return Tags.NoTag;
		}
	}
}