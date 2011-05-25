namespace MvcActionFilers.Filters
{
	using System;
	using System.Web.Mvc;
	using AutoMapper;

	/// <summary>
	/// Copied from http://www.lostechies.com/blogs/jimmy_bogard/archive/2009/06/29/how-we-do-mvc-view-models.aspx
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AutoMapAttribute : ActionFilterAttribute
	{
		private readonly Type _destType;
		private readonly Type _sourceType;

		public AutoMapAttribute(Type sourceType, Type destType)
		{
			_sourceType = sourceType;
			_destType = destType;
		}

		public Type SourceType
		{
			get { return _sourceType; }
		}

		public Type DestType
		{
			get { return _destType; }
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var filter = new AutoMapFilter(SourceType, DestType);

			filter.OnActionExecuted(filterContext);
		}
	}

	/// <summary>
	/// Copied from http://www.lostechies.com/blogs/jimmy_bogard/archive/2009/06/29/how-we-do-mvc-view-models.aspx
	/// </summary>
	public class AutoMapFilter : IActionFilter
	{
		private readonly Type _destType;
		private readonly Type _sourceType;

		public AutoMapFilter(Type sourceType, Type destType)
		{
			_sourceType = sourceType;
			_destType = destType;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var model = filterContext.Controller.ViewData.Model;
			object viewModel = Mapper.Map(model, _sourceType, _destType);

			filterContext.Controller.ViewData.Model = viewModel;
		}
	}
}