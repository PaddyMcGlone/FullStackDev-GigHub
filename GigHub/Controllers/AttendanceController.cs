using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exist = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exist) return BadRequest("Already attending this gig");
            
            var attendance = new Attendance
            {
                GigId      = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
