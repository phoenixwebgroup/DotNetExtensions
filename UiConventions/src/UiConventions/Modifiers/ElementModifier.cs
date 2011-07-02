namespace HtmlTags.UI.Modifiers
{
	using FubuMVC.UI.Configuration;

	public abstract class ElementModifier : IElementModifier
	{
		public TagModifier CreateModifier(AccessorDef accessorDef)
		{
			if (matches(accessorDef))
			{
				return Build;
			}
			return null;
		}

		protected abstract bool matches(AccessorDef accessor);

		public abstract void Build(ElementRequest request, HtmlTag tag);
	}
}