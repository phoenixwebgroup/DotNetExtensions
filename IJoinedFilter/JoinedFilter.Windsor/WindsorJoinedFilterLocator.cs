namespace JoinedFilter.Windsor
{
	using System.Web.Mvc;
	using Castle.Windsor;

	/// <summary>
	/// Use this if you don't want to use list resolution with windsor and the default JoinedFilterLocator.  This is just an adapter to JoinedFilterLocator to use windsor's resolve all to find IJoinedFilter(s).
	/// </summary>
	public class WindsorJoinedFilterLocator : IFilterLocator
	{
		private readonly IWindsorContainer _Container;

		public WindsorJoinedFilterLocator(IWindsorContainer container)
		{
			_Container = container;
		}

		public FilterInfo FindFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var joinedFilters = _Container.ResolveAll<IJoinedFilter>();
			var locator = new JoinedFilterLocator(joinedFilters);
			return locator.FindFilters(controllerContext, actionDescriptor);
		}
	}
}