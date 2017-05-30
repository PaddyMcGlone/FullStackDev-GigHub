using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        #region Properties
        private readonly IUnitOfWork _unitfOfWork;
        #endregion

        #region Constructor
        public GigsController(IUnitOfWork unitfOfWork)
        {
            _unitfOfWork = unitfOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// The method marks a Gig as canceled, via recieving the HttpDelete verb
        /// </summary>
        /// <param name="id">The Gig Identifier</param>
        /// <returns>Http OK 200 result</returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitfOfWork.GigRepository.RetrieveGigWithAttendances(id);                

            if (gig == null || gig.IsCanceled) return NotFound();
            if (gig.ArtistId != userId) return Unauthorized();

            gig.Cancel();            
            _unitfOfWork.Complete();
            return Ok();
        }
        #endregion
    }
}
