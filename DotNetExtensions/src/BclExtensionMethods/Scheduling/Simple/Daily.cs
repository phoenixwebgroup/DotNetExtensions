namespace BclExtensionMethods.Scheduling.Simple
{
	using System;
	using System.Linq;
	using System.Reactive.Linq;

	/// <summary>
	/// 	Execute a task daily at the specified time(s) of day
	/// </summary>
	public class Daily
	{
		private readonly TimeSpan[] _TimesOfDay;
		private readonly Action _Action;
		private IDisposable _Timer;

		public Daily(Action action, params TimeSpan[] timesOfDay)
		{
			_Action = action;
			_TimesOfDay = timesOfDay;
		}

		public void Execute()
		{
			_Action();
		}

		public void Stop()
		{
			if (_Timer == null)
			{
				return;
			}
			_Timer.Dispose();
		}

		public void Start()
		{
			Stop();
			_Timer = Observable
				.Interval(TimeSpan.FromSeconds(10))
				.Select(i => DateTimeProvider.Now())
				.Where(IsTimeToExecute)
				.Select(currentTime => new {currentTime.Date, currentTime.Hour, currentTime.Minute})
				.DistinctUntilChanged()
				.Subscribe(d => _Action());
		}

		private bool IsTimeToExecute(DateTime currentTime)
		{
			return _TimesOfDay.Any(t => t.Hours == currentTime.Hour && t.Minutes == currentTime.Minute);
		}
	}
}