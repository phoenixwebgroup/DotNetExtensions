namespace JoinedFilter
{
	using System;
	using System.Web.Mvc;

	public abstract class JoinToResultFilter<T> : IResultFilter, IJoinedFilter, IFilterPriority
		where T : IResultFilter
	{
		private T _Filter;

		public JoinToResultFilter()
		{
		}

		public JoinToResultFilter(T filter)
		{
			_Filter = filter;
		}

		public virtual T Filter
		{
			get { return _Filter; }
			set { _Filter = value; }
		}

		public virtual void OnResultExecuting(ResultExecutingContext filterContext)
		{
			EnsureFilterSet();
			_Filter.OnResultExecuting(filterContext);
		}

		private void EnsureFilterSet()
		{
			if (_Filter == null)
			{
				throw new NullReferenceException(string.Format("ResultFilter not injected, expecting type {0}", typeof(T)));
			}
		}

		public virtual void OnResultExecuted(ResultExecutedContext filterContext)
		{
			EnsureFilterSet();
			_Filter.OnResultExecuted(filterContext);
		}

		public abstract bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor);

		public virtual int GetOrder()
		{
			return Int32.MaxValue;
		}
	}
}