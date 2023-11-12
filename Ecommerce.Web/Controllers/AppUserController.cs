using AutoMapper;
using Ecommerce.Web.Contract.Api.AppUser.Requests;
using Ecommerce.Web.Contract.Api.AppUser.Responses;
using Ecommerce.Web.Contract.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route(ApiRoutes.Api + "/v1/app-user")]
    public class AppUserController : BaseController
    {
        public AppUserController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
        {
        }

        [HttpPost]
        public async Task<GetUserInfoResponse> GetUserInfo([FromBody] GetUserInfoRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<UpdateUserInfoResponse> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}