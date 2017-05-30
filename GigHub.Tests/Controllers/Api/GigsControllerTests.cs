using FluentAssertions;
using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repository;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userName;
        private string _userId;

        [TestInitialize]
        public void TestInitalize()
        {           
            _mockRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.GigRepository).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userName = "1";
            _userId = "user1@domain.com";
            _controller.MockCurrentUser(_userId, _userName);
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
           var gig = new Gig();
            gig.Cancel();
            _mockRepository.Setup(r => r.RetrieveGigWithAttendances(1)).Returns(gig);
            var result = _controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigCancelledByDifferentArtist_ReturnUnauthorised()
        {
            var gig = new Gig {ArtistId = _userId + 1};            
            _mockRepository.Setup(r => r.RetrieveGigWithAttendances(1)).Returns(gig);
           
            var result = _controller.Delete(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_GigBeingCancelled_ReturnOk()
        {
            var gig = new Gig {ArtistId = _userId};
            _mockRepository.Setup(r => r.RetrieveGigWithAttendances(1)).Returns(gig);

            var result = _controller.Delete(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
