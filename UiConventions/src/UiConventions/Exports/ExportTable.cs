namespace HtmlTags.UI.Exports
{
	using System.Collections.Generic;
	using System.Web.UI.WebControls;

	public class ExportTable : IExportElement
	{
		private List<ExportRow> Rows { get; set; }
		public HorizontalAlign HorizontalAlign { get; set; }

		public ExportTable()
		{
			Rows = new List<ExportRow>();
		}

		public void AddRow(ExportRow row)
		{
			Rows.Add(row);
		}

		public void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, () => Rows.ForEach(r => r.ExportTo(visitor)));
		}
	}
}