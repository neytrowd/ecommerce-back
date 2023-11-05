using Ecommerce.Web.BLL.RequestHandlers;

namespace Ecommerce.Web.Configurations
{
    public static class MediatrConfiguration
    {
        public static IServiceCollection AddMediatrConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(BaseRequestHandler<,>).Assembly);
            });
            
            return services;
        }
    }
}