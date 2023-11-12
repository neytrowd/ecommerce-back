using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Models.AppUser;

namespace Ecommerce.Web.Contract.Api.AppUser.Responses
{
    public class UpdateUserInfoResponse : BaseResponse
    {
        public AppUserModel User { get; set; }
    }
}