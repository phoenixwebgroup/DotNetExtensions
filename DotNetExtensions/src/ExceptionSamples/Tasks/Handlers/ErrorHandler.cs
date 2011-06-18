namespace DotNetExtensions.Services.Tasks.Handlers
{
	public abstract class ErrorHandler
	{
		public abstract void OnError(ErrorEvent errorEvent);
	}
}