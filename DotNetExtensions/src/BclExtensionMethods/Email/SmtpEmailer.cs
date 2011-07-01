namespace BclExtensionMethods.Email
{
	using System.Net.Mail;

	/// <summary>
	/// 	Send emails using SmtpClient
	/// </summary>
	public class SmtpEmailer : Emailer
	{
		public override void Send(MailMessage message)
		{
			var client = new SmtpClient();
			client.Send(message);
		}
	}
}