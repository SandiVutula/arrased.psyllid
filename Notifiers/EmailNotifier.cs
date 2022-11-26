using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using arrased.psyllid.Notifiers;
using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;

namespace Nml.Refactor.Me.Notifiers
{
	public class EmailNotifier : Notifier, INotifier
	{
		private readonly IMailMessageBuilder _messageBuilder;
		private readonly IOptions _options;
		private readonly ILogger _logger = LogManager.For<EmailNotifier>();

        public EmailNotifier(IWebhookMessageBuilder messageBuilder, IOptions options) : base(messageBuilder, options) { }

        public override async Task Notify(NotificationMessage message)
		{
			var smtp = new SmtpClient(_options.Email.SmtpServer);
			smtp.Credentials = new NetworkCredential(_options.Email.UserName, _options.Email.Password);
			var mailMessage = _messageBuilder.CreateMessage(message);
			
			try
			{
				await smtp.SendMailAsync(mailMessage);
				_logger.LogTrace($"Message sent.");
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Failed to send message. {e.Message}");
				throw;
			}
		}
	}
}
