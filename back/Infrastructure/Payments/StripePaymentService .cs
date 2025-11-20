using Aplication.Interfaces.Payments;
using Core.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payments
{
    public class StripePaymentService : IStripePaymentService
    {
        public StripePaymentService(IConfiguration configuration) {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }
        public Task CancelSubscriptionAsync(string stripeSubscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateCustomerAsync (int userId)
        {
            var customerService = new CustomerService();

            var options = new CustomerCreateOptions
            {
             
                Metadata = new Dictionary<string, string>
            {
                { "UserId", userId.ToString() }
            }
            };

            var customer = await customerService.CreateAsync(options);

            return customer.Id; // returns StripeCustomerId
        }
        public async Task<bool> CustomerHasPaymentMethodAsync(string stripeCustomerId)
        {
            var pmService = new PaymentMethodService();
            var list = await pmService.ListAsync(new PaymentMethodListOptions
            {
                Customer = stripeCustomerId,
                Type = "card",
                Limit = 1
            });
            return list.Data.Any();
        }
        private async Task EnsureDefaultPaymentMethodAsync(string stripeCustomerId)
        {
            // 1) Obtener payment methods del customer
            var pmService = new PaymentMethodService();
            var paymentMethods = await pmService.ListAsync(new PaymentMethodListOptions
            {
                Customer = stripeCustomerId,
                Type = "card"
            });

            if (!paymentMethods.Data.Any())
                throw new Exception("Customer has no payment method attached.");

            var firstPm = paymentMethods.Data.First().Id;

            // 2) Obtener el customer para ver si ya tiene default
            var customerService = new CustomerService();
            var customer = await customerService.GetAsync(stripeCustomerId);

            PaymentMethod currentDefault = customer.InvoiceSettings?.DefaultPaymentMethod;

            // 3) Si NO hay default → asignarlo
            if (currentDefault == null)
            {
                await customerService.UpdateAsync(stripeCustomerId, new CustomerUpdateOptions
                {
                    InvoiceSettings = new CustomerInvoiceSettingsOptions
                    {
                        DefaultPaymentMethod = firstPm
                    }
                });
            }
        }

        public async Task<string> CreateSubscriptionAsync(string stripeCustomerId, string stripePriceId)
        {
            await EnsureDefaultPaymentMethodAsync(stripeCustomerId);
            var options = new SubscriptionCreateOptions
            {
                Customer = stripeCustomerId,
                Items = new List<SubscriptionItemOptions>
        {
            new SubscriptionItemOptions
            {
                Price = stripePriceId
            }
        }
            };

            var service = new SubscriptionService();
            Subscription subscription = await service.CreateAsync(options);

            return subscription.Id;
        }
    
        public async Task<string> CreateSubscriptionCheckoutSessionAsync(string stripeCustomerId, string stripePriceId)
        {

            var options = new SessionCreateOptions
            {
                Mode = "subscription",
                Customer = stripeCustomerId,
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = stripePriceId,
                    Quantity = 1
                }
            },
                SuccessUrl = "https://localhost:5001/success",
                CancelUrl = "https://localhost:5001/cancel",
                PaymentMethodTypes = new List<string> { "card" },

            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

        public Task HandleWebhookAsync(string json, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
