using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Aplication.Services;
using Aplication.UseCases.Billing;
using Aplication.UseCases.Subscriptions;
using Core.Dto.SubscriptionPlan;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Stripe;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly SubscribeUserUseCase _subscribeUserUseCase;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly GetCustomerBillingPortalUrlUseCase _getCustomerBillingPortalUrlUseCase;

        public SubscriptionController(SubscribeUserUseCase subscribeUserUseCase, IUserSubscriptionService userSubscriptionService, GetCustomerBillingPortalUrlUseCase getCustomerBillingPortalUrlUseCase)
        {
            _userSubscriptionService = userSubscriptionService;
            _getCustomerBillingPortalUrlUseCase = getCustomerBillingPortalUrlUseCase;
            _subscribeUserUseCase = subscribeUserUseCase;
        }

        [Authorize]
        [HttpGet("billing-portal")]
        public async Task<ActionResult> StripeProtal()
        {
            var userId = User.GetUserId();

            var StripeProtalUrl = await _getCustomerBillingPortalUrlUseCase.ExecuteAsync(userId);
            return Ok(StripeProtalUrl);
        }

        [HttpGet("plans")]
        public ActionResult<List<SubscriptionPlanResponseDto>> GetPlans()
        {
            return Ok(DemoPlans.GetPlanResponse());
        }
        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateSubscription([FromBody] int planId)
        {
            var userId = User.GetUserId();

            var rsta = await _subscribeUserUseCase.ExecuteAsync(userId, planId);
            return Ok(rsta);
        }
        
        [Authorize]

        [HttpGet("current")]
        public async Task<IActionResult> CurrentUserSubscription()
        {
            var userId = User.GetUserId();

            var rsta = await _userSubscriptionService.GetByUserId(userId);
            return Ok(rsta);
        }
    }
}
