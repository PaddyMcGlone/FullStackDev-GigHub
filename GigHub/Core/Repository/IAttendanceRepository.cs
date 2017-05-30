using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repository
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendance);

        void Remove(Attendance attendance);

        Attendance Retrieve(string userId, int gigId);

        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}