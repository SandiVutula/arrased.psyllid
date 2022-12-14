using System;
using System.Net.Mail;
using System.Threading.Tasks;
using arrased.psyllid.Notifiers;
using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;

namespace Nml.Refactor.Me.Notifiers
{
	public class SmsNotifier : INotifier
	{
		private readonly IStringMessageBuilder _messageBuilder;
		private readonly IOptions _options;
		private readonly ILogger _logger = LogManager.For<SmsNotifier>();

		public SmsNotifier(IStringMessageBuilder messageBuilder, IOptions options)
		{
			_messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}
		
		public async Task Notify(NotificationMessage message)
		{
			//Complete after refactoring inheritance. Use "SmsApiClient"
			var mobileNumber = " ";
            var smsApiClient = new SmsApiClient(_options.Sms.ApiUri, _options.Sms.ApiKey);
			var smsMessageBuilder = new SmsMessageBuilder();
            var smsMessage = smsMessageBuilder.CreateMessage(message);

            try
            {
                await smsApiClient.SendAsync(mobileNumber, smsMessage);
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
