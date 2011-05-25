namespace MvcActionFilers.Registries
{
	using System.Reflection;
	using System.Web.Mvc;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using Core;

	public class ControllerRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			RegisterControllers(container);
			RegisterControllerFactory(container);
		}

		private static void RegisterControllerFactory(IWindsorContainer container)
		{
			container.Register(Component
			                   	.For<IControllerFactory>()
			                   	.ImplementedBy<ExtendedWindsorControllerFactory>()
			                   	.LifeStyle.Transient);

			var factory = container.Resolve<IControllerFactory>();

			ControllerBuilder.Current.SetControllerFactory(factory);
		}

		private static void RegisterControllers(IWindsorContainer container)
		{
			container.Register(
				AllTypes.Of<IController>()
					.FromAssembly(Assembly.GetExecutingAssembly())
					.Configure(c => c.LifeStyle.Transient)
				);
		}
	}
}