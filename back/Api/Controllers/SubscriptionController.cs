using Aplication.Helpers;
using Core.Dto.SubscriptionPlan;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {

        [HttpGet("plans")]
        public ActionResult<List<SubscriptionPlanResponseDto>> GetPlans()
        {
            return  Ok(DemoPlans.GetPlanResponse());
        }
    }
}
