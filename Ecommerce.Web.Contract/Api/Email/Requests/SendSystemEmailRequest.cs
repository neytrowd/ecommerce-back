using Ecommerce.Common.Contract;
using Ecommerce.Web.Contract.Api.Email.Responses;
using Ecommerce.Web.Contract.Models.Email;

namespace Ecommerce.Web.Contract.Api.Email.Requests
{
    public class SendSystemEmailRequest : BaseRequest<SendSystemEmailResponse>
    {
        public SystemEmailModel Email { get; set; } 
    }
}