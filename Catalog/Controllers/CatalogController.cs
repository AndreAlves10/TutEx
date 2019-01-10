using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Catalog.Data;
using Catalog.Logging;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _context;
        private readonly ILogger _logger;

        public CatalogController(CatalogContext context, ILogger<CatalogController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        #region Create
        [HttpPost]//TODO
        [Route("teachers")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateNewTeacher([FromBody]Teacher newTeacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = new Teacher
            {
                Name = "Test",
                Birthday = new DateTime(1991, 6, 10),
                Nacionality = "PT",
                AcademicalDegree = "Master",
                //SpokenLanguages = new List<SpokenLanguage>(new SpokenLanguage[] { }),
                PricePerHour = 15.0,
                TeachingAreas = "Math, History"
            };

            try
            {
                //await _context.Teachers.AddAsync(newTeacher);
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetTeacherByID), new { id = teacher.Id }, null);
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("teachers")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeachers()
        {
            _logger.LogInformation(LoggingEvents.GetTeachers, "GetTeachers() method Started");//This library cannot write to files

            try
            {
                var teachers = await _context.Teachers.ToListAsync();

                if (teachers == null)
                {
                    _logger.LogError(LoggingEvents.GetTeachersNotFound, "GetTeachers() NOT FOUND any teachers");
                    return NotFound();
                }

                _logger.LogInformation(LoggingEvents.GetTeachers, "GetTeachers() Ended with success");
                return Json(Ok(teachers));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.GetTeachers, "GetTeachers() BadRequest " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
 
        [HttpGet]
        [Route("teachers/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeacherByID(int id)
        {
            if (id < 0)
                return BadRequest();

            try
            {
                var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Id == id);

                if (teacher == null)
                    return NotFound();

                return Json(Ok(teacher));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("teacherspokenlanguages")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeachersSpokenLanguages()
        {
            try
            {
                var teacherSpokenLanguages = await _context.TeacherSpokenLanguages.ToListAsync();

                if (teacherSpokenLanguages == null)
                    return NotFound();

                return Json(Ok(teacherSpokenLanguages));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("teacherspokenlanguages/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeacherSpokenLanguages(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                var teacherSpokenLanguages = await _context.TeacherSpokenLanguages.SingleOrDefaultAsync(teacherSpokenLanguage => teacherSpokenLanguage.TeacherID == id);

                if (teacherSpokenLanguages == null)
                    return NotFound();

                return Json(Ok(teacherSpokenLanguages));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        #endregion

        #region Update
        #endregion

        #region Delete
        [HttpDelete]
        [Route("teachers")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var teacher = _context.Teachers.SingleOrDefault(t => t.Id == id);

                if (teacher == null)
                    return NotFound();

                _context.Teachers.Remove(teacher);

                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        #endregion
    }
}
