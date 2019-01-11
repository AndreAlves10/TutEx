using System;
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
    [Route("api/Meetings")]
    public class MeetingController : Controller
    {
        private readonly MeetingContext _context;

        public MeetingController(MeetingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create
        [HttpPost]
        [Route("meeting")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateNewMeeting([FromBody] TeacherStudentMeeting newMeeting)
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

            try
            {
                //await _context.Meetings.AddAsync(newMeeting);
                await _context.Meetings.AddAsync(meeting);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateNewMeeting), new { id = meeting.Id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("meeting/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeetingByID(int id)
        {
            if (id < 0)
                return BadRequest();

            try
            {
                var meeting = await _context.Meetings.SingleOrDefaultAsync(t => t.Id == id);

                if (meeting == null)
                    return NotFound();

                return Json(Ok(meeting));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("meeting")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeetings()
        {
            try
            {
                var meetings = await _context.Meetings.ToListAsync();

                if (meetings == null)
                    return NotFound();

                return Json(Ok(meetings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("meeting/student/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public IActionResult GetMeetingsByStudentID(int id)
        {
            if (id < 0)
                return BadRequest();

            try
            {
                var studentMeetings = _context.Meetings.Where(elem => elem.StudentID == id).ToList();

                if (studentMeetings == null)
                    return NotFound();

                return Json(Ok(studentMeetings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("meeting/teacher/id")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeacherStudentMeeting), (int)HttpStatusCode.OK)]
        public IActionResult GetMeetingsByTeacherID(int id)
        {
            if (id < 0)
                return BadRequest();

            try
            {
                var teacherMeetings = _context.Meetings.Where(elem => elem.TeacherID == id).ToList();

                if (teacherMeetings == null)
                    return NotFound();

                return Json(Ok(teacherMeetings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("meeting/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            try
            {
                var meeting = _context.Meetings.SingleOrDefault(t => t.Id == id);

                if (meeting == null)
                    return NotFound();

                _context.Meetings.Remove(meeting);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
