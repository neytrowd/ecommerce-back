using Microsoft.AspNetCore.Http;

namespace Ecommerce.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetHeaderOrigin(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["origin"].ToString();
        }
    }
}