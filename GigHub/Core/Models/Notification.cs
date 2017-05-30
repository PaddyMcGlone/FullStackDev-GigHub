using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    /// <summary>
    /// Notification class
    /// </summary>
    public class Notification
    {
        #region Read-only Properties       
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }
        #endregion

        #region Constructor

        protected Notification()
        {
            
        }

        private Notification(NotificationType notificationType, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException(nameof(gig));

            NotificationType = notificationType;
            Gig = gig;
            DateTime = DateTime.Today;
        }

        #endregion

        #region Factory methods

        /// <summary>
        /// Factory method which manages the creation of gigs
        /// </summary>
        /// <param name="gig">The gig</param>
        /// <returns>A new notification</returns>
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        /// <summary>
        /// Factory method manages the Cancel notification
        /// </summary>
        /// <param name="gig">Current Gig object</param>
        /// <returns>A cancel notification</returns>
        public static Notification CancelNotification(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

        /// <summary>
        ///  Factory method managing the update notifications
        /// </summary>
        /// <param name="gig">Current gig</param>
        /// <param name="originalDateTime">Original datetime</param>
        /// <param name="originalVenue">Original venue</param>
        /// <returns></returns>
        public static Notification UpdateNotification(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            return new Notification(NotificationType.GigUpdated, gig)
            {
                OriginalVenue = originalVenue,
                OriginalDateTime = originalDateTime
            };
        }

        #endregion
    }
}