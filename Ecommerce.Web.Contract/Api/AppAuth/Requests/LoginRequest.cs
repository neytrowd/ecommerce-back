namespace Ecommerce.Web.Contract.Api.AppAuth.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}