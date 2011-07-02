namespace HtmlTags.UI.Builders
{
	using Attributes;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public static class ClickToEditModifier
	{
		public static void ClickToEdit(ElementRequest request, HtmlTag tag)
		{
			if (tag.IsInputElement())
			{
				var clickToEdit = request.Accessor.GetAttribute<ClickToEditAttribute>();
				tag.AddClass("clickToEdit");
				if (!string.IsNullOrEmpty(clickToEdit.Action))
				{
					tag.Attr("ClickToEditAction", clickToEdit.Action);
				}
			}
		}
	}
}