using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public ActionResult Index(string query = null)
        {
            var upComingGigs = _unitOfWork.GigRepository.GetAllFutureGigs();                                    
            QueryFilter(query, ref upComingGigs);

            var userId = User.Identity.GetUserId();
            
            var attendances = _unitOfWork.AttendanceRepository
                .GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {                
                UpComingGigs = upComingGigs,
                ShowActions  = User.Identity.IsAuthenticated,
                Heading      = "Upcoming Gigs",
                SearchTerm   = query,
                Attendances  = attendances,
            };

            return View("Gigs", viewModel);
        }        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Helper method used to filter upcoming gigs
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="upComingGigs">Current all upcoming gigs</param>
        private void QueryFilter(string query, ref IEnumerable<Gig> upComingGigs)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }
        }
        #endregion
    }
}