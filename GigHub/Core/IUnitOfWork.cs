using GigHub.Core.Repository;
using GigHub.Persistence.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository GigRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IGenreRepository GenreRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        INotificationRepository NotificationRepository { get;  }
        void Complete();
    }
}