namespace DotNetExtensions.Services.Tasks
{
	using System;
	using System.Reactive.Linq;
	using BclExtensionMethods.Observables;
	using Handlers;

	/// <summary>
	/// Example of a database down throttled exception stream, so only one notification comes through every 10 minutes.
	/// </summary>
	public class DatabaseDownThrottleExample : ErrorStream
	{
		protected override IObservable<ErrorEvent> Filter(IObservable<ErrorEvent> stream)
		{
			return stream
				.Where(DatabaseIsDown)
				.ThrottleAfterFirst(TimeSpan.FromMinutes(10));
		}

		private bool DatabaseIsDown(ErrorEvent s)
		{
			// put real filter here
			return s.Exception.Message.Contains("database down");
		}
	}
}