using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;

namespace Ecommerce.Web.Contract.Api.AppAuth.Requests
{
    public class RegisterRequest : BaseRequest<RegisterResponse>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}