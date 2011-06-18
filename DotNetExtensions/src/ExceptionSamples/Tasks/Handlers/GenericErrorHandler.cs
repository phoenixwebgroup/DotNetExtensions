namespace DotNetExtensions.Services.Tasks.Handlers
{
	using System;

	/// <summary>
	/// A class only meant to wrap an action as an error handler, mostly meant to pass lambdas as error handlers
	/// </summary>
	internal class GenericErrorHandler : ErrorHandler
	{
		private readonly Action<ErrorEvent> _Handler;

		public GenericErrorHandler(Action<ErrorEvent> handler)
		{
			_Handler = handler;
		}

		public override void OnError(ErrorEvent errorEvent)
		{
			_Handler(errorEvent);
		}
	}
}