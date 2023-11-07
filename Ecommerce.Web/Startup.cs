﻿using Ecommerce.Web.Configurations;
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
            services.AddAuthConfiguration(Configuration.GetSection("Auth").Get<AuthOptions>());
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddDatabaseConfiguration(Configuration);
            services.AddServicesConfiguration();
            services.AddSwaggerConfiguration();
            services.AddMediatrConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionConfiguration(env);
            app.UseDatabaseConfiguration();
            app.UseRouting();
            app.UseSwaggerConfiguration();
            app.UseAuthConfiguration();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}