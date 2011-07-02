namespace HtmlTags.UI.Exports
{
	using System.Web.UI.WebControls;

	public class ExportCell : IExportElement
	{
		public int? ColumnSpan { get; set; }
		public string Text { get; set; }
		public string Markup { get; set; }
		public bool? AllowWrapping { get; set; }
		public HorizontalAlign HorizontalAlignment { get; set; }

		public void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, ActionExtensions.NoOp);
		}
	}
}