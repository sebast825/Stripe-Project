using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Stats
{
    public class AdminDashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveSubscriptions { get; set; }
        public decimal TotalRevenue { get; set; }

        public int TotalSubscriptions { get; set; }
        public int CanceledSubscriptions { get; set; }
        public decimal EstimatedMonthlyRevenue { get; set; }

        public IReadOnlyList<PlanDistributionDto> PlanDistribution { get; set; }
    }

    public class PlanDistributionDto
    {
        public string PlanName { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

}
