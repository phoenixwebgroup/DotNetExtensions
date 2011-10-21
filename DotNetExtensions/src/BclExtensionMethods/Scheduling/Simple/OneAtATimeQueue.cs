namespace BclExtensionMethods.Scheduling.Simple
{
	using System;
	using System.Reactive.Linq;
	using System.Reactive.Subjects;
	using System.Threading.Tasks;
	using Exceptions;

	/// <summary>
	/// 	All tasks will be pushed through a single queue and run one at a time from start to finish, synchronously.
	/// </summary>
	public class OneAtATimeQueue
	{
		private readonly Subject<Task> _ScheduledTasks = new Subject<Task>();

		public OneAtATimeQueue()
		{
			// Synchronize forces a buffer so that only one event is published to the Observer at a time, and the Observer runs the task synchrnously from start to finish.
			_Runner = _ScheduledTasks
				.Synchronize()
				.Subscribe(t => OnException.Continue(t.RunSynchronously));
		}

		/// <summary>
		/// 	Add a task, not yet started, to be run
		/// </summary>
		public void AddTask(Task task)
		{
			_ScheduledTasks.OnNext(task);
		}

		private IDisposable _Runner;
	}
}