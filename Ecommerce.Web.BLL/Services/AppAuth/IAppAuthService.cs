using Ecommerce.Entities;

namespace Ecommerce.Web.BLL.Services.AppAuth
{
    public interface IAppAuthService
    {
        public string GetAccessToken(AppUserEntity user);

        public void AppendTokenToCookieResponse(string token);
    }
}