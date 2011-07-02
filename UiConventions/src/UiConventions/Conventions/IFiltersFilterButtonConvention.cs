namespace HtmlTags.UI.Conventions
{
	public interface IFiltersFilterButtonConvention
	{
		string DefaultFilterText{ get; set; }
		HtmlTag Filter(string formSelector);
	}
}
