using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Stripe
{
    public interface IStripeWebhookHandler
    {
        string EventType { get; }
        Task HandleAsync(Event stripeEvent);

    }
}
