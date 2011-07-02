namespace HtmlTags.UI.Exports
{
	public interface IHaveACustomPdfExport
	{
		HtmlTag GetPdfExport();
		ExportEventArgs.ExportSettings GetOptions();
	}
}