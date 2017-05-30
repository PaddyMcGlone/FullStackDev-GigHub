using GigHub.Core.Models;
using GigHub.Core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }

        public IEnumerable<Following> RetrieveFollowers(string artistId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == artistId).ToList();
        }

        public Following RetrieveFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FollowerId == followerId
                                      && f.FolloweeId == followeeId);
        }
    }
}