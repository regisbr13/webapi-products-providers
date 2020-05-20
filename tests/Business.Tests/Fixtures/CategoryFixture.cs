using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.Business.Models;

namespace Business.Tests.Fixtures
{
    public class CategoryFixture : IDisposable
    {
        public List<Category> GenerateValidCategories()
        {
            var categories = new Faker<Category>("pt_BR");
            categories.RuleFor(x => x.Name, f => f.Commerce.Categories(50).Take(1).FirstOrDefault());

            return categories.Generate(10);
        }

        public Category GenerateAValidCategory()
        {
            return GenerateValidCategories().FirstOrDefault();
        }

        public Category GenerateInvalidCategory()
        {
            var category = new Faker<Category>("pt_BR");
            category.RuleFor(x => x.Name, f => string.Empty);

            return category;
        }

        public void Dispose()
        {
        }
    }
}
