﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Meeting.Data;
using Meeting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Controllers
{
    [Produces("application/json")]
    [Route("api/Meeting")]
    public class MeetingController : Controller
    {
        private readonly MeetingContext _context;

        public MeetingController(MeetingContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost(Name = "CreateNewMeeting")]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateNewMeeting()
        {
            var meeting = new TeacherStudentMeeting
            {
                AcceptedByTeacher = false,
                Duration = 120,
                MeetingDate = new DateTime(2018, 06, 13),
                StudentID = 1,
                TeacherID = 2,
                TotalCost = 40
            };

            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateNewMeeting), new { id = meeting.Id }, null);
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeetingByID(int id)
        {
            if (id < 0)
                return BadRequest();

            var meeting = await _context.Meetings.SingleOrDefaultAsync(t => t.Id == id);

            if (meeting == null)
                return NotFound();

            return Ok(Json(meeting));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeetings()
        {
            var meetings = await _context.Meetings.ToListAsync();

            if (meetings == null)
                return NotFound();

            return Ok(Json(meetings));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public IActionResult GetMeetingsByStudentID(int id)
        {
            if (id < 0)
                return BadRequest();

            var studentMeetings = _context.Meetings.Where(elem => elem.StudentID == id).ToList();

            if (studentMeetings == null)
                return NotFound();

            return Ok(Json(studentMeetings));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public IActionResult GetMeetingsByTeacherID(int id)
        {
            if (id < 0)
                return BadRequest();

            var teacherMeetings = _context.Meetings.Where(elem => elem.TeacherID == id).ToList();

            if (teacherMeetings == null)
                return NotFound();

            return Ok(Json(teacherMeetings));
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var meeting = _context.Meetings.SingleOrDefault(t => t.Id == id);

            if (meeting == null)
                return NotFound();

            _context.Meetings.Remove(meeting);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        // PUT: api/Meeting/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
