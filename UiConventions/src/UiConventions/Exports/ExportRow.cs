namespace HtmlTags.UI.Exports
{
	using System;
	using System.Collections.Generic;
	using System.Web.UI.WebControls;

	public class ExportRow : IExportElement
	{
		protected List<ExportCell> Cells { get; set; }
		private HorizontalAlign DefaultHorizontalAlign;

		public ExportRow(HorizontalAlign defaultAlignment)
		{
			Cells = new List<ExportCell>();
			DefaultHorizontalAlign = defaultAlignment;
		}

		public void AddCell(ExportCell cell)
		{
			if (cell.HorizontalAlignment == HorizontalAlign.NotSet)
			{
				cell.HorizontalAlignment = DefaultHorizontalAlign;
			}
			Cells.Add(cell);
		}

		public void ExportTo(ExportVisitor visitor)
		{
			Action inner = () => Cells.ForEach(r => r.ExportTo(visitor));
			visitor.Visit(this, inner);
		}
	}
}