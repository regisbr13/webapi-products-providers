using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Notifications;
using WebApiProductsProviders.Business.Services;
using WebApiProductsProviders.Data.Repository;

namespace WebApiProductsProviders.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependeces(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
