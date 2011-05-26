namespace BclExtensionMethods.Exceptions
{
	using System;
	using log4net;

	public class OnException
	{
		public static void Continue(Action action)
		{
			try
			{
				action();
			}
			catch (Exception exception)
			{
				var logger = LogManager.GetLogger(typeof(OnException).FullName);
				logger.Error("Error executing action", exception);
			}
		}
	}
}