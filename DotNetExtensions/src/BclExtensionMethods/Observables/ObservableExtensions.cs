namespace BclExtensionMethods.Observables
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reactive;
	using System.Reactive.Concurrency;
	using System.Reactive.Disposables;
	using System.Reactive.Linq;

	public static class ObservableExtensions
	{
		/// <summary>
		///     Throttle a stream of events, only taking the first item after which the timespan will apply to squelch any
		///     subsequent events.
		///     At time A, event T happens, any events from A to (A + delay) will be ignored, upon the next event after (A + delay)
		///     the same rule will apply to the first event T.
		/// </summary>
		public static IObservable<T> ThrottleAfterFirst<T>(this IObservable<T> source, TimeSpan delay)
		{
			return source
				.Select(item => new {TimeStamp = DateTime.Now, Item = item})
				.Scan((last, current) => current.TimeStamp.Subtract(last.TimeStamp) >= delay ? current : last)
				.Select(s => s.Item)
				.DistinctUntilChanged();
		}

		/// <summary>
		///     Throttle a stream of notifications, only taking the last that occurs during a subscription. 
		///		Extension from Lee Campbell on MSDN.
		/// </summary>
		public static IObservable<T> ObserveLatestOn<T>(this IObservable<T> source, IScheduler scheduler)
		{
			return Observable.Create<T>(observer =>
			                            {
				                            Notification<T> outsideNotification = null;
				                            var gate = new object();
				                            var active = false;
				                            var cancelable = new MultipleAssignmentDisposable();
				                            var disposable = source.Materialize()
					                            .Subscribe(thisNotification =>
					                                       {
						                                       bool alreadyActive;
						                                       lock (gate)
						                                       {
							                                       alreadyActive = active;
							                                       active = true;
							                                       outsideNotification = thisNotification;
						                                       }

						                                       if (!alreadyActive)
						                                       {
							                                       cancelable.Disposable = scheduler
								                                       .Schedule(self =>
								                                                 {
									                                                 Notification<T> localNotification = null;
									                                                 lock (gate)
									                                                 {
										                                                 localNotification = outsideNotification;
										                                                 outsideNotification = null;
									                                                 }
									                                                 localNotification.Accept(observer);
									                                                 var hasPendingNotification = false;
									                                                 lock (gate)
									                                                 {
										                                                 hasPendingNotification = active = (outsideNotification != null);
									                                                 }
									                                                 if (hasPendingNotification)
									                                                 {
										                                                 self();
									                                                 }
								                                                 });
						                                       }
					                                       });
				                            return new CompositeDisposable(disposable, cancelable);
			                            });
		}

		/// <summary>
		///     Returns an observable that pulses every hour (24 hour format, 0 to 23) The pulse occurs within the first 15 minutes
		///     of the hour.
		/// </summary>
		/// <returns></returns>
		public static IObservable<int> CreateHourPulse()
		{
			return Observable
				.Interval(TimeSpan.FromMinutes(15))
				.Select(i => DateTime.Now.Hour)
				.DistinctUntilChanged();
		}

		/// <summary>
		///     Returns an observable that pulses every hour (24 hour format, 0 to 23) with the specified resolution.
		///     The pulse occurs within the amount of time specified by the resolution (in minutes.)
		///     Higher resolution (i.e. lower specified number of minutes) may affect execution speed.
		/// </summary>
		/// <returns></returns>
		public static IObservable<int> CreateHourPulse(int resolution)
		{
			return Observable
				.Interval(TimeSpan.FromMinutes(resolution.SetWithinRange(1, 59)))
				.Select(i => DateTime.Now.Hour)
				.DistinctUntilChanged();
		}

		/// <summary>
		///     Returns an observable that pulses every minute. The pulse occurs within the first 15 seconds of the minute.
		/// </summary>
		/// <returns></returns>
		public static IObservable<int> CreateMinutePulse()
		{
			return Observable
				.Interval(TimeSpan.FromSeconds(15))
				.Select(i => DateTime.Now.Minute)
				.DistinctUntilChanged();
		}

		/// <summary>
		///     Returns an observable that pulses every minute with the specified resolution.
		///     The pulse occurs within the amount of time specified by the resolution (in seconds.)
		///     Higher resolution (i.e. lower specified number of seconds) may affect execution speed.
		/// </summary>
		/// <returns></returns>
		public static IObservable<int> CreateMinutePulse(int resolution)
		{
			return Observable
				.Interval(TimeSpan.FromSeconds(resolution.SetWithinRange(1, 59)))
				.Select(i => DateTime.Now.Minute)
				.DistinctUntilChanged();
		}

		/// <summary>
		/// 	Returns an observable that pulses every minute. The pulse occurs within the first 15 seconds of the minute.
		/// </summary>
		/// <returns> </returns>
		public static IObservable<DateTime> CreateUtcMinutePulse()
		{
			return Observable
				.Interval(TimeSpan.FromSeconds(15))
				.Select(i => DateTime.UtcNow)
				.DistinctUntilChanged(t => new { t.Hour, t.Minute });
		}

		public static IObservable<DateTime> OnDaysOfWeek(this IObservable<DateTime> source, IEnumerable<DayOfWeek> daysOfWeek)
		{
			return source.Where(d => daysOfWeek.Contains(d.DayOfWeek));
		}

		public static int SetWithinRange(this int value, int lowerBound, int upperBound)
		{
			if (value < lowerBound) value = lowerBound;
			if (value > upperBound) value = upperBound;
			return value;
		}

		public static IObservable<DateTime> On(IEnumerable<DayOfWeek> days, int hour, int minute, TimeZoneInfo timeZone)
		{
			return CreateMinutePulse()
				.Select(x => TimeZoneInfo.ConvertTime(DateTime.Now, timeZone))
				.Where(a => a.Hour == hour && a.Minute == minute)
				.OnDaysOfWeek(days);
		}
	}
}