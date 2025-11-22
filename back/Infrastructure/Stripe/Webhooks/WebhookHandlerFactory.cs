using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Webhooks
{
    public class WebhookHandlerFactory
    {
        private readonly Dictionary<string, IStripeWebhookHandler> _handlers;

        public WebhookHandlerFactory(IEnumerable<IStripeWebhookHandler> handlers)
        {
            _handlers = handlers.ToDictionary(h => h.EventType);
        }

        public IStripeWebhookHandler? GetHandler(string eventType) {
            return _handlers.TryGetValue(eventType, out var handler)
                   ? handler
                   : null;
        }
    }
}
