using AutoMapper;
using Ecommerce.Common.Contract.Errors;
using Ecommerce.Common.Contract.Errors.Exceptions;
using Ecommerce.Common.Utils;
using Ecommerce.DAL.Repositories.AppUser;
using Ecommerce.Entities;
using Ecommerce.Web.Contract.Api.AppAuth.Requests;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;
using Ecommerce.Web.Contract.ApiRoutes;
using Ecommerce.Web.Contract.Models.AppUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route(ApiRoutes.Api + "/v1/app-auth")]
    public class AppAuthController : BaseController
    {
        public AppAuthController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
        {
            
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            return await _mediator.Send(request);
        }
        
        [HttpPost("register"), AllowAnonymous]
        public async Task<RegisterResponse> Register([FromBody] RegisterRequest request)
        {
            return await _mediator.Send(request);
        }
        
    }
}