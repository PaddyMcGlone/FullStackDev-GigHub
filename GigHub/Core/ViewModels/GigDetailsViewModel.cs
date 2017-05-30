using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        #region Constructor

        public GigDetailsViewModel( Gig gig, bool authenticated = false)
        {
            Gig = gig;
            Authenticated = authenticated;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current Gig
        /// </summary>
        public Gig Gig { get; private set; }
        
        /// <summary>
        /// Gets or sets whether current user attending gig
        /// </summary>
        public bool UserAttendance { get; set; }

        /// <summary>
        /// Gets or sets the artist following
        /// </summary>
        public bool Following { get; set; }

        /// <summary>
        /// Gets or sets whether the current user is attending this gig
        /// </summary>
        public string AttendingCurrentGig => DetermineAttendance();

        /// <summary>
        /// Gets or sets whether a user is currently authenticated
        /// </summary>
        public bool Authenticated { get; set; }

        #endregion

        #region Helper methods        

        /// <summary>
        ///  Helper method for determining 
        /// </summary>
        /// <returns></returns>
        private string DetermineAttendance()
        {
            return UserAttendance ? "You are attending this Gig" : "You are not attending this Gig";            
        }

        #endregion
    }
}