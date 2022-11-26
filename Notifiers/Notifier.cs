using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;
using Nml.Refactor.Me.Notifiers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace arrased.psyllid.Notifiers
{
    public abstract class Notifier
    {
        private readonly IWebhookMessageBuilder _messageBuilder;
        private readonly IOptions _options;
        private readonly ILogger _logger = LogManager.For<TeamsNotifier>();

        public Notifier(IWebhookMessageBuilder messageBuilder, IOptions options)
        {
            _messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        public virtual async Task Notify(NotificationMessage message)
        {
            var serviceEndPoint = new Uri(_options.Teams.WebhookUri);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, serviceEndPoint);
            request.Content = new StringContent(_messageBuilder.CreateMessage(message).ToString(), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            _logger.LogTrace($"Message sent. {response.StatusCode} -> {response.Content}");

        }
    }
}
