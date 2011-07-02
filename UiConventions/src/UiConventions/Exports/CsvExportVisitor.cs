namespace HtmlTags.UI.Exports
{
	using System;
	using System.Linq;
	using System.Text;
	using BclExtensionMethods;

	public class CsvExportVisitor : ExportVisitor
	{
		private readonly StringBuilder _Csv = new StringBuilder();
		private const char ColumnDelimiter = ',';
		private const string ColumnEscape = "\"";
		private const string EscapeForColumnEscape = "\"\"";

		public override void Visit(ExportTable exportTable, Action inner)
		{
			inner();
			AppendRowDelimiter();
		}

		public override void Visit(ExportRow row, Action inner)
		{
			inner();
			StripTrailingSeparator();
			AppendRowDelimiter();
		}

		public override void Visit(ExportList list, Action inner)
		{
			inner();
			StripTrailingSeparator();
			AppendRowDelimiter();
		}

		public override void Visit(ExportListItem item, Action inner)
		{
			inner();
			var escapedValue = EscapeForCsv(item.Text);
			_Csv.Append(escapedValue);
			AppendColumnDelimiters(1);
		}

		public override void Visit(ExportCell cell, Action inner)
		{
			inner();
			var escapedValue = EscapeForCsv(cell.Text);
			_Csv.Append(escapedValue);
			AppendColumnDelimiters(cell.ColumnSpan);
		}

		public override void Visit(ExportDocument table, Action inner)
		{
			inner();
		}

		public override void Visit(ExportParagraph paragraph, Action inner)
		{
			var text = EscapeForCsv(paragraph.Text);
			_Csv.Append(text);
			inner();
			AppendRowDelimiter();
		}

		private void StripTrailingSeparator()
		{
			if (_Csv[_Csv.Length - 1] == ColumnDelimiter)
			{
				_Csv.Remove(_Csv.Length - 1, 1);
			}
		}

		private void AppendRowDelimiter()
		{
			_Csv.AppendLine();
		}

		private void AppendColumnDelimiters(int? columnSpan)
		{
			var colspan = columnSpan ?? 1;
			if (colspan == 0) colspan = 1;
			Enumerable.Range(0, colspan).ForEach(i => _Csv.Append(ColumnDelimiter));
		}

		public override string GetResult()
		{
			return _Csv.ToString();
		}

		public override void Visit(ExportImage image, Action inner)
		{
			var text = EscapeForCsv(image.AlternateText);
			_Csv.Append(text);
			inner();
			AppendRowDelimiter();
		}
		private static string EscapeForCsv(string columnValue)
		{
			if (string.IsNullOrEmpty(columnValue))
			{
				return string.Empty;
			}

			var cleaned = columnValue
				.Replace("&nbsp;", " ")
				.Trim()
				.Replace(ColumnEscape, EscapeForColumnEscape);

			return String.Concat(ColumnEscape, cleaned, ColumnEscape);
		}
	}
}