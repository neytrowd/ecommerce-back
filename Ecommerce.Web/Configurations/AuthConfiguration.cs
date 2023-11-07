using Ecommerce.Web.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Web.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = authOptions.Issuer,
                        ValidAudience = authOptions.Audience,
                        IssuerSigningKey = authOptions.SymmetricKey
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder appBuilder)
        {
            var authOptions = appBuilder.ApplicationServices.GetService<IOptions<AuthOptions>>();
            appBuilder.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[authOptions.Value.CookiesName];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);

                await next();
            });

            appBuilder.UseAuthentication();
            appBuilder.UseAuthorization();
            return appBuilder;
        }
    }
}