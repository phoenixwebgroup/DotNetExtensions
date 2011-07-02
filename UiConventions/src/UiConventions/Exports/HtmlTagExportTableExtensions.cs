namespace HtmlTags.UI.Exports
{
	using System.Collections.Generic;
	using HtmlTags;

	public static class HtmlTagExportTableExtensions
	{
		public static IList<IExportElement> GetExportTable(this HtmlTag tableTag)
		{
			var visitor = new HtmlTagToExportVisitor();
			visitor.Visit(tableTag);
			return visitor.GetElements();
		}

		public static void AddTable(this ExportDocument export, HtmlTag tableTag)
		{
			export.AddRange(tableTag.GetExportTable());
		}
	}
}