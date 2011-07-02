namespace HtmlTags.UI.Exports
{
	using HtmlTags;

	public static class HtmlTagImageExtensions
	{
		public static IExportElement GetExportImage(this HtmlTag imageTag)
		{
			var visitor = new HtmlTagToExportVisitor();
			return visitor.VisitImage(imageTag);
		}

		public static void AddImage(this ExportDocument export, HtmlTag imageTag)
		{
			export.Add(imageTag.GetExportImage());
		}
	}
}