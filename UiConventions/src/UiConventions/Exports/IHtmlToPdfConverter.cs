namespace HtmlTags.UI.Exports
{
	public interface IHtmlToPdfConverter
	{
		byte[] ToPdfBytes(ExportEventArgs exportEventArgs);
	}
}