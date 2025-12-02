using Core.Common;
using Core.Enums;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StatsReadRepository : IStatsReadRepository
    {
        private readonly DataContext _context;

        public StatsReadRepository(DataContext context)
        {
            _context = context;
        }

        public Task<int> GetTotalUsersAsync()
            => _context.Users.Where(x => x.Role == UserRole.User).CountAsync();

        public async Task<decimal> GetTotalRevenueAsync()
        {

            decimal totalInCents = await _context.SubscriptionPaymentRecords
                .Where(i => i.Status == "paid")
                .SumAsync(i => (decimal)i.AmountPaid);
            //conver to usd
            return totalInCents / 100m;
        }

        public async Task<IReadOnlyList<PlanCountResult>> GetPlanCountsAsync()
            => await _context.UserSubscriptions
            .Where(x => x.Status == SubscriptionStatus.Active)
                .GroupBy(s => s.Plan)
                .Select(g => new PlanCountResult(g.Key.ToString(), g.Count()))
                .ToListAsync();

        public Task<decimal> EstimatedMonthlyRevenue()
            =>
             _context.UserSubscriptions
                .Where(x => x.Status == SubscriptionStatus.Active)
                .SumAsync(x => x.Price);

        public async Task<SubscriptionStatsResult> GetSubscriptionStatsAsync()
        {
            return await _context.UserSubscriptions
                .GroupBy(_ => 1)
                .Select(g => new SubscriptionStatsResult(
                    g.Count(),
                    g.Count(x => x.Status == SubscriptionStatus.Active),
                    g.Count(x => x.Status == SubscriptionStatus.Canceled)
                ))
                .FirstAsync();


        }

    }

}
