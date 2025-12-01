using Core.Common;
using Core.Dto.Stats;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Admin
{
    public class GetAdminDashboardStatsUseCase
    {
        private readonly IStatsReadRepository _statsReadRepository;

        public GetAdminDashboardStatsUseCase(IStatsReadRepository statsReadRepository)
        {
            _statsReadRepository = statsReadRepository;
        }
        public async Task<AdminDashboardStatsDto> ExecuteAsync()
        {
         
            decimal totalRevenue = await _statsReadRepository.GetTotalRevenueAsync();
            IReadOnlyList<PlanCountResult> planCounts = await _statsReadRepository.GetPlanCountsAsync();   
            int totalUsers = await _statsReadRepository.GetTotalUsersAsync();
            decimal estimatedMonthlyRevenue = await _statsReadRepository.EstimatedMonthlyRevenue();
            var metricSubscription = await _statsReadRepository.GetSubscriptionStatsAsync();

            return new AdminDashboardStatsDto
            {
                TotalUsers = totalUsers,
                ActiveSubscriptions = metricSubscription.Active,
                TotalRevenue = totalRevenue,
                CanceledSubscriptions = metricSubscription.Canceled,
                EstimatedMonthlyRevenue = estimatedMonthlyRevenue,
                TotalSubscriptions = metricSubscription.Total,
                PlanDistribution = MapToPlanDistributionDto(planCounts)

            };
        }
        private IReadOnlyList<PlanDistributionDto> MapToPlanDistributionDto(IReadOnlyList<PlanCountResult> planCounts)
        {
            return planCounts.Select(x => new PlanDistributionDto
            {
                PlanName = x.PlanName,
                Count = x.Count,
                Percentage = Math.Round((double)x.Count / planCounts.Sum(p => p.Count) * 100,1)
            }).ToList().AsReadOnly();
        }
    }
}
