namespace MvcActionFilers.Filters
{
	using System;
	using System.Web.Mvc;
	using JoinedFilter;

	public abstract class ExceptionHandler<T> : IExceptionFilter, IJoinedFilter, IFilterPriority
		where T : Exception
	{
		public void OnException(ExceptionContext filterContext)
		{
			var exception = filterContext.Exception as T;
			if (exception == null || filterContext.ExceptionHandled)
			{
				return;
			}

			filterContext.Result = ErrorView();
			filterContext.ExceptionHandled = true;
		}

		private ViewResult ErrorView()
		{
			var result = new ViewResult
			             {
			             	ViewName = "Error"
			             };
			result.ViewData["Message"] = string.Format("{0} exception handler caught this", typeof (T));
			return result;
		}

		public bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return true;
		}

		public abstract int GetOrder();
	}
}