using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("Token inválido: no se encontró el userId.");

            if (!int.TryParse(userIdClaim, out int userId))
                throw new UnauthorizedAccessException("El userId del token no es válido.");

            return userId;
        }
    }
}
