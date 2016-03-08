namespace BclExtensionMethods.Scheduling.Simple
{
	using System;
	using System.Reactive.Concurrency;
	using System.Reactive.Linq;
	using System.Reactive.Subjects;
	using System.Threading.Tasks;
	using Exceptions;
	using Observables;

	public class LastInQueue
	{
		private Subject<Task> _ScheduledTasks = new Subject<Task>();
		private IDisposable _Runner;

		public void AddTask(Task task)
		{
			Task.Factory.StartNew((() => _ScheduledTasks.OnNext(task)));
		}

		public void Start()
		{
			DisposableExtesions.TryDispose(_Runner);
			_Runner = (_ScheduledTasks)
				.Synchronize()
				.ObserveLatestOn(new NewThreadScheduler())
				.Subscribe(t => OnException.Continue(t.RunSynchronously));
		}

		public void Stop()
		{
			DisposableExtesions.TryDispose(_Runner);
		}
	}
}