using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;        
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitalize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);            
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsRead_ShouldNotBeReturned()
        {
            var user = new ApplicationUser { Id = "1" };
            var notification = Notification.CancelNotification(new Gig());                        
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockNotifications.SetSource(new[] { userNotification });
            var notifications = _repository.GetNewNotifications(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationsForDifferentUser_ShouldNotReturnResults()
        {
            var user = new ApplicationUser {Id = "1"};
            var notification = Notification.CancelNotification(new Gig());
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] {userNotification});
            var result = _repository.GetNewNotifications(user.Id + '#');

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NumberOfUnreadNotifications_ShouldReturnResults()
        {
            var user = new ApplicationUser {Id = "1"};
            var notification = Notification.CancelNotification(new Gig());
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] {userNotification});
            var result = _repository.GetNewNotifications(user.Id);

            result.Should().HaveCount(1);
            result.First().Should().Equals(notification);
        }
    }
}
