namespace MvcActionFilers.Registries
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using ReflectedAutoMap;
	using ReflectedAutoMap.Mapping;

	public class ReflectedAutoMapRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			container.Register(
				Component.For<IReflectedAutoMapper>().ImplementedBy<ReflectedAutoMapper>().LifeStyle.Transient);
			container.Register(
				Component.For<IViewModelTypeReflector>().ImplementedBy<ViewModelTypeReflector>().LifeStyle.Transient);
			container.Register(
				Component.For<IMapper>().ImplementedBy<AutoMapperObjectMapper>().LifeStyle.Transient);
		}
	}
}