namespace JoinedFilter
{
	using System.Web.Mvc;

	public class OnViewResult_ExecuteResultFilter<T> : JoinToResultFilter<T>
		where T : IResultFilter
	{
		public override bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return ResultCouldBe.ViewResult(actionDescriptor);
		}
	}
}