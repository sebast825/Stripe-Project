using Core.Dto.Auth;
using Core.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> AddAsync(UserCreateRequestDto userCreateDto);
        Task<UserResponseDto> UpdateStripeCustomerId(int userId, string stripeCustomerId);
        Task<UserResponseDto> ValidateCredentialsAsync(LoginRequestDto loginDto);
        Task<UserResponseDto> GetByIdAsync(int id);
    }
}
