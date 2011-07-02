namespace HtmlTags.UI
{
	using System;
	using ExpertPdf.HtmlToPdf;
	using Exports;
	using PDFPageOrientation = Exports.PDFPageOrientation;
	using PdfPageSize = Exports.PdfPageSize;

	public class ExpertPdfConverter : IHtmlToPdfConverter
	{
		public byte[] ToPdfBytes(ExportEventArgs exportEventArgs)
		{
			var converter = new PdfConverter
			                	{
			                		LicenseKey = Settings.Default.HtmlToPdfLicenseKey
			                	};
			converter.PdfDocumentOptions.PdfPageSize = GetPdfPageSize(exportEventArgs);
			converter.PdfDocumentOptions.PdfPageOrientation = GetPageOrientation(exportEventArgs);
			converter.PdfDocumentOptions.LeftMargin = 18;
			converter.PdfDocumentOptions.RightMargin = 18;
			converter.PdfDocumentOptions.TopMargin = 18;
			converter.PdfDocumentOptions.BottomMargin = 18;
			converter.PdfDocumentOptions.GenerateSelectablePdf = true;
			converter.PdfDocumentOptions.FitWidth = true;
			converter.PageWidth = 1200;
			converter.AvoidTextBreak = true;
			converter.AvoidImageBreak = true;
			var html = exportEventArgs.ExportType == ExportType.CustomPdf
			           	? exportEventArgs.Document
			           	: String.Format("<html><head>{0}</head><body><CENTER><BR/>{1}</center></body></html>", ExportPdfHelper.cssPdf,
			           	                exportEventArgs.Document);
			return converter.GetPdfBytesFromHtmlString(html);
		}

		private static ExpertPdf.HtmlToPdf.PdfPageSize GetPdfPageSize(ExportEventArgs exportEventArgs)
		{
			switch (exportEventArgs.Settings.PdfPageSize)
			{
				case PdfPageSize.Letter:
					return ExpertPdf.HtmlToPdf.PdfPageSize.Letter;
				case PdfPageSize.Legal:
					return ExpertPdf.HtmlToPdf.PdfPageSize.Legal;
			}
			throw new NotSupportedException();
		}

		private static ExpertPdf.HtmlToPdf.PDFPageOrientation GetPageOrientation(ExportEventArgs exportEventArgs)
		{
			switch (exportEventArgs.Settings.PdfPageOrientation)
			{
				case PDFPageOrientation.Landscape:
					return ExpertPdf.HtmlToPdf.PDFPageOrientation.Landscape;
				case PDFPageOrientation.Portrait:
					return ExpertPdf.HtmlToPdf.PDFPageOrientation.Portrait;
			}
			throw new NotSupportedException();
		}
	}
}