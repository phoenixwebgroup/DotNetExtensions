namespace HtmlTags.UI.Exports
{
	using System.Web.UI.WebControls;

	public class ExportParagraph : IExportElement
	{
		public string Text { get; set; }
		public HorizontalAlign HorizontalAlignment { get; set; }

		public static ExportParagraph Create(string text, HorizontalAlign horizontalAlign)
		{
			return new ExportParagraph {Text = text, HorizontalAlignment = horizontalAlign};
		}

		public void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, ActionExtensions.NoOp);
		}

		public static ExportParagraph Create(string text)
		{
			return new ExportParagraph {Text = text};
		}
	}
}