using Ecommerce.Common.Utils;
using Ecommerce.Enums;
using Ecommerce.Web.Contract.Models.Email;
using MimeKit;

namespace Ecommerce.Web.BLL.Services.Email
{
    public static class SystemEmailGenerator
    {
        public static MimeMessage GenerateEmail(string emailFrom, SystemEmailModel email, bool needConfirmationLink = true)
        {
            var emailParams = new Dictionary<string, string>()
            {
                { "Username", email.Host },
                { "FirstName", email.FirstName }
            };

            if (needConfirmationLink)
            {
                emailParams.Add("ConfirmationLink", $"{email.Host}/{GetRouteLink(email.Type)}?code={email.Token}");
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Yugram", emailFrom));
            emailMessage.To.Add(new MailboxAddress("", email.To));
            emailMessage.Subject = "Ecommerce";
            emailMessage.Body = GenerateEmailBody(Enum.GetName(typeof(EmailType), email.Type), emailParams);

            return emailMessage;
        }

        private static string GetRouteLink(EmailType emailType)
        {
            return emailType switch
            {
                EmailType.ConfirmEmail => "email-confirmation",
                EmailType.ResetPassword => "reset-password",
                _ => throw new ArgumentOutOfRangeException(nameof(emailType))
            };
        }

        private static MimeEntity GenerateEmailBody(string emailTypeName, Dictionary<string, string> emailParams)
        {
            var builder = new BodyBuilder();
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Emails", $"{emailTypeName}.html");
            var templateString = File.ReadAllText(templatePath);

            templateString = StringUtils.InsertParamsIntoString(templateString, emailParams);
            builder.HtmlBody = templateString;

            return builder.ToMessageBody();
        }
    }
}