namespace HtmlTags.UI.Builders
{
	using BclExtensionMethods;
	using Extensions;
	using FubuMVC.UI.Configuration;

	public class CheckBoxBuilder : BaseElementBuilder
	{
		public static string CheckBoxValueWhenChecked = "true";

		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.PropertyType.In(typeof (bool));
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var isChecked = request.Value<bool>();

			return Tags.Checkbox(isChecked)
				.Value(CheckBoxValueWhenChecked)
				.Id(request.ElementId);
		}
	}
}