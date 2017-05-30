using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Core.Repository;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Extensions
{
    [TestClass]
    public class GigRepositoriesTest
    {
        private IGigRepository _gigRepository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttedance; 

        [TestInitialize]
        public void TestInitalize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttedance = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttedance.Object);

            _gigRepository = new GigRepository(mockContext.Object);
        }

        #region GetGig

        [TestMethod]
        public void GetGig_ReturnAGig_ShouldReturnAGig()
        {
            var gig = new Gig
            {
                Id = 1,
                DateTime = DateTime.Today.AddMonths(1),
                ArtistId = "1",
                Artist = new ApplicationUser { Id = "1" , Name = "test" }
            };
            _mockGigs.SetSource(new[] {gig});
            var result = _gigRepository.GetGig(1);

            result.Should().NotBeNull();
            result.Artist.Name.Should().Be("test");
        }

        [TestMethod]
        public void GetGig_NoGigsExisting_ShouldNotReturnAGig()
        {
            _mockGigs.SetSource(new List<Gig>());
            var result = _gigRepository.GetGig(1);
            result.Should().BeNull();
        }

        #endregion

        #region GetAllFutureGig

        [TestMethod]
        public void GetAllFutureGigs_GigsInFutureExist_ReturnsFutureGigs()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddDays(2)                
            };
            _mockGigs.SetSource(new[] {gig});
            var result = _gigRepository.GetAllFutureGigs();
            result.Should().HaveCount(1);
            result.First().Should().Equals(gig);
        }

        [TestMethod]
        public void GetAllFutureGigs_GigsInThePast_ShouldReturnNull()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddDays(-10)
            };
            _mockGigs.SetSource(new[] {gig});
            var result = _gigRepository.GetAllFutureGigs();
            result.Should().BeEmpty();
        }

        #endregion

        #region GetArtistFutureGigs

        [TestMethod]
        public void GetArtistFutureGigs_GigsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddMonths(-1),
                ArtistId = "1"
            };
            _mockGigs.SetSource(new [] { gig } );

            var gigs = _gigRepository.GetArtistFutureGigs("1");
            gigs.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void GetArtistFutureGigs_GigIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddMonths(1),
                ArtistId = "1"                
            };
            gig.Cancel();
            _mockGigs.SetSource(new [] { gig });

            var result = _gigRepository.GetArtistFutureGigs("1");
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void GetArtistFutureGigs_GigIsForaDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddMonths(1),
                ArtistId = "2"
            };
            _mockGigs.SetSource(new [] {gig});

            var result = _gigRepository.GetArtistFutureGigs("1");
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void GetArtistFutureGigs_GigInFutureForArtist_ShouldReturnaGig()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Today.AddMonths(1),
                ArtistId = "1"
            };
            _mockGigs.SetSource(new[] { gig });

            var result = _gigRepository.GetArtistFutureGigs("1");
            result.Should().Contain(gig);
        }

        #endregion

        #region GetGigsUserIsAttending

        [TestMethod]
        public void GetGigsUserIsAttending_NoUserId_ShouldNotReturnResults()
        {
            var gig = new Gig
            {
                Artist = new ApplicationUser(),
                Genre = new Genre(),
            };

            var attendance = new Attendance
            {
                AttendeeId = "1",
                Gig = gig
            };
            
            _mockAttedance.SetSource(new [] { attendance });
            var result = _gigRepository.GetGigsUserIsAttending("1");
            result.Should().Contain(gig);
        }

        #endregion
    }
}
