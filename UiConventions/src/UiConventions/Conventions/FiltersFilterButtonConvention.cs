namespace HtmlTags.UI.Conventions
{
	using Extensions;

	public class FiltersFilterButtonConvention : IFiltersFilterButtonConvention
	{
		public string DefaultFilterText { get; set; }

		public FiltersFilterButtonConvention()
		{
			DefaultFilterText = "Filter";
		}

		public HtmlTag Filter(string formSelector)
		{
			var filter = ViewConventionExtensions.SubmitButton(DefaultFilterText);
			return Tags.Div.AddClass("filterButton").Nest(filter);
		}
	}
}