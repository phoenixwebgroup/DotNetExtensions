namespace HtmlTags.UI.Builders
{
	using System.Collections;
	using Attributes;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Helpers;

	public class MultiSelectListBuilder : BaseElementBuilder
	{
		public static string SelectListChoicesClass = "selectlist-choices";

		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.HasAttribute<MultiSelectAttribute>()
				&& def.Accessor.HasAttribute<OptionsFromAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var attribute = request.Accessor.GetAttribute<MultiSelectAttribute>();
			var selected = request.RawValue as IEnumerable;
			var select = SelectBuilder.Build(request)
				.AllowMultiple()
				.SelectValues(selected)
				.Rows(attribute.Rows)
				.AddClass(SelectListChoicesClass);
			select.Next = JQuerySelectList.SelectList(request.ElementId);
			return select;
		}
	}
}