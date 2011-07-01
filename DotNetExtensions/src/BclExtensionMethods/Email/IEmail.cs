namespace BclExtensionMethods.Email
{
	using System.Net.Mail;

	public interface IEmail
	{
		void Send(MailMessage message);
	}
}