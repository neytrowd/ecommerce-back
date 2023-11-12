using Newtonsoft.Json;

namespace Ecommerce.Web.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddControllerConfiguration(this IServiceCollection services)
        {
            services
                .AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            return services;
        }

        public static IApplicationBuilder UseControllerConfiguration(this IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }   
    }
}