using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace WebApiProductsProviders.App.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Products and Providers WebApi",
                    Version = "v1.0.0",
                    Description = "WebApi desenvolvida para gerenciamento de produtos, fornecedores e categorias.",
                    Contact = new OpenApiContact { Name = "Regis Lima", Email = "regislima1391@gmail.com", Url = new Uri("https://regislimaprojects.site") },
                    TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"); 
                x.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
