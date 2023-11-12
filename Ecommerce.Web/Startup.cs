using Ecommerce.Web.Configurations;
using Ecommerce.Web.Options;

namespace Ecommerce.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptionsConfiguration(Configuration);
            services.AddHttpContextAccessor();
            services.AddControllerConfiguration();
            services.AddDatabaseConfiguration(Configuration);
            services.AddAuthConfiguration(Configuration.GetSection("Auth").Get<AuthOptions>());
            services.AddServicesConfiguration();
            services.AddCorsConfiguration();
            services.AddMediatrConfiguration();
            services.AddSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionConfiguration(env);
            app.UseSwaggerConfiguration();
            app.UseCorsConfiguration(Configuration);
            app.UseDatabaseConfiguration();
            app.UseRouting();
            app.UseAuthConfiguration();
            app.UseControllerConfiguration();
        }
    }
}