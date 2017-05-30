using GigHub.Core.Models;
using GigHub.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }        

        public void Add(Gig gig)
        {            
            _context.Gigs.Add(gig);           
        }

        public Gig RetrieveGigWithAttendances(int gigId)
        {
            return _context.Gigs
                .Include(g => Enumerable.Select<Attendance, ApplicationUser>(g.Attendances, a => a.Attendee))
                .Single(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs.SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetAllFutureGigs()
        {
            return _context.Gigs                
                .Where(g => g.DateTime > DateTime.Now
                            && !g.IsCanceled)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsUserIsAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetArtistFutureGigs(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId
                    && g.DateTime > DateTime.Now
                    && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }
    }
}