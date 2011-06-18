namespace DotNetExtensions.Services.Tasks
{
	using System.Runtime.Remoting.Messaging;
	using BclExtensionMethods;

	public class TaskContexts
	{
		private const string CurrentTaskContextKey = "CurrentTaskContextKey";

		public static TaskContext GetCurrentTaskContext()
		{
			return (TaskContext)CallContext.GetData(CurrentTaskContextKey);
		}

		public static void SetExceptionContext(TaskContext context)
		{
			CallContext.SetData(CurrentTaskContextKey, context);
		}

		public static void NotifyParent(ErrorEvent errorEvent)
		{
			var parent = GetParentTaskContext();
			if (parent == null)
			{
				return;
			}
			parent.ChildError(errorEvent);
		}

		public static void NotifyAllParents(ErrorEvent errorEvent)
		{
			var parent = GetParentTaskContext();
			if (parent == null)
			{
				return;
			}
			parent.EnumerateUpTheContextChain()
				.ForEach(p => p.ChildError(errorEvent));
		}

		private static TaskContext GetParentTaskContext()
		{
			var context = GetCurrentTaskContext();
			if (context == null)
			{
				return null;
			}
			return context.Parent;
		}
	}
}