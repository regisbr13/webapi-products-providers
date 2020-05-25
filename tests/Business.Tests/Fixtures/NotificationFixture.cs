using Bogus;
using Bogus.DataSets;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.Business.Notifications;

namespace Business.Tests.Fixtures
{
    public class NotificationFixture : IDisposable
    {
        public List<Notification> GetNotifications()
        {
            var notifications = new Faker<Notification>("pt_BR")
                .CustomInstantiator(f => new Notification(f.Lorem.Sentence(5)));

            return notifications.Generate(10);
        }

        public Notification GetNotification()
        {
            return GetNotifications().FirstOrDefault();
        }

        public Notifier GetNotifier()
        {
            var autoMocker = new AutoMocker();
            var notifier = autoMocker.CreateInstance<Notifier>();
            return notifier;
        }

        public void Dispose()
        {
        }
    }
}
