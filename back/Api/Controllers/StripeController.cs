using Aplication.UseCases.Subscriptions;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        private readonly SubscribeUserUseCase _subscribeUserUseCase;
        public StripeController(SubscribeUserUseCase subscribeUserUseCase)
        {
            _subscribeUserUseCase = subscribeUserUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubscription(int id)
        {
            SubscriptionPlanType plan = SubscriptionPlanType.Yearly;
            var rsta = await _subscribeUserUseCase.ExecuteAsync(id, plan);
            return Ok(rsta);
        }
    }
}
