namespace BclExtensionMethods.Email
{
	using System.Linq;
	using System.Net.Mail;
	using log4net;

	/// <summary>
	/// 	log4net emailer
	/// </summary>
	public class Log4NetEmailer : Emailer
	{
		public static ILog Logger = LogManager.GetLogger("Log4NetEmailer");

		public override void Send(MailMessage message)
		{
			Logger.Info(CreateLog(message));
		}

		protected virtual object CreateLog(MailMessage message)
		{
			var log = new
			          	{
			          		Message = "Sending email",
			          		message.Subject,
			          		message.Body,
			          		message.From,
			          		To = message.To.Select(c => c.Address).StringJoin(","),
			          		CC = message.CC.Select(c => c.Address).StringJoin(","),
			          		Bcc = message.Bcc.Select(c => c.Address).StringJoin(","),
			          		NumberOfAttachments = message.Attachments.Count,
			          		Headers = message.Headers.OfType<string>().Select(h => h + ": " + message.Headers[h]).StringJoin(","),
			          		message.DeliveryNotificationOptions,
			          		message.HeadersEncoding,
			          		message.IsBodyHtml,
			          		message.Priority,
			          		ReplyToList = message.ReplyToList.Select(c => c.Address).StringJoin(","),
			          		message.SubjectEncoding
			          	};
			return log;
		}
	}
}