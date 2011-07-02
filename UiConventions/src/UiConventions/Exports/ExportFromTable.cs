namespace HtmlTags.UI.Exports
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using BclExtensionMethods;

	public class ExportFromTable
	{
		public readonly ExportTable ExportTable = new ExportTable();

		public static Regex HtmlTagRegex;

		static ExportFromTable()
		{
			HtmlTagRegex = new Regex("</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>",
			                         RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled |
			                         RegexOptions.ExplicitCapture);
		}

		/// <summary>
		/// 	Transforms an HTML table into an Export table
		/// </summary>
		/// <param name = "tableToExport">The HTML table to transform</param>
		/// <returns></returns>
		public static ExportTable Create(Table tableToExport)
		{
			return new ExportFromTable(tableToExport).ExportTable;
		}

		public ExportFromTable(Table table)
		{
			table.Rows.OfType<TableRow>()
				.Where(r => r.Visible)
				.Select(ExportRow)
				.ForEach(ExportTable.AddRow);
		}

		private static ExportRow ExportRow(TableRow tableRow)
		{
			var row = new ExportRow(tableRow.HorizontalAlign);
			tableRow.Cells.OfType<TableCell>()
				.Where(c => c.Visible)
				.Select(ExportCell)
				.ForEach(row.AddCell);
			return row;
		}

		private static ExportCell ExportCell(TableCell tableCell)
		{
			var cell = CreateCell(tableCell);
			RenderInnerControlContent(tableCell, cell);
			return cell;
		}

		private static void RenderInnerControlContent(TableCell tableCell, ExportCell cell)
		{
			if (!tableCell.HasControls())
			{
				return;
			}

			using (var writer = new StringWriter())
			using (var html = new HtmlTextWriter(writer))
			{
				tableCell.Controls.OfType<Control>()
					.Where(c => c.Visible)
					.Where(c => !(c is HiddenField))
					.ForEach(c => SetContent(c, cell, html));

				cell.Markup += writer.ToString();
			}

			cell.Text += HtmlTagRegex.Replace(cell.Markup, String.Empty);
		}

		private static void SetContent(Control control, ExportCell cell, HtmlTextWriter htmlwriter)
		{
			if (control is IButtonControl)
			{
				var button = control as IButtonControl;
				cell.Text += button.Text;
			}
			else
			{
				control.RenderControl(htmlwriter);
			}
		}

		private static ExportCell CreateCell(TableCell tableCell)
		{
			return new ExportCell
			       {
			       	AllowWrapping = tableCell.Wrap,
			       	ColumnSpan = tableCell.ColumnSpan,
			       	HorizontalAlignment = tableCell.HorizontalAlign,
			       	Markup = tableCell.Text ?? string.Empty,
					Text = string.Empty
			       };
		}
	}
}