namespace HtmlTags.UI.Exports
{
	public interface IExportElement
	{
		void ExportTo(ExportVisitor visitor);
	}
}