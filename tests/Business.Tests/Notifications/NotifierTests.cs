using Business.Tests.Fixtures;
using FluentAssertions;
using System.Linq;
using WebApiProductsProviders.Business.Interfaces;
using Xunit;

namespace Business.Tests.Notifications
{
    [Collection("NotificationsCollectionTests")]
    public class NotifierTests
    {
        private readonly NotificationFixture _notificationFixture;
        private readonly INotifier _notifier;

        public NotifierTests(NotificationFixture notificationFixture)
        {
            _notificationFixture = notificationFixture;
            _notifier = _notificationFixture.GetNotifier();
        }

        [Fact]
        public void Handle_ShouldAddANotification()
        {
            // Arrange
            var notification = _notificationFixture.GetNotification();
            var notifications = _notifier.GetNotifications();

            // Act
            _notifier.Handle(notification);
            var result = notifications.Any(n => n.Equals(notification));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GetNotifications_ShouldReturnTheListOfNotifications()
        {
            // Arrange
            var notifications = _notificationFixture.GetNotifications();
            notifications.ForEach(n => _notifier.Handle(n));

            // Act
            var result = _notifier.GetNotifications();

            // Assert
            result.Should().BeEquivalentTo(notifications);
        }

        [Fact]
        public void HasNotification_ShouldReturnFalseIfDoNotHaveNotifications()
        {
            // Arrange

            // Act
            var result = _notifier.HasNotification();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasNotification_ShouldReturnFalseIfHaveNotifications()
        {
            // Arrange
            var notification = _notificationFixture.GetNotification();

            // Act
            _notifier.Handle(notification);
            var result = _notifier.HasNotification();

            // Assert
            result.Should().BeTrue();
        }
    }
}
