using Xunit;

namespace Business.Tests.Fixtures
{
    [CollectionDefinition(nameof(CategoryFixture))]
    public class CategoryTestsCollection : ICollectionFixture<CategoryFixture>
    {
    }
}
