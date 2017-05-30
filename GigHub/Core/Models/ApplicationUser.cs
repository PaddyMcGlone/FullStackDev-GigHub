using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Properties
 
        public string Name { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> Notifications { get; set; }
        #endregion

        #region Constructor

        public ApplicationUser()
        {
            Followers = new List<Following>();
            Followees = new List<Following>();
            Notifications = new List<UserNotification>();
        }

        #endregion

        #region Methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Adds a notification onto the Attendee
        /// </summary>
        /// <param name="notification">User Notication</param>
        public void Notify(Notification notification)
        {
            Notifications.Add(new UserNotification(this, notification));                        
        }

        #endregion
    }
}