using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        #region Properties
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public bool IsCanceled { get; private set; }

        // Navigational property
        public ICollection<Attendance> Attendances { get; private set; }        

        #endregion

        #region Constructor

        public Gig()
        {
            Attendances = new Collection<Attendance>();            
            Create();
        }

        #endregion

        #region Method

        /// <summary>
        /// A method used to encapsulate the gig creation
        /// </summary>
        public void Create()
        {
            AddAttendeeNotification(Notification.GigCreated(this));
        }

        /// <summary>
        /// A method used to encapsulate the cancel action
        /// </summary>
        public void Cancel()
        {
            IsCanceled = true;
            AddAttendeeNotification(Notification.CancelNotification(this)); 
        }

        /// <summary>
        /// A method used to encapsulate the gig being updated action
        /// </summary>
        public void Update(string venue, byte genre, DateTime dateTime)
        {
            var notification = Notification.UpdateNotification(this, dateTime, venue);
            
            // Map updated values
            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            AddAttendeeNotification(notification);
        }

        #endregion

        #region Helper Methods

        private void AddAttendeeNotification(Notification notification)
        {
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        #endregion

    }
}