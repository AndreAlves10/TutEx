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
        [Route("users")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostUserPreferences([FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _context.UserPreference.Add(userPreferences);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUserPreferences", new { id = userPreferences.UserID }, userPreferences);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Read
        // GET: api/users
        [HttpGet]
        [Route("users")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserPreferences), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPreference()
        {
            try
            {
                var userPreferences = await _context.UserPreference.ToListAsync();

                if (userPreferences == null)
                    return NotFound();

                return Json(Ok(userPreferences));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/UserPreferences/5
        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserPreferences), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);

                if (userPreferences == null)
                    return NotFound();

                return Json(Ok(userPreferences));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Update
        // PUT: api/users/5
        [HttpPut]
        [Route("users/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutUserPreferences([FromRoute] int id, [FromBody] UserPreferences userPreferences)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                UserPreferences oldUserPreferences = await _context.UserPreference.FirstOrDefaultAsync(e => e.UserID == id);

                if (oldUserPreferences == null)
                    return NotFound();

                if (id != userPreferences.UserID)
                    return BadRequest();

                oldUserPreferences.SystemLanguange = userPreferences.SystemLanguange;
                oldUserPreferences.Currency = userPreferences.Currency;
            
                _context.Entry(userPreferences).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        // DELETE: api/UserPreferences/5
        [HttpDelete]
        [Route("users/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserPreferences), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserPreferences([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userPreferences = await _context.UserPreference.SingleOrDefaultAsync(m => m.UserID == id);
                if (userPreferences == null)
                    return NotFound();
            
                _context.UserPreference.Remove(userPreferences);
                await _context.SaveChangesAsync();

                return Json(Ok(userPreferences));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
        #endregion

        private bool UserPreferencesExists(int id)
        {
            return _context.UserPreference.Any(e => e.UserID == id);
        }
    }
}