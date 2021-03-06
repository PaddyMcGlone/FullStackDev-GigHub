﻿using GigHub.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Delete(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public IEnumerable<Notification> GetNewNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();            
        }

        public IEnumerable<UserNotification> GetNewUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId & !un.IsRead)
                .ToList();
        }

    }
}