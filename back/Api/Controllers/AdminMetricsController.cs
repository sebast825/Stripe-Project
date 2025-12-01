using Aplication.UseCases.Admin;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Authorize(Roles =nameof(UserRole.Admin))]
    [ApiController]
    [Route("api/admin")]
    public class AdminMetricsController : Controller
    {
        private readonly GetAdminDashboardStatsUseCase _getAdminDashboardStatsUseCase;

        public AdminMetricsController(GetAdminDashboardStatsUseCase getAdminDashboardStatsUseCase)
        {
            _getAdminDashboardStatsUseCase = getAdminDashboardStatsUseCase;
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            return Ok(await _getAdminDashboardStatsUseCase.ExecuteAsync());
        
        }
    }

}
