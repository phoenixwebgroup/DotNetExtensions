namespace HtmlTags.UI.Exports
{
	public interface IHaveACustomHtmlExport
	{
		HtmlTag GetHtmlExport();
		ExportEventArgs.ExportSettings GetOptions();
	}
}