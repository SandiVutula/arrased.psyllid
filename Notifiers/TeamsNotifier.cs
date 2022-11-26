using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using arrased.psyllid.Notifiers;
using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;

namespace Nml.Refactor.Me.Notifiers
{
	public class TeamsNotifier : Notifier
	{
		public TeamsNotifier(IWebhookMessageBuilder messageBuilder, IOptions options) : base(messageBuilder, options) { }
    }
}
