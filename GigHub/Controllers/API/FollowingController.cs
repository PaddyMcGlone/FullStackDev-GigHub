using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    public class FollowingController : ApiController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public FollowingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();
            var currentFollowing = _unitOfWork
                .FollowingRepository
                .RetrieveFollowing(User.Identity.GetUserId(), dto.FollowerId);

            if (currentFollowing != null)
                return BadRequest("Already following this artist");

            var following = new Following
            {
                FollowerId = dto.FollowerId,
                FolloweeId = userId
            };

            _unitOfWork.FollowingRepository.Add(following);
            _unitOfWork.Complete();            
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.FollowingRepository
                .RetrieveFollowing(User.Identity.GetUserId(), id);            

            if (following == null) return NotFound();

            _unitOfWork.FollowingRepository.Remove(following);
            _unitOfWork.Complete();
            return Ok(id);
        }

        #endregion
    }
}
