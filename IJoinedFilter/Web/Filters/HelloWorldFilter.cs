namespace MvcActionFilers.Filters
{
	using System.Web.Mvc;

	public class HelloWorldFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var result = new JsonResult
			             {
			             	Data = new
			             	       {
			             	       	message = "Hello World!"
			             	       }
			             };

			filterContext.Result = result;
		}
	}

	/// This is an example how how we would have to do this the old way, only used to create my blog post.
	//public class HelloWorldFilter : ActionFilterAttribute
	//{
	//    public override void OnActionExecuted(ActionExecutedContext filterContext)
	//    {
	//        var result = new JsonResult
	//        {
	//            Data = new
	//            {
	//                message = "Hello World!"
	//            }
	//        };

	//        filterContext.Result = result;
	//    }
	//}
}