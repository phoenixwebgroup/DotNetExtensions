namespace BclExtensionMethods.Email
{
	using System.Net.Mail;

	/// <summary>
	/// 	Class to send email, with an interface for stubbing as necessary
	/// </summary>
	public class Email : IEmail
	{
		public static void SendEmail(MailMessage message)
		{
			new Email().Send(message);
		}

		public void Send(MailMessage message)
		{
			EmailConfiguration.Configuration.Emailers
				.ForEach(e => e.Send(message));
		}
	}
}