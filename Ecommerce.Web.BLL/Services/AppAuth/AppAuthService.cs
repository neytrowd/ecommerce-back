using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Common.Constants;
using Ecommerce.Common.Extensions;
using Ecommerce.Entities;
using Ecommerce.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Web.BLL.Services.AppAuth
{
    [AddService(typeof(IAppAuthService), ServiceLifetime.Scoped)]
    public class AppAuthService : IAppAuthService
    {
        private readonly AuthOptions _authOptions;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppAuthService(IConfiguration configuration, IOptions<AuthOptions> authOptions, IHttpContextAccessor httpContextAccessor)
        {
            _authOptions = authOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetAccessToken(AppUserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(ClaimNames.ApplicationUserId, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Secret)
                ), algorithm);

            var token = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(366),
                signingCredentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void AppendTokenToCookieResponse(string token)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(_authOptions.CookiesName, token,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(366),
                    Domain = _authOptions.Domain,
                }
            );
        }
    }
}