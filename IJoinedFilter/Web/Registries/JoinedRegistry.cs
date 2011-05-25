namespace MvcActionFilers.Registries
{
	using System.Reflection;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using Filters;
	using JoinedFilter;
	using JoinedFilter.Windsor;
	using ReflectedAutoMap;
	using ReflectedAutoMap.JoinedFilter;

	public class JoinedRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			JoinedFilterRegistry.Register(container);

			RegisterJoinedFiltersInExecutingAssembly(container);

			RegisterReflectedAutoMapFilter(container);

			RegisterDependentFilters(container);
		}

		private static void RegisterReflectedAutoMapFilter(IWindsorContainer container)
		{
			ReflectedAutoMapRegistry.Register(container);

			container.Register(
				Component.For<IJoinedFilter>().ImplementedBy<OnViewResult_IfModelDoesNotMatchViewModelThenMapWithAutoMapper>().LifeStyle.Transient);

			container.Register(
				Component.For<ReflectedAutoMapFilter>().ImplementedBy<ReflectedAutoMapFilter>().LifeStyle.Transient);
		}

		private static void RegisterDependentFilters(IWindsorContainer container)
		{
			container.Register(Component.For<HelloWorldFilter>());
			container.Register(Component.For<InjectStatesFilter>());
		}

		private static void RegisterJoinedFiltersInExecutingAssembly(IWindsorContainer container)
		{
			container.Register(
				AllTypes.Of<IJoinedFilter>()
					.FromAssembly(Assembly.GetExecutingAssembly())
					.Configure(c => c.LifeStyle.Transient).WithService.FromInterface(typeof (IJoinedFilter))
				);
		}
	}
}