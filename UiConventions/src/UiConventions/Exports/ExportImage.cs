namespace HtmlTags.UI.Exports
{
	public class ExportImage : IExportElement
	{
		public string Source { get; set; }
		public string AlternateText{ get; set;}
		public void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, ActionExtensions.NoOp);
		}
	}
}