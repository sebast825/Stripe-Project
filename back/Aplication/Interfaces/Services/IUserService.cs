using Aplication.Dto;
using Core.Dto.Auth;
using Core.Dto.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IUserService
    {
        Task<PagedResponseDto<UserResponseDto>> GetPagedAsync(int page, int pageSize, string? searchTerm);
        Task<UserResponseDto> AddAsync(UserCreateRequestDto userCreateDto);
        Task<UserResponseDto> UpdateStripeCustomerId(int userId, string stripeCustomerId);
        Task<UserResponseDto> ValidateCredentialsAsync(LoginRequestDto loginDto);
        Task<UserResponseDto> GetByIdAsync(int id);
    }
}
