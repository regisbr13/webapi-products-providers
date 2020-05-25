using Xunit;

namespace Business.Tests.Fixtures
{
    [CollectionDefinition("NotificationsCollectionTests")]
    public class NotificationsTestsCollection : ICollectionFixture<NotificationFixture>
    {
    }
}
