using Aplication.Interfaces.Payments;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Core.Dto.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Users
{
    public class CreateUserUseCase
    {
        private readonly IUserService _userService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public CreateUserUseCase(IUserService userService, IStripeCustomerService stripeCustomerService)
        {
            _userService = userService;
            _stripeCustomerService = stripeCustomerService;
        }

        public async Task ExecuteAsync(UserCreateRequestDto userCreateDto)
        {
            UserResponseDto userResponse = await _userService.AddAsync(userCreateDto);
            string stripeCustomerId = await _stripeCustomerService.CreateCustomerAsync(userResponse.Id);
            await _userService.UpdateStripeCustomerId(userResponse.Id, stripeCustomerId);
        }

    }
}
