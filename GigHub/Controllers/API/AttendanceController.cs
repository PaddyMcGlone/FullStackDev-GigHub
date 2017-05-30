using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException();            

            var userId = User.Identity.GetUserId();
            var attending = _unitOfWork.AttendanceRepository.Retrieve(userId, dto.GigId);
            if (attending != null) return BadRequest("Already attending this gig");
            
            var attendance = new Attendance
            {
                GigId      = dto.GigId,
                AttendeeId = userId
            };
           
            _unitOfWork.AttendanceRepository.Add(attendance);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int gigId)
        {            
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.AttendanceRepository.Retrieve(userId, gigId);
                        
            if(attendance == null) return BadRequest("Unable to find attendance");

            _unitOfWork.AttendanceRepository.Remove(attendance);
            _unitOfWork.Complete();            
            return Ok(gigId);
        }
    }
}
