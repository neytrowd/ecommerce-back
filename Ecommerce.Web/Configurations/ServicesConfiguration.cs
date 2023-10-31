using Ecommerce.Common.Extensions;

namespace Ecommerce.Web.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddServicesFromCurrentDomain();

            return services;
        }
    }
}