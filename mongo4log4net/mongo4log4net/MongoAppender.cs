namespace mongo4log4net
{
	using System;
	using System.Net.Sockets;
	using MongoDB.Driver;
	using log4net.Appender;
	using log4net.Core;

	public class MongoAppender : AppenderSkeleton
	{
		private MongoServer _Mongo;
		private readonly object _MongoLock = new object();

		private MongoServer GetMongo()
		{
			if (_Mongo == null)
			{
				lock (_MongoLock)
				{
					if (_Mongo == null)
					{
						_Mongo = MongoServer.Create(MongoConnectionString);
						MongoAppenderInit.Provider().Init();
					}
				}
			}
			return _Mongo;
		}

		public string MongoConnectionString { get; set; }

		public string DatabaseName { get; set; }

		public string CollectionName { get; set; }

		public override void ActivateOptions()
		{
			// Is there a way to stop this appender from logging subsequent messages, how does log4net avoid using misconfigured appenders, or do I have to do that in here?
			try
			{
				GetMongo();
			}
			catch (SocketException exception)
			{
				var message = string.Format("Cannot connect to mongo server {0} for appender {1}", MongoConnectionString, Name);
				throw new Exception(message, exception);
			}
		}

		protected override void Append(LoggingEvent loggingEvent)
		{
			var log = new LogEntry(loggingEvent);
			InsertLogEntry(log);
		}

		private void InsertLogEntry(LogEntry log)
		{
			var logs = GetLogCollection();
			logs.Insert(log);
		}

		private MongoCollection<LogEntry> GetLogCollection()
		{
			var collectionName = string.IsNullOrEmpty(CollectionName) ? "entries" : CollectionName;
			var databaseName = string.IsNullOrEmpty(DatabaseName) ? "logs" : DatabaseName;

			return GetMongo()
				.GetDatabase(databaseName)
				.GetCollection<LogEntry>(collectionName);
		}

		protected override void OnClose()
		{
			if (_Mongo != null)
			{
				lock (_MongoLock)
				{
					if (_Mongo != null)
					{
						_Mongo.Disconnect();
						_Mongo = null;
					}
				}
			}
		}
	}
}