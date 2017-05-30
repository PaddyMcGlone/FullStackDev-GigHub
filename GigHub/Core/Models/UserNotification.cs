using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        #region Properties
        [Key, Column(Order = 1)]
        public string UserId { get; private set; }

        [Key, Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Entity Framework required
        /// </summary>
        protected UserNotification()
        {
            
        }

        /// <summary>
        /// A custom constructor used to create a new noticiation.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="notification">The notification</param>
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            User = user;
            UserId = user.Id;
            Notification = notification;
        }

        #endregion

        #region Helper methods

        public void Read()
        {
            IsRead = true;
        }

        #endregion
    }
}