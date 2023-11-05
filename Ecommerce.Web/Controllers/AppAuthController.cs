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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route(ApiRoutes.Api + "/v1/app-auth")]
    public class AppAuthController : BaseController
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppAuthController(IMapper mapper, IAppUserRepository appUserRepository) : base(mapper)
        {
            _appUserRepository = appUserRepository;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var user = await _appUserRepository.GetByEmailAsync(request.Email);

            if (user == null || HashUtil.VerifyEquality(user.HashedPassword, request.Password))
            {
                throw new BaseException(ErrorCodes.AppAuth.UserNotFoundError);
            }
            
            return new LoginResponse()
            {
                User = Map<AppUserModel, AppUserEntity>(user)
            };
        }
        
        [HttpPost("register"), AllowAnonymous]
        public async Task<RegisterResponse> Register()
        {
            Console.WriteLine("Register");
            
            throw new UserException(ErrorCodes.AppUser.AccessToResourceDenied);

            return new RegisterResponse()
            {

            };
        }
    }
}