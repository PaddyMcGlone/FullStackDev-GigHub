using GigHub.Core;
using GigHub.Core.Repository;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository GigRepository { get; private set; }
        public IAttendanceRepository AttendanceRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public IFollowingRepository FollowingRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context               = context;
            GigRepository          = new GigRepository(_context);
            AttendanceRepository   = new AttendanceRepository(_context);
            GenreRepository        = new GenreRepository(_context);
            FollowingRepository    = new FollowingRepository(_context);
            NotificationRepository = new NotificationRepository(_context);
        }
        
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}