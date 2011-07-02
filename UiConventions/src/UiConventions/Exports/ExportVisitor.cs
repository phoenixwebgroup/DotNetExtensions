namespace HtmlTags.UI.Exports
{
	using System;

	public abstract class ExportVisitor
	{
		public abstract void Visit(ExportTable exportTable, Action inner);
		public abstract void Visit(ExportRow row, Action inner);
		public abstract void Visit(ExportCell cell, Action inner);
		public abstract void Visit(ExportDocument table, Action inner);
		public abstract void Visit(ExportParagraph paragraph, Action inner);
		public abstract void Visit(ExportImage image, Action inner);
		public abstract void Visit(ExportList list, Action inner);
		public abstract void Visit(ExportListItem listItem, Action inner);
		public abstract string GetResult();
	}
}