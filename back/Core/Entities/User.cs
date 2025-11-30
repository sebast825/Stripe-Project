using Core.Constants;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : ClassBase
    {



        public required string Email { get; set; }
        public required string Password { get; set; }

        public string? FullName { get; set; }
        public string? StripeCustomerId { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<UserLoginHistory> LoginAttempts { get; set; } = new List<UserLoginHistory>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public void Validate()
        {
            ValidteEmail();
            ValidatePassword();
        }

        private void ValidatePassword()
        {
            if (Password?.Length < 8)
                throw new ValidationException(ErrorMessages.PasswordLengthMin);
        }

        private void ValidteEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
            }
            catch
            {
                throw new ValidationException(ErrorMessages.EmailFormat);
            }
        }
    }
}
