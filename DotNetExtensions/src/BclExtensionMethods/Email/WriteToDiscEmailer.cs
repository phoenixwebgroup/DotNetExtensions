namespace BclExtensionMethods.Email
{
	using System;
	using System.IO;
	using System.Net.Mail;
	using System.Reflection;

	/// <summary>
	/// 	Writing emails to disc in eml format, can be used to write to an IIS pickup directory too
	/// </summary>
	public class WriteToDiscEmailer : Emailer
	{
		/// <summary>
		/// 	Directory to write emails to
		/// </summary>
		public static string EmailDirectory;

		static WriteToDiscEmailer()
		{
			var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
			EmailDirectory = Path.Combine(path, @"DebugEmails");
		}

		public override void Send(MailMessage message)
		{
			var directory = Path.GetFullPath(EmailDirectory);
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			var client = new SmtpClient("discemailer")
			             	{
			             		DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
			             		PickupDirectoryLocation = directory
			             	};
			client.Send(message);
		}
	}
}