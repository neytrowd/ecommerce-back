using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Api.AppUser.Responses;
using Ecommerce.Web.Contract.Models.AppUser;

namespace Ecommerce.Web.Contract.Api.AppUser.Requests
{
    public class UpdateUserInfoRequest : BaseRequest<UpdateUserInfoResponse>
    {
        public AppUserModel User { get; set; }
    }
}