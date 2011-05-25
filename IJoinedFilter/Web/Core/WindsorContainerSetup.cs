namespace MvcActionFilers.Core
{
	using Castle.MicroKernel.Registration;
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;

	public class WindsorContainerSetup
	{
		private static object _lock = new object();

		public static IWindsorContainer Container;

		public static bool InitializeContainer()
		{
			lock (_lock)
			{
				if (Container != null)
				{
					return false;
				}
				Container = new WindsorContainer();
				Container.Kernel.Resolver.AddSubResolver(new ListResolver(Container.Kernel));
				Container.Register(
					Component.For<IWindsorContainer>()
						.Instance(Container)
						.LifeStyle.Singleton
					);
			}
			return true;
		}
	}
}