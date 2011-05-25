namespace JoinedFilter.Windsor
{
	using System.Web.Mvc;
	using Castle.Windsor;

	/// <summary>
	/// This is simply a windsor adapter to MasterFilterLocator to avoid list constructor injection.
	/// </summary>
	public class WindsorMasterFilterLocator : IMasterFilterLocator
	{
		private readonly IWindsorContainer _Container;
		private MasterFilterLocator _MaserFilterLocator;

		public WindsorMasterFilterLocator(IWindsorContainer container)
		{
			_Container = container;
			var filterLocators = _Container.ResolveAll<IFilterLocator>();
			_MaserFilterLocator = new MasterFilterLocator(filterLocators);
		}

		public void AddComposedFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor,
		                               FilterInfo filters)
		{
			_MaserFilterLocator.AddComposedFilters(controllerContext, actionDescriptor, filters);
		}
	}
}