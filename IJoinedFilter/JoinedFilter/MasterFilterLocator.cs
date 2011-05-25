namespace JoinedFilter
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	public class MasterFilterLocator : IMasterFilterLocator
	{
		public IList<IFilterLocator> FilterLocators { get; set; }

		public MasterFilterLocator(IList<IFilterLocator> filterLocators)
		{
			if(filterLocators == null || filterLocators.Count == 0)
			{
				throw new ArgumentException("No filter locators provided, please register at least one IFilterLocator.  It is possible that one of your IFilterLocator has a dependency that cannot be satisified and might not be able to be created, check your container registrations and make sure that all dependencies are satisfied for your registered IFilterLocator(s).");
			}
			FilterLocators = filterLocators;
		}

		public virtual void AddComposedFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor,
		                               FilterInfo filters)
		{
			var foundFilters = FilterLocators.Select(f => f.FindFilters(controllerContext, actionDescriptor));

			foundFilters.ForEach(f => AddFilters(filters, f));
		}

		protected void AddFilters(FilterInfo filters, FilterInfo mergeFilters)
		{
			mergeFilters.ActionFilters.ForEach(filters.ActionFilters.Add);
			mergeFilters.ExceptionFilters.ForEach(filters.ExceptionFilters.Add);
			mergeFilters.AuthorizationFilters.ForEach(filters.AuthorizationFilters.Add);
			mergeFilters.ResultFilters.ForEach(filters.ResultFilters.Add);
		}
	}
}