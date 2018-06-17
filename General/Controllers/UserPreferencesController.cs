using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using General.Data;
using General.Models;
using System.Net;

namespace General.Controllers
{
    [Produces("application/json")]
    [Route("api/UserPreferences")]
    public class UserPreferencesController : Controller
    {
        private readonly GeneralContext _context;

        public UserPreferencesController(GeneralContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create
        // POST: api/UserPreferences
        [HttpPost]
        public async Task<IActionResult> PostUserPreferences([FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.UserPreference.Add(userPreferences);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction("GetUserPreferences", new { id = userPreferences.UserID }, userPreferences);
        }
        #endregion

        #region Read
        // GET: api/UserPreferences
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserPreferences), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPreference()
        {
            var userPreferences = await _context.UserPreference.ToListAsync();

            if (userPreferences == null)
                return NotFound();

            return Json(Ok(userPreferences));
        }

        // GET: api/UserPreferences/5
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserPreferences), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);

            if (userPreferences == null)
                return NotFound();
            

            return Json(Ok(userPreferences));
        }
        #endregion

        #region Update
        // PUT: api/UserPreferences/5
        [Route("PutUserPreferences/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutUserPreferences([FromRoute] int id, [FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (id != userPreferences.UserID)
                return BadRequest();
            
            _context.Entry(userPreferences).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPreferencesExists(id))
                    return NotFound();
                else
                    throw;
                
            }

            return NoContent();
        }
        #endregion

        #region Delete
        // DELETE: api/UserPreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);
            if (userPreferences == null)
                return NotFound();
            
            _context.UserPreference.Remove(userPreferences);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return Json(Ok(userPreferences));
        }
        #endregion

        private bool UserPreferencesExists(int id)
        {
            return _context.UserPreference.Any(e => e.UserID == id);
        }
    }
}