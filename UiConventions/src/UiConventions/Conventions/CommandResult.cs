namespace HtmlTags.UI.Conventions
{
	using System.Web.Mvc;

	public class CommandResult : ActionResult
	{
		private readonly string _Message;

		public CommandResult(string message)
		{
			_Message = message;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Write(_Message);
		}

		public static implicit operator CommandResult(string message)
		{
			return new CommandResult(message);
		}
	}
}