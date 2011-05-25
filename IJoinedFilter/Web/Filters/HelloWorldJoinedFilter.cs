namespace MvcActionFilers.Filters
{
	using System.Web.Mvc;
	using Controllers;
	using JoinedFilter;

	public class HelloWorldJoinedFilter : JoinToActionFilter<HelloWorldFilter>
	{
		public override bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return actionDescriptor.ActionName == "World" && controllerContext.Controller is HomeController;
		}
	}
}