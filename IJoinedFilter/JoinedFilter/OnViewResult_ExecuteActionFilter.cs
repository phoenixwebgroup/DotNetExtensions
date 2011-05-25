namespace JoinedFilter
{
	using System.Web.Mvc;

	public class OnViewResult_ExecuteActionFilter<T> : JoinToActionFilter<T>
		where T : IActionFilter
	{
		public override bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return ResultCouldBe.ViewResult(actionDescriptor);
		}
	}
}