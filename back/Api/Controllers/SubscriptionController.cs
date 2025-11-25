using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Services;
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
        private readonly IUserSubscriptionService _userSubscriptionService;
        public SubscriptionController(SubscribeUserUseCase subscribeUserUseCase, IUserSubscriptionService userSubscriptionService)
        { 
            _userSubscriptionService = userSubscriptionService;
            
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
        [HttpGet("current")]
        public async Task<IActionResult> CurrentUserSubscription()
        {
            var userId = User.GetUserId();

            var rsta = await _userSubscriptionService.GetByUserId(userId);
            return Ok(rsta);
        }
    }
}
