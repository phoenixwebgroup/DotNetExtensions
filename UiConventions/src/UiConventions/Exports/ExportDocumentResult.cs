namespace HtmlTags.UI.Exports
{
	using System;
	using System.Globalization;
	using System.Text;
	using System.Web;
	using System.Web.Mvc;

	public class ExportDocumentResult : ActionResult
	{
		private readonly ExportDocument _ExportDocument;
		private readonly ExportType _ExportType;
		private readonly bool _SendEmail;
		private readonly ExportEventArgs _Options;

		public ExportDocumentResult(ExportDocument exportDocument, ExportType exportType, bool sendEmail)
		{
			_ExportDocument = exportDocument;
			_ExportType = exportType;
			_SendEmail = sendEmail;
		}

		public ExportDocumentResult(string html, ExportType exportType, bool sendEmail, ExportEventArgs.ExportSettings settings)
		{
			_Options = new ExportEventArgs(html, "export")
			           	{
			           		ExportType = exportType,
			           		SendByEmail = sendEmail,
			           		Settings = settings
			           	};
			_ExportType = exportType;
			_SendEmail = sendEmail;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var options = _Options ?? new ExportEventArgs(_ExportType, _ExportDocument, _SendEmail, _ExportDocument.Header);
			if (_SendEmail)
			{
				ExportToEmail();
			}
			else
			{
				ExportToResponse(options, context);
			}
		}

		private void ExportToEmail()
		{
		}

		private void ExportToResponse(ExportEventArgs options, ControllerContext context)
		{
			switch (_ExportType)
			{
				case ExportType.Csv:
					ExportCsv(options, context.HttpContext.Response);
					break;
				case ExportType.Html:
					ExportHtml(options, context.HttpContext.Response);
					break;
				case ExportType.Pdf:
				case ExportType.CustomPdf:
					ExportPdf(options, context.HttpContext.Response);
					break;
			}
		}

		public static void ExportPdf(ExportEventArgs options, HttpResponseBase response)
		{
			response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.pdf", options.Title));
			response.ContentType = "application/pdf";
			response.BinaryWrite(ExportPdfHelper.ToPdfBytes(options));
		}

		public static void ExportHtml(ExportEventArgs options, HttpResponseBase response)
		{
			response.Write(options.Document);
		}

		public static void ExportCsv(ExportEventArgs options, HttpResponseBase response)
		{
			response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.csv", options.Title));
			response.ContentType = "application/csv";
			if (CultureInfo.CurrentUICulture.Name != "en-US")
			{
				response.ContentEncoding = Encoding.Unicode;
			}
			response.Write(options.Document);
		}
	}
}