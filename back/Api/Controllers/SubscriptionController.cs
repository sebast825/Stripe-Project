using Aplication.Helpers;
using Aplication.UseCases.Subscriptions;
using Core.Dto.SubscriptionPlan;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly SubscribeUserUseCase _subscribeUserUseCase;
        public SubscriptionController(SubscribeUserUseCase subscribeUserUseCase)
        {
            _subscribeUserUseCase = subscribeUserUseCase;
        }
        [HttpGet("plans")]
        public ActionResult<List<SubscriptionPlanResponseDto>> GetPlans()
        {
            return  Ok(DemoPlans.GetPlanResponse());
        }
   
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateSubscription([FromBody]  int planId)
        {
            var userId = User.GetUserId();

            var rsta = await _subscribeUserUseCase.ExecuteAsync(userId, planId);
            return Ok(rsta);
        }
    }
}
