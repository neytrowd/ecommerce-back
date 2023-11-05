using Ecommerce.Common.Contract.Errors;
using Ecommerce.Common.Extensions;
using Ecommerce.Web.BLL.Mapping.AppUser;

namespace Ecommerce.Web.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IErrorsFactory, ErrorsFactory>();
            services.AddAutoMapper(typeof(AppUserMap));
            services.AddServicesFromCurrentDomain();

            return services;
        }
    }
}