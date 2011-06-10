namespace mongo4log4net
{
	using System;
	using MongoDB.Bson.Serialization;
	using MongoDB.Bson.Serialization.Conventions;

	public class MongoAppenderInit
	{
		public static Func<MongoAppenderInit> Provider { get; set; }

		static MongoAppenderInit()
		{
			Provider = () => new MongoAppenderInit();
		}

		public virtual void Init()
		{
			var profile = new ConventionProfile();
			profile.SetMemberFinderConvention(new LoggingMemberFinderConvention());
			BsonClassMap.RegisterClassMap(new ExceptionMap());
			BsonClassMap.RegisterClassMap(new LocationInformationMap());
			BsonClassMap.RegisterConventions(profile, t => true);
		}
	}
}