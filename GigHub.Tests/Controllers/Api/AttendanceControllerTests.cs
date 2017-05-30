using FluentAssertions;
using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repository;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendanceControllerTests
    {
        private AttendanceController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userName;
        private string _userId;
        private int _gigId = 1;

        [TestInitialize]
        public void TestInitalize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUnitofWork = new Mock<IUnitOfWork>();
            mockUnitofWork.SetupGet(u => u.AttendanceRepository).Returns(_mockRepository.Object);

            _controller = new AttendanceController(mockUnitofWork.Object);
            _userName = "user1@domain.com";
            _userId = "1";
            _controller.MockCurrentUser(_userId, _userName);
        }

        [TestMethod]
        public void Attend_NoAttendanceDto_ShouldReturnArgumentNullException()
        {
            //Need to figure how to handle exceptions within FluentAsserts            
        }

        [TestMethod]
        public void Attend_AlreadyAttendingGig_ShouldReturnBadRequest()
        {
            var attendance = new Attendance
            {
                AttendeeId = _userId,
                GigId = _gigId
            };
            
            _mockRepository.Setup(r => r.Retrieve(_userId, _gigId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = _gigId });
            result.Should().BeOfType<BadRequestErrorMessageResult>("Already attending this gig");  
        }        

        [TestMethod]
        public void Attend_NotAttendingthisGig_ShouldReturnOk()
        {            
            var result = _controller.Attend(new AttendanceDto {GigId = _gigId});           
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Delete_NotAttendingThisGig_ShouldReturnBadRequest()
        {
            _mockRepository.Setup(r => r.Retrieve(_userId, _gigId)).Returns((Attendance)null);
            var result = _controller.Delete(_gigId);
            result.Should().BeOfType<BadRequestErrorMessageResult>("Unable to find attendance");
        }

        [TestMethod]
        public void Delete_DeleteAttendance_ShouldReturnOkMessage()
        {
            _mockRepository.Setup(r => r.Retrieve(_userId, _gigId))
                .Returns(new Attendance {AttendeeId = _userId, GigId = _gigId});
            var result = _controller.Delete(_gigId);
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void Delete_DeleteAttendance_ShouldReturnIdOfDeletedAttendance()
        {
            _mockRepository.Setup(r => r.Retrieve(_userId, _gigId))
                .Returns(new Attendance { AttendeeId = _userId, GigId = _gigId });
            var result = (OkNegotiatedContentResult<int>) _controller.Delete(_gigId);
            result.Should().Equals(_gigId);
        }
    }
}
