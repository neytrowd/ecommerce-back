using Microsoft.AspNetCore.HttpOverrides;

namespace Ecommerce.Web.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors();

            return services;
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app.UseCors(builder =>
            {
                var hosts = configuration.GetSection("CorsOrigins").Get<string[]>();

                builder
                    .WithOrigins(hosts)
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
            });

            return app;
        }
    }
}