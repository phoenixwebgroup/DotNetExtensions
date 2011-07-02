namespace HtmlTags.UI.Conventions
{
	using FubuCore.Reflection;

	public static class LabelingConvention
	{
		public static ILabelingConvention Convention { get; set; }

		static LabelingConvention()
		{
			Convention = new SpaceBeforeCapitalsLabelingConvention();
		}

		public static string GetLabelText(Accessor accessor)
		{
			return Convention.GetLabelText(accessor);
		}

		public static string GetLabelText(string text)
		{
			return Convention.GetLabelText(text);
		}
	}
}