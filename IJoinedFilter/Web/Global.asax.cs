namespace MvcActionFilers
{
	using System.Web;
	using System.Web.Routing;
	using Core;
	using Registries;

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			RouteRegistry.RegisterRoutes(RouteTable.Routes);
			ViewEngineRegistry.SetViewEngines();

			if (!WindsorContainerSetup.InitializeContainer())
			{
				return;
			}

			JoinedRegistry.Register(WindsorContainerSetup.Container);
			ControllerRegistry.Register(WindsorContainerSetup.Container);
		}
	}
}