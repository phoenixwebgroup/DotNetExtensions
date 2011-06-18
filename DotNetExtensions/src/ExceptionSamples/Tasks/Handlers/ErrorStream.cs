namespace DotNetExtensions.Services.Tasks.Handlers
{
	using System;
	using System.Reactive.Subjects;

	/// <summary>
	/// A template for creating error streams as an error handler
	/// </summary>
	public abstract class ErrorStream : ErrorHandler
	{
		private readonly ISubject<ErrorEvent> _Errors = new Subject<ErrorEvent>();

		public virtual IObservable<ErrorEvent> Errors
		{
			get { return Filter(_Errors); }
		}

		protected abstract IObservable<ErrorEvent> Filter(IObservable<ErrorEvent> stream);

		public override void OnError(ErrorEvent errorEvent)
		{
			_Errors.OnNext(errorEvent);
		}
	}
}