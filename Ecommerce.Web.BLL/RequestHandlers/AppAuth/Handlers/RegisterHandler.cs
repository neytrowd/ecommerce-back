using System.Text.RegularExpressions;
using AutoMapper;
using Ecommerce.Common.Constants;
using Ecommerce.Common.Contract.Errors;
using Ecommerce.Common.Contract.Errors.Exceptions;
using Ecommerce.Common.Utils;
using Ecommerce.DAL.Repositories.AppUser;
using Ecommerce.Entities;
using Ecommerce.Enums;
using Ecommerce.Web.Contract.Api.AppAuth.Requests;
using Ecommerce.Web.Contract.Api.AppAuth.Responses;
using Ecommerce.Web.Contract.Api.Email.Requests;
using Ecommerce.Web.Contract.Models.AppUser;
using Ecommerce.Web.Contract.Models.Email;
using MediatR;
using Microsoft.AspNetCore.Http;
using Ecommerce.Common.Extensions;

namespace Ecommerce.Web.BLL.RequestHandlers.AppAuth.Handlers
{
    public class RegisterHandler : BaseRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IMediator _mediator;
        private readonly IAppUserRepository _appUserRepository;

        public RegisterHandler(
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IAppUserRepository appUserRepository,
            IMediator mediator) : base(httpContext, mapper)
        {
            _mediator = mediator;
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
            
            // Send confirmation email
            await _mediator.Send(new SendSystemEmailRequest()
            {
                Email = new SystemEmailModel
                {
                    To = user.Email,
                    FirstName = user.FirstName,
                    Host = _httpContext.HttpContext.GetHeaderOrigin(),
                    Token = Guid.NewGuid().ToString(),
                    Type = EmailType.ConfirmEmail
                }
            });

            return new RegisterResponse()
            {
                User = Map<AppUserModel, AppUserEntity>(user),
            };
        }
    }
}