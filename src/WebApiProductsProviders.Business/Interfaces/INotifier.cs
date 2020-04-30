using System.Collections.Generic;
using WebApiProductsProviders.Business.Notifications;

namespace WebApiProductsProviders.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
