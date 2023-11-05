using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;

namespace Ecommerce.Web.Contract.Api.AppAuth.Requests
{
    public class LoginRequest : BaseRequest<LoginResponse>
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}