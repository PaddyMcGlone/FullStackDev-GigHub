using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GigHub.Tests.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void Notify_NewNotificationRaised_AddsNewUserNotificationToUser()
        {
            var user = new ApplicationUser {Id = "1"};
            user.Notify(Notification.CancelNotification(new Gig()));

            user.Notifications.Count.Should().Be(1);
            var userNotification = user.Notifications.First();
            userNotification.User.Should().Be(user);
            userNotification.Notification.NotificationType.Should().Be(NotificationType.GigCanceled);
        }
    }
}
