namespace BclExtensionMethods.Email
{
	using System.Collections.Generic;

	/// <summary>
	/// 	Configuration for how email operates, by default, it will be in debug mode
	/// </summary>
	public class EmailConfiguration
	{
		public static EmailConfiguration Configuration { get; set; }

		public List<Emailer> Emailers { get; set; }

		static EmailConfiguration()
		{
			Configuration = Default();
		}

		public EmailConfiguration()
		{
			Emailers = new List<Emailer>();
		}

		public static EmailConfiguration Default()
		{
			var configuration = new EmailConfiguration();
			configuration.SetDebugMode();
			return configuration;
		}

		/// <summary>
		/// 	Call to setup sending emails
		/// </summary>
		public void SetSendMode()
		{
			Emailers = new List<Emailer>
			           	{
			           		new SmtpEmailer()
			           	};
		}

		/// <summary>
		/// 	Call to setup debug mode (writes to disc and log4net)
		/// </summary>
		public void SetDebugMode()
		{
			Emailers = new List<Emailer>
			           	{
			           		new Log4NetEmailer(),
			           		new WriteToDiscEmailer()
			           	};
		}
	}
}