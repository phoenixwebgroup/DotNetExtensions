namespace DotNetExtensions.Services.Tasks
{
	using System;
	using System.Collections.Generic;
	using Handlers;

	public class GlobalTaskContext : TaskContext
	{
		public static List<ErrorHandler> DefaultHandlers = new List<ErrorHandler>();

		public GlobalTaskContext(string globalState) : base(globalState)
		{
		}

		public static void ClearGlobalHanlders()
		{
			DefaultHandlers.Clear();
		}

		public static void RegisterGlobalHandler(Action<ErrorEvent> handler)
		{
			DefaultHandlers.Add(new GenericErrorHandler(handler));
		}	

		public static void RegisterGlobalHandler(ErrorHandler handler)
		{
			DefaultHandlers.Add(handler);
		}	
	}
}