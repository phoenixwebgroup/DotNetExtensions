namespace JoinedFilter
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Web.Mvc;

	public abstract class ViewModelInjectFilter : IResultFilter, IFilterPriority
	{
		public void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}

		public void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var viewResult = filterContext.Result as ViewResultBase;
			if (viewResult == null || viewResult.ViewData.Model == null)
			{
				return;
			}
			var model = viewResult.ViewData.Model;
			var property = model.GetType().GetProperties().FirstOrDefault(InjectProperty());
			if (property == null)
			{
				return;
			}
			var value = property.GetValue(model, null);
			if (value == null)
			{
				property.SetValue(model, WithValue(property), null);
			}
		}

		/// <summary>
		/// The criteria to use to find the property to inject.0
		/// </summary>
		/// <returns></returns>
		protected abstract Func<PropertyInfo, bool> InjectProperty();

		/// <summary>
		/// The value to inject, only queried if a matching, null property is found.
		/// </summary>
		/// <returns></returns>
		protected abstract object WithValue(PropertyInfo property);

		public virtual int GetOrder()
		{
			return Int32.MaxValue;
		}
	}
}