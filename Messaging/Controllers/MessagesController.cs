using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Messaging.Data;
using Messaging.Models;
using System.Net;

namespace Messaging.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly MessagingContext _context;

        public MessagesController(MessagingContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _context.Messages.ToListAsync();

            if (messages == null)
                return NotFound();

            return Json(Ok(_context.Messages));
        }

        // GET: api/Messages/5
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);

            if (message == null)
                return NotFound();
            
            return Json(Ok(message));
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            if (id != message.Id)
                return BadRequest();
            

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Messages
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);
            if (message == null)
                return NotFound();
            

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}