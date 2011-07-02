namespace HtmlTags.UI.Exports
{
	using HtmlTags;

	public static class HtmlTagExportListExtensions
	{
		public static IExportElement GetExportList(this HtmlTag listTag)
		{
			var visitor = new HtmlTagToExportVisitor();
			return visitor.VisitList(listTag);
		}

		public static void AddList(this ExportDocument export, HtmlTag listTag)
		{
			export.Add(listTag.GetExportList());
		}
	}
}