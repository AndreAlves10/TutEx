using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Messaging.Data;
using Messaging.Models;
using System.Net;
using System;

namespace Messaging.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly MessagingContext _context;

        public MessagesController(MessagingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Messages
        [HttpGet]
        [Route("messages")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                var messages = await _context.Messages.ToListAsync();

                if (messages == null)
                    return NotFound();

                return Json(Ok(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        // GET: api/Messages/5
        [HttpGet]
        [Route("messages/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);

                if (message == null)
                    return NotFound();

                return Json(Ok(message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT: api/Messages/5
        [HttpPut("message/{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!MessageExists(id))
                    return NotFound();

                if (id != message.Id)
                    return BadRequest();
            
                _context.Entry(message).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Messages
        [HttpPost]
        [Route("message")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMessage", new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Messages/5
        [HttpDelete]
        [Route("message/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);
                if (message == null)
                    return NotFound();
            
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();

                return Json(Ok(message));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}