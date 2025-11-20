using Core.Dto.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    internal class UserEventMapper
    {
        public static UserResponseDto UserResponseDtoMapper(User user)
        {
            return new UserResponseDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                StripeCustomerId = user.StripeCustomerId

            };
        }
    }
}
