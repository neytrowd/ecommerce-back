using System.Security.Claims;
using Ecommerce.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Common.Extensions
{
    public static class ClaimsExtensions
    {
        public static long GetApplicationUserId(this HttpContext context)
        {
            return context.User.GetApplicationUserId();
        }
        
        public static long GetApplicationUserId(this ClaimsPrincipal principal)
        {
            var applicationUserIdClaim = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimNames.ApplicationUserId);

            if (string.IsNullOrEmpty(applicationUserIdClaim?.Value))
            {
                return default;
            }

            return long.Parse(applicationUserIdClaim!.Value);
        }
    }
}