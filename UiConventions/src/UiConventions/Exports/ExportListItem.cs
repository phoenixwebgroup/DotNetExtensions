namespace HtmlTags.UI.Exports
{
	using System.Collections.Generic;

	public class ExportListItem : IExportElement
	{
		protected virtual List<ExportListItem> Items { get; set; }
		public string Text { get; set; }

		public ExportListItem()
		{
			Items = new List<ExportListItem>();
		}

		public virtual void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, () => Items.ForEach(r => r.ExportTo(visitor)));
		}

		public void AddItem(ExportListItem item)
		{
			Items.Add(item);
		}
	}
}