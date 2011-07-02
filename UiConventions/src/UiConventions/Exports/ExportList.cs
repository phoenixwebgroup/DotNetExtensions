namespace HtmlTags.UI.Exports
{
	using System.Collections.Generic;

	public class ExportList : ExportListItem
	{
		public ExportList()
		{
			Items = new List<ExportListItem>();
		}

		public override void ExportTo(ExportVisitor visitor)
		{
			visitor.Visit(this, () => Items.ForEach(r => r.ExportTo(visitor)));
		}
	}
}