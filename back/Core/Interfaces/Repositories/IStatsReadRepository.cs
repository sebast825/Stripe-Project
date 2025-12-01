using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IStatsReadRepository
    {
        Task<int> GetTotalUsersAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> EstimatedMonthlyRevenue();
        public  Task<SubscriptionStatsResult> GetSubscriptionStatsAsync();
        Task<IReadOnlyList<PlanCountResult>> GetPlanCountsAsync();
    }

}
