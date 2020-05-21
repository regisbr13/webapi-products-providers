using Bogus;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Services;

namespace Business.Tests.Fixtures
{
    public class CategoryFixture : IDisposable
    {
        public AutoMocker AutoMocker;

        public List<Category> GetValidCategories()
        {
            var categories = new Faker<Category>("pt_BR")
                .CustomInstantiator(f => new Category()
                {
                    Id = Guid.NewGuid(),
                    Products = new Faker<List<Product>>()
                });
            categories.RuleFor(x => x.Name, f => f.Commerce.Categories(50).Take(1).FirstOrDefault());

            return categories.Generate(10);
        }

        public Category GetValidCategory()
        {
            return GetValidCategories().FirstOrDefault();
        }

        public Category GetInvalidCategory()
        {
            var category = GetValidCategory();
            category.Name = new Faker().Random.Bool() ? string.Empty : category.Name.Substring(0, 1);

            return category;
        }

        public CategoryService GetCategoryService()
        {
            AutoMocker = new AutoMocker();
            var categoryService = AutoMocker.CreateInstance<CategoryService>();
            return categoryService;
        }

        public void Dispose()
        {
        }
    }
}
