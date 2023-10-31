using Ecommerce.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecommerce.Web.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<EcommerceDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("ecommerce");
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.UseRelationalNulls(true);
                    builder.MigrationsAssembly("Ecommerce.Web");
                }).EnableSensitiveDataLogging();
            });
            
            return services;
        }

        public static IApplicationBuilder UseDatabaseConfiguration(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var dbContext = serviceScope.ServiceProvider.GetService<EcommerceDbContext>();
            dbContext.Database.Migrate();

            using var connection = (NpgsqlConnection)dbContext.Database.GetDbConnection();
            connection.Open();
            connection.ReloadTypes();

            return builder;
        }
    }
}   