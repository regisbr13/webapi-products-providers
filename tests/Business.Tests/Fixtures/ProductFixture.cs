using Bogus;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Services;

namespace Business.Tests.Fixtures
{
    public class ProductFixture : IDisposable
    {
        public AutoMocker AutoMocker;

        public List<Product> GetValidProducts(bool productsAndCategories = true)
        {
            var products = new Faker<Product>("pt_BR")
                .CustomInstantiator(f => new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = f.Commerce.ProductName(),
                    Description = f.Commerce.ProductAdjective(),
                    Image = f.Internet.Avatar(),
                    Active = f.Random.Bool(),
                    Value = (decimal)f.Random.Double(1.0, 1000000.0),
                    Register = f.Date.Past(),
                    Category = new CategoryFixture().GetValidCategory(),
                    Provider = null
                });
            products.RuleFor(x => x.CategoryId, (f, p) => p.Category.Id);

            if(!productsAndCategories)
            {
                products.RuleFor(x => x.Category, f => null);
                products.RuleFor(x => x.Provider, f => null);
            }

            return products.Generate(10);
        }

        public Product GetValidProduct()
        {
            return GetValidProducts().FirstOrDefault();
        }

        public Product GetInvalidProduct()
        {
            var product = new Faker<Product>("pt_BR")
               .CustomInstantiator(f => new Product()
               {
                   Id = Guid.NewGuid(),
                   Name = f.Commerce.ProductName(),
                   Description = string.Empty,
                   Image = f.Internet.Avatar(),
                   Active = f.Random.Bool(),
                   Value = (decimal)f.Random.Double(0.0, 1.0),
                   Register = f.Date.Past(),
                   Category = new CategoryFixture().GetValidCategory(),
                   Provider = null
               });
            product.RuleFor(x => x.CategoryId, (f, p) => p.Category.Id);

            return product;
        }

        public ProductService GetProductService()
        {
            AutoMocker = new AutoMocker();
            var productService = AutoMocker.CreateInstance<ProductService>();
            return productService;
        }

        public void Dispose()
        {
        }
    }
}
