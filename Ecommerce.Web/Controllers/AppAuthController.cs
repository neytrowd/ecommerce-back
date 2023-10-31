using Ecommerce.Web.Contract.ApiRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route(ApiRoutes.Api + "/v1/app-auth")]
    public class AppAuthController : BaseController
    {
        [HttpPost("login"), AllowAnonymous]
        public async void Login()
        {
            
        }
        
        [HttpPost("register"), AllowAnonymous]
        public async void Register()
        {
            
        }
    }
}