namespace DotNetExtensions.Services.Tasks
{
	using Handlers;

	public static class TaskContextExtensions
	{
		public static TaskContext LogErrors(this TaskContext context, string loggerName = "TaskContexts")
		{
			return context
				.OnError(new LogErrors(loggerName));
		}

		public static TaskContext NotifyParentsOfErrors(this TaskContext context)
		{
			return context
				.OnError(TaskContexts.NotifyParent);
		}

		public static TaskContext NotifyAllParentsOfErrors(this TaskContext context)
		{
			return context
				.OnError(TaskContexts.NotifyAllParents);
		}
	}
}