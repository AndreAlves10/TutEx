using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using General.Data;
using General.Models;

namespace General.Controllers
{
    [Produces("application/json")]
    [Route("api/UserPreferences")]
    public class UserPreferencesController : Controller
    {
        private readonly GeneralContext _context;

        public UserPreferencesController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/UserPreferences
        [HttpGet]
        public IEnumerable<UserPreferences> GetUserPreference()
        {
            return _context.UserPreference;
        }

        // GET: api/UserPreferences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);

            if (userPreferences == null)
            {
                return NotFound();
            }

            return Ok(userPreferences);
        }

        // PUT: api/UserPreferences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPreferences([FromRoute] int id, [FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userPreferences.UserID)
            {
                return BadRequest();
            }

            _context.Entry(userPreferences).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPreferencesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserPreferences
        [HttpPost]
        public async Task<IActionResult> PostUserPreferences([FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserPreference.Add(userPreferences);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPreferences", new { id = userPreferences.UserID }, userPreferences);
        }

        // DELETE: api/UserPreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);
            if (userPreferences == null)
            {
                return NotFound();
            }

            _context.UserPreference.Remove(userPreferences);
            await _context.SaveChangesAsync();

            return Ok(userPreferences);
        }

        private bool UserPreferencesExists(int id)
        {
            return _context.UserPreference.Any(e => e.UserID == id);
        }
    }
}