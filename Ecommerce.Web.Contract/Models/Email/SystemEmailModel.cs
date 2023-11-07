using Ecommerce.Enums;

namespace Ecommerce.Web.Contract.Models.Email
{
    public class SystemEmailModel
    {
        public string To { get; set; }
        
        public string FirstName { get; set; }
        
        public string Token { get; set; }
        
        public string Host { get; set; }
        
        public EmailType Type { get; set; }
    }
}