namespace BclExtensionMethods.Observables
{
	using System;
	using System.Reactive.Linq;

	public static class ObservableExtensions
	{
		/// <summary>
		/// Throttle a stream of events, only taking the first item after which the timespan will apply to squelch any subsequent events.
		/// At time A, event T happens, any events from A to (A + delay) will be ignored, upon the next event after (A + delay) the same rule will apply to the first event T.
		/// </summary>
		public static IObservable<T> ThrottleAfterFirst<T>(this IObservable<T> source, TimeSpan delay)
		{
			return source
				.Select(item => new { TimeStamp = DateTime.Now, Item = item })
				.Scan((last, current) => current.TimeStamp.Subtract(last.TimeStamp) >= delay ? current : last)
				.Select(s => s.Item)
				.DistinctUntilChanged();
		} 
	}
}