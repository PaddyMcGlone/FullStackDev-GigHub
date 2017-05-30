using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweeController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public FolloweeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Actions

        // GET: Followee
        public ActionResult Index()
        {            
            var artists = _unitOfWork
                .FollowingRepository
                .RetrieveFollowers(User.Identity.GetUserId());            
            return View(artists);
        }

        #endregion
    }
}