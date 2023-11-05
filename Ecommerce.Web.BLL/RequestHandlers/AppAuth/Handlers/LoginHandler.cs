using AutoMapper;
using Ecommerce.Common.Contract.Errors;
using Ecommerce.Common.Contract.Errors.Exceptions;
using Ecommerce.Common.Utils;
using Ecommerce.DAL.Repositories.AppUser;
using Ecommerce.Entities;
using Ecommerce.Web.BLL.Services.AppAuth;
using Ecommerce.Web.Contract.Api.AppAuth.Requests;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;
using Ecommerce.Web.Contract.Models.AppUser;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.BLL.RequestHandlers.AppAuth.Handlers
{
    public class LoginHandler : BaseRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAppUserRepository _appUserRepository;

        private readonly IAppAuthService _appAuthService;
        
        public LoginHandler(
            IHttpContextAccessor httpContext, IMapper mapper,
            IAppUserRepository appUserRepository, 
            IAppAuthService appAuthService) : base(httpContext, mapper)
        {
            _appUserRepository = appUserRepository;
            _appAuthService = appAuthService;
        }

        public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null || !HashUtil.VerifyEquality(user.HashedPassword, request.Password))
            {
                throw new BaseException(ErrorCodes.AppAuth.UserNotFoundError);
            }
            
            if(user.IsEmailConfirmed)
            {
                var token = _appAuthService.GetAccessToken(user);
                _appAuthService.AppendTokenToCookieResponse(token);
            }
            
            return new LoginResponse()
            {
                User = Map<AppUserModel, AppUserEntity>(user)
            };
        }
    }
}