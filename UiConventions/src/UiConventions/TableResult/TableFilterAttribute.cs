namespace HtmlTags.UI.TableResult
{
	using System.Linq;
	using System.Web.Mvc;

	public class TableFilterAttribute : ActionFilterAttribute
	{
		private TableOutputType _OutputType;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var gridInput = filterContext.ActionParameters.FirstOrDefault(p => p.Value is TableInputModel).Value as TableInputModel;
			if (gridInput == null)
			{
				return;
			}
			_OutputType = gridInput.OutputType;
			if (_OutputType == TableOutputType.Csv)
			{
				gridInput.NotPaged = true;
			}
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			ActionResult actionResult = null;
			var result = filterContext.Result as TableResult;
			if (result == null)
			{
				return;
			}

			switch (_OutputType)
			{
				case TableOutputType.JqGrid:
					actionResult = new JsonResult {Data = result.ToJqGridJson()};
					break;
				case TableOutputType.Csv:
					var actionName = filterContext.ActionDescriptor.ActionName;
					actionResult = new CsvResult(result.Rows, actionName);
					break;
				case TableOutputType.Flexigrid:
					actionResult = new JsonResult {Data = result.ToFlexigridJson()};
					break;
			}

			filterContext.Result = actionResult;
		}
	}
}