namespace mongo4log4net
{
	using System;
	using log4net.Core;
	using log4net.Util;

	public class LogEntry
	{
		protected DateTime TimeStamp { get; set; }
		public object Message { get; set; }
		public object Exception { get; set; }
		protected string LoggerName { get; set; }
		protected string Domain { get; set; }
		protected string Identity { get; set; }
		protected Level Level { get; set; }
		protected LocationInfo LocationInformation { get; set; }
		protected FixFlags Fix { get; set; }
		protected PropertiesDictionary Properties { get; set; }
		protected string UserName { get; set; }
		protected string ThreadName { get; set; }

		public LogEntry(LoggingEvent logEvent)
		{
			LoggerName = logEvent.LoggerName;
			Domain = logEvent.Domain;
			Identity = logEvent.Identity;
			Level = logEvent.Level;
			LocationInformation = logEvent.LocationInformation;
			Fix = logEvent.Fix;
			Properties = logEvent.Properties;
			ThreadName = logEvent.ThreadName;
			UserName = logEvent.UserName;
			Message = logEvent.MessageObject;
			TimeStamp = logEvent.TimeStamp;
			Exception = logEvent.ExceptionObject;
		}
	}
}