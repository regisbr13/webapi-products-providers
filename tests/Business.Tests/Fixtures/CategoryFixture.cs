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
            var categories = new Faker<Category>("pt_BR");
            categories.RuleFor(x => x.Name, f => f.Commerce.Categories(50).Take(1).FirstOrDefault());

            return categories.Generate(10);
        }

        public Category GetValidCategory()
        {
            return GetValidCategories().FirstOrDefault();
        }

        public Category GetInvalidCategory()
        {
            var category = new Faker<Category>("pt_BR");
            category.RuleFor(x => x.Name, f => string.Empty);

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
