using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class NotificationControllerTests
    {
        private NotificationsController notificationsController;
        private Mock<NotificationRepository> notificationRepository;
        private string _userName;
        private string _userId;

        [TestInitialize]
        public void Initialize()
        {
            notificationRepository = new Mock<NotificationRepository>();
            var MockUoW = new Mock<IUnitOfWork>();
            MockUoW.SetupGet(u => u.NotificationRepository).Returns(notificationRepository.Object);

            notificationsController = new NotificationsController(MockUoW.Object);
            _userName = "1";
            _userId = "user1@domain.com";
            notificationsController.MockCurrentUser(_userName, _userId);
        }

        [TestMethod]
        public void GetNewNotifications_GetUnreadNotifications_GetNewNotificaitons()
        {
       
        }
    }
}
