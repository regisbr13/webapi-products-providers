using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiProductsProviders.Data.Context;

namespace WebApiProductsProviders.App.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("WebApiProductsProviders.App")));
            services.AddScoped<MyContext>();

            return services;
        }
    }
}
