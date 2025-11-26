using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Core.Entities;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Billing
{
    public class GetCustomerBillingPortalUrlUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStripeBillingService _stripeBillingService;

        public GetCustomerBillingPortalUrlUseCase(IUserRepository userRepository, IStripeBillingService stripeBillingService)
        {
            _userRepository = userRepository;
            _stripeBillingService = stripeBillingService;
        }
        public async Task<string> ExecuteAsync(int userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            string? StripeCustomerId = user?.StripeCustomerId;
            if (StripeCustomerId == null) {
                throw new KeyNotFoundException();
            }
            var url = await _stripeBillingService.CreateCustomerPortalUrlAsync(StripeCustomerId);

            return url;
        }
    }
}
