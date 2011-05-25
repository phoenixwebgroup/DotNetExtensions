namespace MvcActionFilers.Filters
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using JoinedFilter;
	using Model;

	public class InjectStatesFilter : ViewModelInjectFilter
	{
		protected override Func<PropertyInfo, bool> InjectProperty()
		{
			return p => p.PropertyType == typeof (IList<State>);
		}

		protected override object WithValue(PropertyInfo propertyInfo)
		{
			return StatesService.GetStates();
		}
	}

	public class OnViewResult_IfNullStatesListInjectAllStates : OnViewResult_ExecuteResultFilter<InjectStatesFilter>
	{
	}
}