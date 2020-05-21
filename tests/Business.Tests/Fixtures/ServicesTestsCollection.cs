using Xunit;

namespace Business.Tests.Fixtures
{
    [CollectionDefinition("ServicesCollectionTests")]
    public class ServicesTestsCollection : ICollectionFixture<CategoryFixture>, ICollectionFixture<ProductFixture>, ICollectionFixture<ProviderFixture>
    {
    }
}
