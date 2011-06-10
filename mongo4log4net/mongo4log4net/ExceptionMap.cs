namespace mongo4log4net
{
	using System;
	using MongoDB.Bson.Serialization;

	public class ExceptionMap : BsonClassMap<Exception>
	{
		public ExceptionMap()
		{
			MapProperty(e => e.Message);
			MapProperty(e => e.InnerException);
			MapProperty(e => e.HelpLink);
			MapProperty(e => e.Source);
			MapProperty(e => e.StackTrace);
		}
	}
}