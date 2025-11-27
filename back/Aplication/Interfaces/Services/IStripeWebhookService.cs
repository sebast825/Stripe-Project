using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IStripeWebhookService
    {
        Task ProcessAsync( Event stripeEvent);
    }
}
