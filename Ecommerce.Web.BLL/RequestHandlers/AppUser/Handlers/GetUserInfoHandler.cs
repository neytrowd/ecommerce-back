using AutoMapper;
using Ecommerce.DAL.Repositories.AppUser;
using Ecommerce.Entities;
using Ecommerce.Web.Contract.Api.AppUser.Requests;
using Ecommerce.Web.Contract.Api.AppUser.Responses;
using Ecommerce.Web.Contract.Models.AppUser;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.BLL.RequestHandlers.AppUser.Handlers
{
    public class GetUserInfoHandler : BaseRequestHandler<GetUserInfoRequest, GetUserInfoResponse>
    {
        private readonly IAppUserRepository _appUserRepository;
        
        public GetUserInfoHandler(
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IAppUserRepository appUserRepository) : base(httpContext, mapper)
        {
            _appUserRepository = appUserRepository;
        }

        public override async Task<GetUserInfoResponse> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetByUserIdAsync(ApplicationUserId, cancellationToken);

            return new GetUserInfoResponse()
            {
                User = Map<AppUserModel, AppUserEntity>(user)
            };
        }
    }
}