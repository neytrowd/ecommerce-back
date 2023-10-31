using Microsoft.AspNetCore.Http;

namespace Ecommerce.Common.Contract.Errors
{
    public interface IErrorsFactory
    {
        public BaseError CreateBaseError(HttpContext httpContext, Exception ex, int statusCode);
    }
}
