using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Persistence.Repositories
{
    public interface INotificationRepository
    {
        void Add(Attendance attendance);
        void Delete(Attendance attendance);
        IEnumerable<Notification> GetNewNotifications(string userId);
        IEnumerable<UserNotification> GetNewUserNotifications(string userId);
    }
}