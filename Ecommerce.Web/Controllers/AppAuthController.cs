using AutoMapper;
using Ecommerce.Web.Contract.Api.AppAuth.Requests;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;
using Ecommerce.Web.Contract.ApiRoutes;
using Ecommerce.Web.Options;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    [Route(ApiRoutes.Api + "/v1/app-auth")]
    public class AppAuthController : BaseController
    {
        private readonly AuthOptions _authOptions;
        
        public AppAuthController(
            IMapper mapper, 
            IMediator mediator,
            IOptions<AuthOptions> authOptions) : base(mapper, mediator)
        {
            _authOptions = authOptions.Value;
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
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(_authOptions.CookiesName, new CookieOptions()
            {
                Domain = _authOptions.Domain
            });

            return NoContent();
        }
    }
}