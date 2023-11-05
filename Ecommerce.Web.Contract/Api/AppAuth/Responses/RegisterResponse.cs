using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Models.AppUser;

namespace Ecommerce.Web.Contract.Api.AppAuth.Responses
{
    public class RegisterResponse : BaseResponse
    {
        public AppUserModel User { get; set; }
    }
}