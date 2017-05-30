using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GigHub.Tests.Models
{
    [TestClass]
    public class GigTests
    {
        private Gig _gig;

        [TestMethod]
        public void Cancel_CallCancel_SetCancelFlag()
        {
            _gig = new Gig();
            _gig.Cancel();

            _gig.IsCanceled.Should().BeTrue();            
        }

        [TestMethod]
        public void Cancel_whenCalled_EachAttendeeShouldHaveANotification()
        {
            var user = new ApplicationUser {Id = "1"};
            _gig = new Gig();            
            _gig.Attendances.Add(new Attendance { Attendee = user });
            _gig.Cancel();

            //Should be only one notification
            var attendees = _gig.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].Notifications.Count.Should().Be(1);

            //This notification is of type GigCancelled
            var notifications = attendees[0].Notifications.Select(n => n.Notification).ToList();
            notifications[0].NotificationType.Should().Be(NotificationType.GigCanceled);

        }
    }
}
