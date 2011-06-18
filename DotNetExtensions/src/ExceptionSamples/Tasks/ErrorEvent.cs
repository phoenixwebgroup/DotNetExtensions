namespace DotNetExtensions.Services.Tasks
{
	using System;

	public class ErrorEvent
	{
		public readonly TaskContext Context;
		public readonly Exception Exception;

		public ErrorEvent(TaskContext context, Exception exception)
		{
			Context = context;
			Exception = exception;
		}
	}
}