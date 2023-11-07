using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Web.Options
{
    public class AuthOptions
    {
        public string SiteLoginUrl { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int Lifetime { get; set; }
        public string CookiesName { get; set; }
        public string Domain { get; set; }

        private SymmetricSecurityKey _symmetricKey;

        public SymmetricSecurityKey SymmetricKey
        {
            get
            {
                return _symmetricKey ??= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            }
        }
    }
}
