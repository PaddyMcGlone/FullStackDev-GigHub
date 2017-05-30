using System;

namespace GigHub.Core.Dto
{
    public class GigDto
    {
        #region Properties

        public int Id { get; set; }

        public ArtistDto Artist { get; set; }
        
        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }

        public bool IsCanceled { get; set; }

        #endregion
    }
}