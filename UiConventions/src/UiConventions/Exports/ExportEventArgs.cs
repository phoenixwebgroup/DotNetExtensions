namespace HtmlTags.UI.Exports
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;

	/// <summary>
	/// 	These are the valid export types.
	/// </summary>
	public enum ExportType
	{
		Html,
		Pdf,
		Csv,
		CustomPdf
	}

	[Serializable]
	public class ExportEventArgs : EventArgs
	{
		private static Regex _SanitizeFileRegex;
		public string Document { get; set; }
		public string Title { get; set; }

		public virtual ExportType ExportType { get; set; }
		public bool SendByEmail { get; set; }
		public string EmailAddress { get; set; }

		public ExportSettings Settings { get; set; }

		public class ExportSettings
		{
			public ExportSettings()
			{
				PdfPageSize = PdfPageSize.Letter;
				PdfPageOrientation = PDFPageOrientation.Landscape;
			}

			public PdfPageSize PdfPageSize { get; set; }
	
			public PDFPageOrientation PdfPageOrientation { get; set; }
		}

		public ExportEventArgs() : this(String.Empty, String.Empty)
		{
		}

		public ExportEventArgs(string document, string title)
		{
			Settings = new ExportSettings();
			Document = document;
			Title = title;

			ExportType = ExportType.Html;
			SendByEmail = false;
		}

		public ExportEventArgs(ExportType type, IExportElement document, bool sendByEmail, string fileName)
		{
			Settings = new ExportSettings();
			SetTitle(fileName);
			ExportType = type;
			SendByEmail = sendByEmail;

			ExportDocument(document);
		}

		private void SetTitle(string fileName)
		{
			var title = fileName + "_" + DateTime.Now.ToShortDateString();
			Title = SanitizeTitle(title);
		}

		private static string SanitizeTitle(string title)
		{
			return _SanitizeFileRegex.Replace(title, "_").Replace(" ", null);
		}

		static ExportEventArgs()
		{
			var invalidCharacters = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			var invalidRegex = string.Format(@"[{0}]", invalidCharacters);
			_SanitizeFileRegex = new Regex(invalidRegex);
		}

		private void ExportDocument(IExportElement document)
		{
			ExportVisitor visitor;
			if (ExportType == ExportType.Html || ExportType == ExportType.Pdf)
			{
				visitor = new HtmlExportVisitor();
			}
			else
			{
				visitor = new CsvExportVisitor();
			}
			document.ExportTo(visitor);
			Document = visitor.GetResult();
		}
	}
}