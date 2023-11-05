using System.Text.RegularExpressions;
using AutoMapper;
using Ecommerce.Common.Constants;
using Ecommerce.Common.Contract.Errors;
using Ecommerce.Common.Contract.Errors.Exceptions;
using Ecommerce.Common.Utils;
using Ecommerce.DAL.Repositories.AppUser;
using Ecommerce.Entities;
using Ecommerce.Web.Contract.Api.AppAuth.Requests;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;
using Ecommerce.Web.Contract.Models.AppUser;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.BLL.RequestHandlers.AppAuth.Handlers
{
    public class RegisterHandler : BaseRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IAppUserRepository _appUserRepository;

        public RegisterHandler(
            IHttpContextAccessor httpContext, IMapper mapper,
            IAppUserRepository appUserRepository) : base(httpContext, mapper)
        {
            _appUserRepository = appUserRepository;
        }

        public override async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var userWithSameLogin = await _appUserRepository.GetOneWhereAsync(user => user.Email == request.Email);

            if (userWithSameLogin != null)
            {
                throw new BaseException(ErrorCodes.AppUser.UserWithTheSameLoginAlreadyExistsError);
            }
            if (!Regex.IsMatch(request.Password, ValidationRegulars.Password))
            {
                throw new BaseException(ErrorCodes.AppAuth.IncorrectPasswordError);
            }

            var user = new AppUserEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                HashedPassword = HashUtil.Hash(request.Password),
                CreatedTime = DateTime.UtcNow.RoundToSeconds()
            };

            _appUserRepository.Add(user);
            await _appUserRepository.SaveChangesAsync();

            return new RegisterResponse()
            {
                User = Map<AppUserModel, AppUserEntity>(user),
            };
        }
    }
}