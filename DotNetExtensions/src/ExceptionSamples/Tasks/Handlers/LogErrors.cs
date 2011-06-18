namespace DotNetExtensions.Services.Tasks.Handlers
{
	using System.Linq;
	using BclExtensionMethods;
	using log4net;

	/// <summary>
	/// Log errors to log4net
	/// </summary>
	public class LogErrors : ErrorHandler
	{
		private readonly string _LoggerName;

		public LogErrors(string loggerName = "errors")
		{
			_LoggerName = loggerName;
		}

		public override void OnError(ErrorEvent errorEvent)
		{
			var context = GetContextDescription(errorEvent.Context);
			var logger = LogManager.GetLogger(_LoggerName);
			logger.Error("Warning in " + context, errorEvent.Exception);
		}

		/// <summary>
		/// Walk the context chain and get a description of each state in the chain.
		/// </summary>
		public static string GetContextDescription(TaskContext context)
		{
			return context.EnumerateUpTheContextChain()
				.Reverse()
				.Select(c => c.StateDescription())
				.StringJoin("\n in ");
		}
	}
}