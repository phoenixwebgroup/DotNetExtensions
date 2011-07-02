namespace HtmlTags.UI.Conventions
{
	using Extensions;

	public class FiltersResetButtonConvention : IFiltersResetButtonConvention
	{
		public string DefaultResetText { get; set;}

		public FiltersResetButtonConvention()
		{
			DefaultResetText = "Reset";
		}

		public HtmlTag ResetFilters()
		{
			var reset = ViewConventionExtensions.Button(DefaultResetText)
				.Attr("onclick", "reset();$.pageActions.ResetFilters();$.pageActions.RefreshData();");
			return Tags.Div.AddClass("resetButton").Nest(reset);
		}
	}
}