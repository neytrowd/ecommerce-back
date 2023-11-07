using MailKit.Net.Smtp;
using AutoMapper;
using Ecommerce.Web.BLL.Services.Email;
using Ecommerce.Web.Contract.Api.Email.Requests;
using Ecommerce.Web.Contract.Api.Email.Responses;
using Ecommerce.Web.Options;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.BLL.RequestHandlers.Email.Handlers
{
    public class SendSystemEmailHandler : BaseRequestHandler<SendSystemEmailRequest, SendSystemEmailResponse>
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<SendSystemEmailHandler> _logger;
        
        public SendSystemEmailHandler(
            IMapper mapper,
            IHttpContextAccessor httpContext, 
            IOptions<EmailOptions> emailOptions,
            ILogger<SendSystemEmailHandler> logger) : base(httpContext, mapper)
        {
            _logger = logger;
            _emailOptions = emailOptions.Value;
        }

        public override async Task<SendSystemEmailResponse> Handle(SendSystemEmailRequest request, CancellationToken cancellationToken)
        {
            var emailMessage = SystemEmailGenerator.GenerateEmail(_emailOptions.Email, request.Email);

            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_emailOptions.Smtp, _emailOptions.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_emailOptions.Email, _emailOptions.Password);
                    await smtp.SendAsync(emailMessage);  
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            return new SendSystemEmailResponse();
        }
    }
}