using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Models.AppUser;

namespace Ecommerce.Web.Contract.Api.AppAuth.Responses
{
    public class LoginResponse : BaseResponse
    {
        public AppUserModel User { get; set; }
    }
}