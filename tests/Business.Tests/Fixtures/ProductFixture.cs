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
                    Category = new Faker<Category>(),
                    Provider = new Faker<Provider>()
                });
            products.RuleFor(x => x.CategoryId, (f, p) => p.Category.Id);
            products.RuleFor(x => x.ProviderId, (f, p) => p.Provider.Id);

            if (!productsAndCategories)
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
            var product = GetValidProduct();
            var faker = new Faker();
            product.Name = faker.Random.Bool() ? product.Name.Substring(0, 1) : string.Empty;
            product.Description = faker.Random.Bool() ? product.Description.Substring(0, 1) : string.Empty;
            product.Value = faker.Random.Bool() ? 0 : (decimal)faker.Random.Double(1, 10000);

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
