using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repository
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig RetrieveGigWithAttendances(int gigId);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserIsAttending(string userId);
        IEnumerable<Gig> GetArtistFutureGigs(string userId);
        IEnumerable<Gig> GetAllFutureGigs();
    }
}