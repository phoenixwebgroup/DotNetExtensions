namespace HtmlTags.UI.Conventions
{
	public interface IFiltersResetButtonConvention
	{
		string DefaultResetText{ get; set;}
		HtmlTag ResetFilters();
	}
}