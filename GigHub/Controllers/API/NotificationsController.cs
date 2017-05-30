using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    /// <summary>
    /// An api which returns all of the notifications for a user
    /// as well as their gig and artist info
    /// </summary>
    [Authorize]
    public class NotificationsController : ApiController
    {
        public IUnitOfWork UnitOfWork { get; }

        #region Constructor
        public NotificationsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;            
        }
        #endregion

        #region Methods

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = UnitOfWork.NotificationRepository.GetNewNotifications(User.Identity.GetUserId());            
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult Read()
        {
            var notifications = UnitOfWork.NotificationRepository.GetNewUserNotifications(User.Identity.GetUserId());
            notifications.ToList().ForEach(un => un.Read());             
                         
            UnitOfWork.Complete();
            return Ok();
        }

        #endregion
    }
}
