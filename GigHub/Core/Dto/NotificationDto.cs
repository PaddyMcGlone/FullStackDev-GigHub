using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dto
{
    public class NotificationDto
    {
        #region Read-only Properties       
   
        public DateTime DateTime { get; set; }

        public NotificationType NotificationType { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }
   
        public GigDto Gig { get; set; }
        #endregion
    }
}