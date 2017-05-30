using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repository
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> RetrieveFollowers(string artistId);
        Following RetrieveFollowing(string followerId, string followeeId);
        void Add(Following following);
        void Remove(Following following);
    }
}