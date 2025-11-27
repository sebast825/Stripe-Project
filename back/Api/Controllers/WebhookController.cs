using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Core.Dto.User;
using Core.Entities;
using Infrastructure.Stripe.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Cryptography.Xml;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly string _stripeSignatureKey;
        private  IStripeWebhookService _stripeWebhookService;

        public WebhookController(IConfiguration configuration, IStripeWebhookService _stripeWebhookServic)
        {
            _stripeSignatureKey = configuration["Stripe:Signature"];
            _stripeWebhookService = _stripeWebhookServic;
        }
     
        [HttpPost]
        public async Task<ActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var headerSignature = Request.Headers["Stripe-Signature"];
            Event stripeEvent;
            try
            {
                //transfor to validate the signature is valid
                stripeEvent = EventUtility.ConstructEvent(json, headerSignature, _stripeSignatureKey);
                await _stripeWebhookService.ProcessAsync(stripeEvent);       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok();
        }
    }
}
