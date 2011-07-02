namespace HtmlTags.UI.Exports
{
	using System.Web.Mvc;

	public class OnExport_ReplaceWithExportDocumentResult : IActionFilter
	{
		public bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return controllerContext.HttpContext.Request["outputType"] != null;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		private void Export(ActionExecutedContext filterContext, IExportableViewModel document)
		{
			ReplaceIfCsv(filterContext, document);
			ReplaceIfPrint(filterContext, document);
			ReplaceIfPdf(filterContext, document);
		}

		private void ReplaceIfPdf(ActionExecutedContext filterContext, IExportableViewModel export)
		{
			if (!RequestIsPdf(filterContext)) return;
			
			if(export is IHaveACustomPdfExport)
			{
				var customExport = (export as IHaveACustomPdfExport);
				var html = customExport.GetPdfExport().ToString();
				var settings = customExport.GetOptions();
				filterContext.Result = new ExportDocumentResult(html, ExportType.CustomPdf, false, settings);
			}
			else
			{
				filterContext.Result = new ExportDocumentResult(export.GetExport(), ExportType.Pdf, false);
			}
		}

		private void ReplaceIfPrint(ActionExecutedContext filterContext, IExportableViewModel export)
		{
			if (!RequestIsPrint(filterContext)) return;
			
			if(export is IHaveACustomHtmlExport)
			{
				var customExport = (export as IHaveACustomHtmlExport);
				var html = customExport.GetHtmlExport().ToString();
				var settings = customExport.GetOptions();
				filterContext.Result = new ExportDocumentResult(html, ExportType.Html, false, settings);
			}
			else
			{
				filterContext.Result = new ExportDocumentResult(export.GetExport(), ExportType.Html, false);
			}
		}

		private void ReplaceIfCsv(ActionExecutedContext filterContext, IExportableViewModel export)
		{
			if (!RequestIsCsv(filterContext)) return;
			
			filterContext.Result = new ExportDocumentResult(export.GetExport(), ExportType.Csv, false);
		}

		private bool RequestIsPdf(ActionExecutedContext filterContext)
		{
			var outputType = filterContext.HttpContext.Request["outputType"];
			return outputType == OutputType.Pdf.ToString();
		}

		private bool RequestIsPrint(ActionExecutedContext filterContext)
		{
			var outputType = filterContext.HttpContext.Request["outputType"];
			return outputType == OutputType.Print.ToString();
		}

		private bool RequestIsCsv(ActionExecutedContext filterContext)
		{
			var outputType = filterContext.HttpContext.Request["outputType"];
			return outputType == OutputType.Csv.ToString();
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var view = filterContext.Result as ViewResultBase;
			if (view == null)
			{
				return;
			}
			var exportable = view.ViewData.Model as IExportableViewModel;
			if (exportable == null)
			{
				return;
			}
			Export(filterContext, exportable);
		}
	}
}