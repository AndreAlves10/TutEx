using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Data;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _context;

        public CatalogController(CatalogContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost(Name = "CreateNewTeacher")]//Should be HttpPut?
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateNewTeacher()
        {
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

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

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
            var teachers = await _context.Teachers.ToListAsync();

            if (teachers == null)
                return NotFound();

            return Ok(Json(teachers));
        }
 
        [HttpGet]
        [Route("teachers/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeacherByID(int id)
        {
            if (id < 0)
                return BadRequest();

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return NotFound();

            return Ok(Json(teacher));
        }

        [HttpGet]
        [Route("teacherspokenlanguages")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeachersSpokenLanguages()
        {
            var teacherSpokenLanguages = await _context.TeacherSpokenLanguages.ToListAsync();

            if (teacherSpokenLanguages == null)
                return NotFound();

            return Ok(Json(teacherSpokenLanguages));
        }

        [HttpGet]
        [Route("teacherspokenlanguages/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Teacher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTeacherSpokenLanguages(int id)
        {
            if (id < 0)
                return BadRequest();

            var teacherSpokenLanguages = await _context.TeacherSpokenLanguages.SingleOrDefaultAsync(teacherSpokenLanguage => teacherSpokenLanguage.TeacherID == id);

            if (teacherSpokenLanguages == null)
                return NotFound();

            return Ok(Json(teacherSpokenLanguages));
        }

        #endregion

        #region Update
        #endregion

        #region Delete
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.SingleOrDefault(t => t.Id == id);

            if (teacher == null)
                return NotFound();

            _context.Teachers.Remove(teacher);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        // POST: api/Catalog
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Catalog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
