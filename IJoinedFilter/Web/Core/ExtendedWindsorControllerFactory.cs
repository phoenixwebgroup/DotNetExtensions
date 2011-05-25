namespace MvcActionFilers.Core
{
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Castle.Windsor;

	public class ExtendedWindsorControllerFactory : WindsorControllerFactory
	{
		public ExtendedWindsorControllerFactory(IWindsorContainer container) : base(container)
		{
			Container = container;
		}

		protected IWindsorContainer Container { get; set; }

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			var controller = base.GetControllerInstance(requestContext, controllerType) as Controller;

			if (Container.Kernel.HasComponent(typeof (IActionInvoker)))
			{
				controller.ActionInvoker = Container.Resolve<IActionInvoker>();
			}

			return controller;
		}
	}
}