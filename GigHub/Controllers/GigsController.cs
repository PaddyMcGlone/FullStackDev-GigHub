using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        #region Properties        
        private readonly IUnitOfWork _unitOfWork;        
        #endregion

        #region Constructor

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Create a Gig",
                Genres = _unitOfWork.GenreRepository.RetrieveGenres().ToList()
            };
            return View("Gigform", viewModel);
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.RetrieveGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue    = viewModel.Venue,
                DateTime = viewModel.GetDateTime(),
                GenreId  = viewModel.Genre
            };

            _unitOfWork.GigRepository.Add(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (User.Identity.GetUserId().IsNullOrWhiteSpace())
            {
                return HttpNotFound();
            }

            var currentGig = _unitOfWork.GigRepository.GetGig(id);
            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id      = currentGig.Id,  
                Venue   = currentGig.Venue,
                Genre   = currentGig.GenreId,
                Date    = currentGig.DateTime.ToString("d MMM yyyy"),
                Time    = currentGig.DateTime.ToString("HH:mm"),
                Genres  = _unitOfWork.GenreRepository.RetrieveGenres().ToList()
            };
            return View("GigForm",viewModel);
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.RetrieveGenres();
                return View("GigForm", viewModel);
            }

            if (User.Identity.GetUserId().IsNullOrWhiteSpace())
            {
                return HttpNotFound();
            }

            var gig = _unitOfWork.GigRepository.RetrieveGigWithAttendances(viewModel.Id);                        
            gig.Update(viewModel.Venue, viewModel.Genre, viewModel.GetDateTime());
            _unitOfWork.Complete();
            
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpComingGigs = _unitOfWork.GigRepository.GetGigsUserIsAttending(userId),
                ShowActions  = User.Identity.IsAuthenticated,
                Heading      = "Gigs I'm Attending",
                Attendances  = _unitOfWork.AttendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId),
            };

            return View("Gigs", viewModel);
        }               

        [Authorize]
        public ActionResult Mine()
        {
            var myGigs = _unitOfWork.GigRepository.GetArtistFutureGigs(User.Identity.GetUserId());
            return View(myGigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
           var authenticated = User.Identity.IsAuthenticated;
           var gig = _unitOfWork.GigRepository.GetGig(id);
           var viewModel = new GigDetailsViewModel(gig, authenticated);

           if (!authenticated) return View(viewModel);

           var userId = User.Identity.GetUserId();            
           viewModel.UserAttendance = gig.Attendances.Any(a => a.AttendeeId == userId);
           viewModel.Following = _unitOfWork.FollowingRepository.RetrieveFollowers(gig.ArtistId)
                .Any(f => f.FolloweeId == userId); 
            
            return View(viewModel);
        }

        #endregion
    }
}