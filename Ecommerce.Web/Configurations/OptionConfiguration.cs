using ShipCheaper.Web.Options;

namespace Ecommerce.Web.Configurations
{
    public static class OptionConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection("Auth"));
            
            return services;
        }
    }
}