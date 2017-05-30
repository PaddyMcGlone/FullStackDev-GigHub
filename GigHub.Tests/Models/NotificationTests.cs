using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GigHub.Tests.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void Notification_ConstructorWithoutGig_ShouldRaiseNullArgException()
        {
            //var a = Notification.CancelNotification(null);
        }

        [TestMethod]
        public void Notification_CancelNotification_ShouldRaiseCancelNotificaiton()
        {
            var n = Notification.CancelNotification(new Gig());
            n.NotificationType.Should().Be(NotificationType.GigCanceled);
            n.DateTime.Should().Be(DateTime.Today);
        }

        [TestMethod]
        public void Notification_CreatedNotification_ShouldRaiseCreatedNotification()
        {
            var n = Notification.GigCreated(new Gig());
            n.NotificationType.Should().Be(NotificationType.GigCreated);
            n.DateTime.Should().Be(DateTime.Today);
        }

        [TestMethod]
        public void Notification_UpdatedNotification_ShouldRaiseUpdatedNotification()
        {
            var n = Notification.UpdateNotification(new Gig(), DateTime.Today.AddDays(2), "Madhatters");
            n.NotificationType.Should().Be(NotificationType.GigUpdated);
            n.DateTime.Should().Be(DateTime.Today);
            n.OriginalDateTime.Should().Be(DateTime.Today.AddDays(2));
            n.OriginalVenue.Should().Be("Madhatters");
        }
    }
}
