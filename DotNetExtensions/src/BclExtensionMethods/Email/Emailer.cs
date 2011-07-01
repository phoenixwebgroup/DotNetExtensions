namespace BclExtensionMethods.Email
{
	using System.Net.Mail;

	/// <summary>
	/// 	An emailer (endpoint) to send emails, implement new emailers using this class and add as desired to the email configuration.
	/// </summary>
	public abstract class Emailer
	{
		public abstract void Send(MailMessage message);
	}
}