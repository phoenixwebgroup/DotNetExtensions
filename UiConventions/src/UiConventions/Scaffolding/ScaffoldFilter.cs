namespace HtmlTags.UI.Scaffolding
{
	using System.Web.Mvc;

	public class ScaffoldFilter : IActionFilter
	{
		private ActionExecutedContext _FilterContext;
		private ViewResult _ViewResult;

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			_FilterContext = filterContext;

			if (!IsViewResult())
			{
				return;
			}

			if (ViewNameIsAlreadySet())
			{
				return;
			}

			if (ModelTypeEndsWithForm())
			{
				_ViewResult.ViewData.Model = new ScaffoldedForm(GetModel());
				_ViewResult.ViewName = "ScaffoldedForm";
			}

			if (ModelTypeEndsWithView())
			{
				_ViewResult.ViewData.Model = new ScaffoldedView(GetModel());
				_ViewResult.ViewName = "ScaffoldedView";
			}
		}

		private bool IsViewResult()
		{
			_ViewResult = _FilterContext.Result as ViewResult;
			return _ViewResult != null;
		}

		private bool ViewNameIsAlreadySet()
		{
			return !string.IsNullOrEmpty(_ViewResult.ViewName);
		}

		private bool ModelTypeEndsWithForm()
		{
			return ModelTypeEndsWith("Form");
		}

		private bool ModelTypeEndsWithView()
		{
			return ModelTypeEndsWith("View");
		}

		private bool ModelTypeEndsWith(string ending)
		{
			var model = GetModel();
			return model != null && model.GetType().Name.EndsWith(ending);
		}

		private object GetModel()
		{
			return _ViewResult.ViewData.Model;
		}

		public bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return true;
		}
	}
}