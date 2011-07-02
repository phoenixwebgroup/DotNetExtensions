namespace HtmlTags.UI.Exports
{
	public class ExportPdfHelper
	{
		public static IHtmlToPdfConverter Converter { get; set; }

		public static byte[] ToPdfBytes(ExportEventArgs exportEventArgs)
		{
			return Converter.ToPdfBytes(exportEventArgs);
		}

		public const string cssPdf =
			@"
<style>
/* Used for printing */
table.printReport
{
	empty-cells: show;
	border-collapse: collapse;
	page-break-inside: avoid;
}

table.printReport th
{
	font-family: Tahoma,Verdana;
	font-size: 8pt;
	padding: 1px;
	text-align: center;
}
table.printReport th.month
{
	font-family: Tahoma, Verdana;
	font-size: 8pt;
	padding: 1px;
	text-align: center;
}
table.printReport td
{
	text-align: center;
	font-family: Tahoma, Verdana;
	font-size: 8pt;
	padding: 1px;
}
table.printReport thead td
{
	font-family: Tahoma,Verdana;
	font-size: 8pt;
	padding: 1px;
}
table.printReport td.left
{
	text-align: left;
	font-family: Tahoma, Verdana;
	font-size: 8pt;
	padding: 1px;
}
table.printReport span
{
	font-size: 8pt;
	margin-right: 2px;
}

</style>
";
	}
}