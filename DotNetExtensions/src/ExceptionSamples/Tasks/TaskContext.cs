namespace DotNetExtensions.Services.Tasks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Handlers;

	public class TaskContext
	{
		public TaskContext Parent;

		/// <summary>
		/// All child errors that have occured in the operation of the task thus far.
		/// </summary>
		public readonly List<ErrorEvent> ChildErrors = new List<ErrorEvent>();
		protected readonly List<ErrorHandler> ChildErrorHandlers = new List<ErrorHandler>();
		protected readonly List<ErrorHandler> ErrorHandlers = new List<ErrorHandler>();

		public object State { get; set; }

		public TaskContext(object state)
		{
			State = state;
			Parent = TaskContexts.GetCurrentTaskContext();
		}

		public Task Start(Action action)
		{
			var task = CreateTask(action);
			task.Start();
			return task;
		}

		public Task CreateTask(Action action)
		{
			var task = new Task(() => ActionWithContext(action));
			return task;
		}

		private void ActionWithContext(Action action)
		{
			TaskContexts.SetExceptionContext(this);
			try
			{
				action();
			}
			catch (Exception exception)
			{
				var handlers = GetErrorHandlers();
				if (!handlers.Any())
				{
					throw;
				}
				Error(exception);
			}
		}

		/// <summary>
		/// Notify the context of an error
		/// </summary>
		public void Error(Exception exception)
		{
			var handlers = GetErrorHandlers();
			handlers.ForEach(h => h.OnError(new ErrorEvent(this, exception)));
		}

		private List<ErrorHandler> GetErrorHandlers()
		{
			var handlers = ErrorHandlers
				.Union(GlobalTaskContext.DefaultHandlers)
				.ToList();
			return handlers;
		}

		/// <summary>
		/// Notify the context of a child error
		/// </summary>
		public void ChildError(ErrorEvent childError)
		{
			ChildErrors.Add(childError);
			ChildErrorHandlers
				.ForEach(n => n.OnError(childError));
		}

		/// <summary>
		/// Add a child error handler, when a child task forwards errors to the parent, this method is invoked
		/// </summary>
		public TaskContext OnChildError(Action<ErrorEvent> handler)
		{
			return OnChildError(new GenericErrorHandler(handler));
		}

		/// <summary>
		/// Add a child error handler, when a child task forwards errors to the parent, this method is invoked
		/// </summary>
		public TaskContext OnChildError(ErrorHandler handler)
		{
			ChildErrorHandlers.Add(handler);
			return this;
		}

		/// <summary>
		/// Add an error handler
		/// </summary>
		public TaskContext OnError(Action<ErrorEvent> handler)
		{
			ErrorHandlers.Add(new GenericErrorHandler(handler));
			return this;
		}

		/// <summary>
		/// Add an error handler
		/// </summary>
		public TaskContext OnError(ErrorHandler handler)
		{
			ErrorHandlers.Add(handler);
			return this;
		}

		/// <summary>
		/// Returns an enumerable context chain from current context up to the topmost parent context.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TaskContext> EnumerateUpTheContextChain()
		{
			yield return this;
			if (Parent == null || Parent == this)
			{
				yield break;
			}
			foreach (var context in Parent.EnumerateUpTheContextChain())
			{
				yield return context;
			}
		}

		/// <summary>
		/// Serializes the state of the context to a string
		/// </summary>
		public string StateDescription()
		{
			return State == null ? string.Empty : State.ToString();
		}
	}
}