using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Core.Constants;
using Core.Dto;
using Core.Dto.Auth;
using Core.Dto.User;
using Core.Entities;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        public async Task<PagedResponseDto<UserWithSubscriptionResponseDto>> GetPagedAsync(int page, int pageSize, string? searchTerm)
        {
            PagedResult<UserWithSubscriptionResponseDto> rsta = await _userRepository.GetPagedAsync(page, pageSize, searchTerm);
            return PagedMapper.ToResponse(page, pageSize, rsta.TotalItems,rsta.Data.ToList());
    
        }
        public async Task<UserResponseDto> AddAsync(UserCreateRequestDto userCreateDto)
        {
            User user = new User { Email = userCreateDto.Email, Password = userCreateDto.Password, FullName = userCreateDto.FullName };
            if (await this.EmailAlreadyUsed(user.Email))
            {
                throw new InvalidOperationException(ErrorMessages.EmailNotAviable);
            }

            user.Validate();
            //add hashpassword after validate, if not validation don't work on password
            user.Password = this.HashPassword(userCreateDto.Password);


            await _userRepository.AddAsync(user);

            return UserMapper.ToResponseDto(user);
        }

        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound("User", id));

            }
            return UserMapper.ToResponseDto(user);

        }

        public async Task<UserResponseDto> UpdateStripeCustomerId(int userId, string stripeCustomerId)
        {

            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound("User", userId));

            }
            user.StripeCustomerId = stripeCustomerId;
            await _userRepository.UpdateAsync(user);
            return UserMapper.ToResponseDto(user);
        }

        public async Task<UserResponseDto> ValidateCredentialsAsync(LoginRequestDto loginDto)
        {
            User? user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new InvalidCredentialException(ErrorMessages.InvalidCredentials);

            }
            bool matchMassword = this.MatchPassword(loginDto.Password, user.Password);
            if (!matchMassword)
            {
                throw new InvalidCredentialException(ErrorMessages.InvalidCredentials);

            }
            return UserMapper.ToResponseDto(user);

        }
        private async Task<bool> EmailAlreadyUsed(string email)
        {
            return await _userRepository.GetByEmailAsync(email) != null;

        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
        private bool MatchPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }

}
